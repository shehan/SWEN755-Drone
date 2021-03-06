﻿namespace Common
{
    partial class MonitorControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxProcessStatusLog = new System.Windows.Forms.RichTextBox();
            this.textBoxProcessWorkStatusLog = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Process Status";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(230, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Process Work Status";
            // 
            // textBoxProcessStatusLog
            // 
            this.textBoxProcessStatusLog.Location = new System.Drawing.Point(0, 34);
            this.textBoxProcessStatusLog.Name = "textBoxProcessStatusLog";
            this.textBoxProcessStatusLog.ReadOnly = true;
            this.textBoxProcessStatusLog.Size = new System.Drawing.Size(206, 330);
            this.textBoxProcessStatusLog.TabIndex = 4;
            this.textBoxProcessStatusLog.Text = "";
            // 
            // textBoxProcessWorkStatusLog
            // 
            this.textBoxProcessWorkStatusLog.Location = new System.Drawing.Point(224, 34);
            this.textBoxProcessWorkStatusLog.Name = "textBoxProcessWorkStatusLog";
            this.textBoxProcessWorkStatusLog.ReadOnly = true;
            this.textBoxProcessWorkStatusLog.Size = new System.Drawing.Size(206, 330);
            this.textBoxProcessWorkStatusLog.TabIndex = 5;
            this.textBoxProcessWorkStatusLog.Text = "";
            // 
            // MonitorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textBoxProcessWorkStatusLog);
            this.Controls.Add(this.textBoxProcessStatusLog);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "MonitorControl";
            this.Size = new System.Drawing.Size(433, 380);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox textBoxProcessStatusLog;
        private System.Windows.Forms.RichTextBox textBoxProcessWorkStatusLog;
    }
}
