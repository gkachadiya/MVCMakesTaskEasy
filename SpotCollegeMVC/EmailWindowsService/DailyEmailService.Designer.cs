﻿namespace EmailWindowsService
{
    partial class DailyEmailService
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
            this.eventLogEmail = new System.Diagnostics.EventLog();
            ((System.ComponentModel.ISupportInitialize)(this.eventLogEmail)).BeginInit();
            // 
            // DailyEmailService
            // 
            this.ServiceName = "DailyEmailService";
            ((System.ComponentModel.ISupportInitialize)(this.eventLogEmail)).EndInit();

        }

        #endregion

        private System.Diagnostics.EventLog eventLogEmail;
    }
}
