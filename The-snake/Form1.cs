using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;  // Для Path.Combine
using System.Windows.Forms;

namespace The_snake
{
    public partial class Form1 : Form
    {
        // Список точек для сегментов змейки.
        private List<Point> snake = new List<Point>();
        // Точка яблока.
        private Point apple;
        // Точка бонуса.
        private Point bonus;
        private bool bonusActive = false;
        private int score = 0;
        private int bonusTicks = 0;
        private int bonusDurationTicks = 30;

        // Направление движения змейки.
        private int directionX = 1;
        private int directionY = 0;
        // Размер ячейки.
        private int gridSize = 20;
        // Таймер игрового цикла.
        private Timer gameTimer = new Timer();
        private bool gameOver = false;
        private Random rand = new Random();
        // Метка для отображения счета.
        private Label scoreLabel = new Label();

        public Form1()
        {
            InitializeComponent();

            // Задаем заголовок окна.
            this.Text = "The Snake: Корпоративная версия";

            // Указываем путь к иконке, лежащей в папке Resources.
            string iconPath = Path.Combine(Application.StartupPath, "Resources", "act.ico");
            this.Icon = new Icon(iconPath);

            // Настройка формы.
            this.DoubleBuffered = true;
            this.ClientSize = new Size(800, 600);
            this.BackColor = Color.Black;
            this.KeyDown += new KeyEventHandler(OnKeyDown);

            // Настройка метки для счета.
            scoreLabel.ForeColor = Color.White;
            scoreLabel.BackColor = Color.Transparent;
            scoreLabel.Font = new Font("Arial", 16, FontStyle.Bold);
            scoreLabel.Location = new Point(10, 10);
            scoreLabel.AutoSize = true;
            scoreLabel.Text = "Счет: 0";
            this.Controls.Add(scoreLabel);

            // Инициализация игры.
            StartGame();

            // Настройка таймера.
            gameTimer.Interval = 100;
            gameTimer.Tick += Update;
            gameTimer.Start();
        }

        private void StartGame()
        {
            snake.Clear();
            snake.Add(new Point(ClientSize.Width / 2, ClientSize.Height / 2));
            PlaceApple();
            bonusActive = false;
            score = 0;
            scoreLabel.Text = "Счет: 0";
            directionX = 1;
            directionY = 0;
            gameOver = false;
            bonusTicks = 0;
        }

        // Проверяет, находится ли точка внутри змейки.
        private bool IsPointOnSnake(Point p)
        {
            foreach (Point segment in snake)
            {
                if (p.Equals(segment))
                    return true;
            }
            return false;
        }

        private void PlaceApple()
        {
            int maxX = ClientSize.Width / gridSize;
            int maxY = ClientSize.Height / gridSize;
            Point p;
            do
            {
                p = new Point(rand.Next(0, maxX) * gridSize, rand.Next(0, maxY) * gridSize);
            }
            while (IsPointOnSnake(p));
            apple = p;
        }

        private void PlaceBonus()
        {
            int maxX = ClientSize.Width / gridSize;
            int maxY = ClientSize.Height / gridSize;
            Point p;
            do
            {
                p = new Point(rand.Next(0, maxX) * gridSize, rand.Next(0, maxY) * gridSize);
            }
            while (IsPointOnSnake(p) || p.Equals(apple));
            bonus = p;
            bonusActive = true;
            bonusTicks = 0;
        }

        private void Update(object sender, EventArgs e)
        {
            // Проверка на победу: если змейка заняла все клетки.
            int maxCells = (ClientSize.Width / gridSize) * (ClientSize.Height / gridSize);
            if (snake.Count >= maxCells)
            {
                gameOver = true;
            }

            if (gameOver)
            {
                gameTimer.Stop();

                string message = (snake.Count >= maxCells)
                    ? $"Поздравляем! Вы победили!\nВаш счет: {score}"
                    : $"Игра окончена!\nВаш счет: {score}";

                // Показываем меню после окончания игры.
                using (MenuForm menu = new MenuForm(message + "\n\nНажмите \"Продолжить\", чтобы сыграть снова,\nили \"Выйти\", чтобы закрыть игру."))
                {
                    if (menu.ShowDialog(this) == DialogResult.OK)
                    {
                        StartGame();
                        gameTimer.Start();
                    }
                    else
                    {
                        Application.Exit();
                    }
                }
                return;
            }

            if (!bonusActive)
            {
                if (rand.NextDouble() < 0.01)
                {
                    PlaceBonus();
                }
            }
            else
            {
                bonusTicks++;
                if (bonusTicks >= bonusDurationTicks)
                {
                    bonusActive = false;
                }
            }

            Point head = snake[0];
            Point newHead = new Point(head.X + directionX * gridSize, head.Y + directionY * gridSize);

            if (newHead.X < 0 || newHead.X >= ClientSize.Width ||
                newHead.Y < 0 || newHead.Y >= ClientSize.Height)
            {
                gameOver = true;
            }

            foreach (Point segment in snake)
            {
                if (newHead.Equals(segment))
                {
                    gameOver = true;
                    break;
                }
            }

            if (gameOver)
            {
                Invalidate();
                return;
            }

            snake.Insert(0, newHead);

            if (newHead.Equals(apple))
            {
                score++;
                scoreLabel.Text = "Счет: " + score;
                PlaceApple();
                if (score % 5 == 0 && gameTimer.Interval > 50)
                {
                    gameTimer.Interval -= 5;
                }
            }
            else if (bonusActive && newHead.Equals(bonus))
            {
                score += 5;
                scoreLabel.Text = "Счет: " + score;
                bonusActive = false;
            }
            else
            {
                snake.RemoveAt(snake.Count - 1);
            }

            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            using (Pen gridPen = new Pen(Color.FromArgb(40, 40, 40)))
            {
                for (int x = 0; x < ClientSize.Width; x += gridSize)
                    g.DrawLine(gridPen, x, 0, x, ClientSize.Height);
                for (int y = 0; y < ClientSize.Height; y += gridSize)
                    g.DrawLine(gridPen, 0, y, ClientSize.Width, y);
            }

            g.FillRectangle(Brushes.Red, new Rectangle(apple.X, apple.Y, gridSize, gridSize));

            if (bonusActive)
                g.FillRectangle(Brushes.Gold, new Rectangle(bonus.X, bonus.Y, gridSize, gridSize));

            foreach (Point p in snake)
                g.FillRectangle(Brushes.Lime, new Rectangle(p.X, p.Y, gridSize, gridSize));
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    if (directionY == 0)
                    {
                        directionX = 0;
                        directionY = -1;
                    }
                    break;
                case Keys.Down:
                    if (directionY == 0)
                    {
                        directionX = 0;
                        directionY = 1;
                    }
                    break;
                case Keys.Left:
                    if (directionX == 0)
                    {
                        directionX = -1;
                        directionY = 0;
                    }
                    break;
                case Keys.Right:
                    if (directionX == 0)
                    {
                        directionX = 1;
                        directionY = 0;
                    }
                    break;
            }
        }
    }
}
