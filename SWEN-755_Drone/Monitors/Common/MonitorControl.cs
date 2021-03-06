﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Security.AccessControl;
using System.Threading;
using System.Timers;
using System.Windows.Forms;

namespace Common
{
    public partial class MonitorControl : UserControl
    {
        private string _processId;
        private ModuleTpe _moduleTpe;
        private int _instanceCount;
        private System.Timers.Timer _hangingTimer;
        private Dictionary<string, DateTime> _lastRecordedTime;

        public MonitorControl()
        {
            InitializeComponent();
        }

        public void Initialize(string processId, ModuleTpe type, int instanceCount)
        {
            _processId = processId;
            _moduleTpe = type;
            _instanceCount = instanceCount;
            _lastRecordedTime = new Dictionary<string, DateTime>();

            for (var i = 0; i < instanceCount; i++)
            {
                var threadStream = new Thread(StartStream);
                threadStream.IsBackground = true;
                threadStream.Start();
            }

            _hangingTimer = new System.Timers.Timer();
            _hangingTimer.Interval = 1000;
            _hangingTimer.Elapsed += _hangingTimer_Elapsed;
            _hangingTimer.Enabled = true;

        }


        private void _hangingTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            foreach (var item in _lastRecordedTime)
            {
                TimeSpan x = DateTime.Now.Subtract(item.Value);
                Console.WriteLine();
                if (x.Seconds > 2)
                {
                    UpdateWorkStatusLog($"Alert: {item.Key} is hanging", Color.Blue);
                }
                
            }
        }

        private void StartStream()
        {
            Console.WriteLine("PipeTo" + _processId + _moduleTpe.ToString());

            PipeSecurity ps = new PipeSecurity();
            ps.AddAccessRule(new PipeAccessRule("Users", PipeAccessRights.ReadWrite | PipeAccessRights.CreateNewInstance, AccessControlType.Allow));
            ps.AddAccessRule(new PipeAccessRule("CREATOR OWNER", PipeAccessRights.FullControl, AccessControlType.Allow));
            ps.AddAccessRule(new PipeAccessRule("SYSTEM", PipeAccessRights.FullControl, AccessControlType.Allow));
            ps.AddAccessRule(new PipeAccessRule("Everyone", PipeAccessRights.ReadWrite, AccessControlType.Allow));

            NamedPipeServerStream pipeStream = new NamedPipeServerStream("PipeTo" + _processId + _moduleTpe.ToString(), PipeDirection.InOut,
                _instanceCount, PipeTransmissionMode.Message, PipeOptions.WriteThrough, 1024, 1024, ps);
            Console.WriteLine("[Server] {0} Pipe Created; Process ID: {1}", _moduleTpe.ToString(),
                Process.GetCurrentProcess().Id);

            pipeStream.WaitForConnection();
            Console.WriteLine("[Server] {0} Pipe connection established; Process ID: {1}", _moduleTpe.ToString(),
                Process.GetCurrentProcess().Id);

            StreamReader streamReader = new StreamReader(pipeStream);
            string module = string.Empty, message = string.Empty, messageType = string.Empty;            
            while ((message = streamReader.ReadLine()) != null)
            {
                module = message.Split(';')[0];
                messageType = message.Split(';')[1];
                switch (messageType)
                {
                    case "Connected":
                        UpdateProcessStatusLog($"Connected: {module}", Color.DarkCyan);                    
                        break;
                    case "Alive":
                        UpdateWorkStatusLog($"Alive: {module}", Color.DarkCyan);
                        if (!_lastRecordedTime.ContainsKey(module))
                            _lastRecordedTime.Add(module, DateTime.Now);
                        else
                            _lastRecordedTime[module] = DateTime.Now;
                        break;
                }
            }

            UpdateProcessStatusLog($"Disconnected: {module}", Color.Red);
            UpdateWorkStatusLog($"Dead: {module}", Color.Red);
            _lastRecordedTime.Remove(module);
        }

        private void UpdateProcessStatusLog(string text, Color fontColor)
        {
            if (this.textBoxProcessStatusLog.InvokeRequired)
            {
                UpdateLogCallback callback = new UpdateLogCallback(UpdateProcessStatusLog);
                this.Invoke(callback, new object[] { text, fontColor });
            }
            else
            {
                textBoxProcessStatusLog.Focus();
                textBoxProcessStatusLog.SelectionColor = fontColor;
                textBoxProcessStatusLog.AppendText(Environment.NewLine + text);
            }
        }

        private void UpdateWorkStatusLog(string text, Color fontColor)
        {
            if (this.textBoxProcessWorkStatusLog.InvokeRequired)
            {
                UpdateLogCallback callback = new UpdateLogCallback(UpdateWorkStatusLog);
                this.Invoke(callback, new object[] { text, fontColor });
            }
            else
            {
                textBoxProcessWorkStatusLog.Focus();
                textBoxProcessWorkStatusLog.SelectionColor = fontColor;
                textBoxProcessWorkStatusLog.AppendText(Environment.NewLine + text);
            }
        }

        delegate void UpdateLogCallback(string text, Color fontColor);

        public enum ModuleTpe
        {
            Critical,
            NonCritical
        }

    }
}
