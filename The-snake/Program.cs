using System;
using System.Windows.Forms;

namespace The_snake
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Показываем окно меню при запуске
            using (MenuForm menu = new MenuForm("Выберите действие:"))
            {
                DialogResult result = menu.ShowDialog();
                if (result == DialogResult.OK)
                {
                    // Если пользователь нажал "Играть", запускаем основную форму
                    Application.Run(new Form1());
                }
                else
                {
                    // Иначе завершаем приложение
                    Application.Exit();
                }
            }
        }
    }
}
