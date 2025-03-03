using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace The_snake
{
    partial class MenuForm
    {
        private IContainer components = null;
        private Label lblMessage;
        private Button btnPlay;
        private Button btnExit;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        private void InitializeComponent()
        {
            this.lblMessage = new Label();
            this.btnPlay = new Button();
            this.btnExit = new Button();
            this.SuspendLayout();
            // 
            // lblMessage
            // 
            this.lblMessage.Font = new Font("Arial", 14F, FontStyle.Bold);
            this.lblMessage.ForeColor = Color.Black;
            this.lblMessage.Location = new Point(12, 20);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new Size(360, 50);
            this.lblMessage.TabIndex = 0;
            this.lblMessage.Text = "Сообщение";
            this.lblMessage.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnPlay
            // 
            this.btnPlay.Font = new Font("Arial", 12F);
            this.btnPlay.Location = new Point(50, 90);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new Size(130, 40);
            this.btnPlay.TabIndex = 1;
            this.btnPlay.Text = "Играть";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new EventHandler(this.btnPlay_Click);
            // 
            // btnExit
            // 
            this.btnExit.Font = new Font("Arial", 12F);
            this.btnExit.Location = new Point(220, 90);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new Size(130, 40);
            this.btnExit.TabIndex = 2;
            this.btnExit.Text = "Выход";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new EventHandler(this.btnExit_Click);
            // 
            // MenuForm
            // 
            this.ClientSize = new Size(384, 161);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.lblMessage);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.Name = "MenuForm";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Меню";
            this.ResumeLayout(false);
        }

        #endregion
    }
}
