using System;
using System.Drawing;
using System.Windows.Forms;

namespace The_snake
{
    public partial class MenuForm : Form
    {
        // Свойство для получения выбора пользователя.
        public bool PlayAgain { get; private set; } = false;

        // Конструктор принимает сообщение для отображения на форме.
        public MenuForm(string message)
        {
            InitializeComponent();
            lblMessage.Text = message;
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            PlayAgain = true;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            PlayAgain = false;
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
