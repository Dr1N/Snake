using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Snake
{
    class Game
    {
        private int tactInterval = 10;

        //============================= Поля =============================

        //событие, которое будет вызываться по такту таймера
        public event OnTimer Actions;
        public event OnKey ChangeSnakeDirection;

        #region Временные параметры игры
        private int foodLifeTime;                      //время жизни еды в тактах
        private int moveTime;                          //время перемещения змейки на одну позицию в тактах
        private int tactCount;                         //счётчик тактов
        #endregion

        #region Параметры интерфейса игры
        public static int ScreenLength = 120;
        public static int ScreenHeight = 40;
        public static int GameAreaLength = 100;
        public static int GameAreaHeight = 25;
        public static int GameAreaOffsetX = 10;
        public static int GameAreaOffsetY = 3;
        public static int PanelInfoHeight = 6;
        #endregion

        #region Вспомогательные объекты
        private static Random rand = new Random();
        #endregion

        #region Объекты игры
        private Snake snake;
        private List<Block> foods;
        #endregion

        #region Состояние игры
        private bool GameOver = false;
        private int score;
        private DateTime gameStartTime;
        private int foodAmount;                     //Количество одновременно существуещей еды на игровом поле
        private bool generatedFirstFood = false;    //Флаг для генерации еды вначале игры
        #endregion

        #region Конструкторы
        /// <summary>
        /// Установить параметры окна
        /// </summary>
        static Game()
        {
            //Настройка консоли
            Console.SetWindowSize(ScreenLength, ScreenHeight);
            Console.SetBufferSize(ScreenLength, ScreenHeight);
            Console.Title = "Snake";
            Console.CursorVisible = false;
        }
        #endregion

        //============================= Методы =============================

        #region Игровой процесс
        /// <summary>
        /// Основной метод - процесс игры
        /// </summary>
        public void BeginGame()
        {
            bool exit = false;
            while (!exit)
            {
                this.InitGame();
                //Игровой цикл
                while (!this.GameOver)
                {
                    Thread.Sleep(this.tactInterval);
                    this.tactCount++;
                    this.Actions();
                    this.ShowInfo();
                    this.SetDifficulty();
                    //Если нажата клавиша - посылаем событие змейке
                    if (Console.KeyAvailable == true)
                    {
                        ChangeSnakeDirection(Console.ReadKey());
                    }
                }
                //Game over
                PsCon.PsCon.PrintString("G A M E  O V E R", GameAreaOffsetX + GameAreaLength / 2 - "G A M E  O V E R".Length / 2, GameAreaOffsetY + GameAreaHeight / 2, ConsoleColor.Red, ConsoleColor.White);
                //Главное меню
                if (this.ShowMainMenu() == 2)
                {
                    exit = true;
                }
            }
        }
        /// <summary>
        /// Инициализация параметров игры, подготовка окна
        /// </summary>
        private void InitGame()
        {
            //Сброс(инициализация) параметров
            this.generatedFirstFood = false;
            this.GameOver = false;
            this.tactCount = 0;
            this.foodLifeTime = 800;
            this.moveTime = 10;
            this.gameStartTime = DateTime.Now;
            this.score = 0;
            this.foodAmount = 1;
            //Обновление интерфейса игры
            Console.Clear();
            this.DrawInterface();
            //Создание игровых объектов
            this.snake = new Snake();
            this.foods = new List<Block>();
            //Регистрируем события для объектов
            this.Actions += this.GenerateFood;
            this.Actions += this.MoveSnake;
            this.ChangeSnakeDirection += this.snake.ChangeDirection;
        }
        /// <summary>
        /// Установка сложности игры в зависимости от набранных очков
        /// </summary>
        private void SetDifficulty()
        {
            switch (this.score)
            {
                case 5:
                    this.moveTime = 8;
                    this.foodLifeTime = 640;
                    break;
                case 10:
                    this.moveTime = 6;
                    this.foodLifeTime = 480;
                    break;
                case 15:
                    this.moveTime = 4;
                    this.foodLifeTime = 320;
                    break;
                case 20:
                    this.moveTime = 2;
                    this.foodLifeTime = 160;
                    break;
            }
        }

        #endregion

        #region Действия игровых объектов (вызываются через делегат)
        /// <summary>
        /// Генерация еды на игровом поле
        /// </summary>
        private void GenerateFood()
        {
            //Генерируем еду, если начало игры, если пришло время, или вся еда съедена
            if (tactCount % this.foodLifeTime != 0 && this.generatedFirstFood && foods.Count != 0)
            {
                return;
            }
            //Если количество еды, что может находится на карте, превышает максиальное - чистим коллекцию еды и консоль
            if (foods.Count >= this.foodAmount)
            {
                this.ClearFood();
                foods.Clear();
            }
            //Генерируем еду в случайном месте игрового поля, если попали в змейку - повторим
            string[] foodSymbols = { "@", "#", "☺", "☻", "♦", "☼" };
            Block food = null;
            int topFood, leftFood;
            bool overlap = true;
            while (overlap)
            {
                //Генерация координат
                topFood = rand.Next(GameAreaOffsetY + 1, GameAreaOffsetY + GameAreaHeight - 1);
                leftFood = rand.Next(GameAreaOffsetX + 1, GameAreaOffsetX + GameAreaLength - 1);
                //Символа (в конструкторе)
                food = new Block(topFood, leftFood, foodSymbols[rand.Next(0, foodSymbols.Length)]);
                //Цвета
                food.Color = (ConsoleColor)rand.Next(9, 16);
                //Проверка не попали ли в змею при генерации
                foreach (Block snakeBlock in snake)
                {
                    if (food.Equals(snakeBlock))
                    {
                        overlap = true;
                        break;
                    }
                    else
                    {
                        overlap = false;
                    }
                }
            }
            this.generatedFirstFood = true;
            this.foods.Add(food);
            this.DrawFood();
        }
        private void MoveSnake()
        {
            //Проверка, пришло ли время перемещаться
            if (tactCount % this.moveTime != 0)
            {
                return;
            }

            //Не съели ли еду?
            //Получаем положение головы в следующий момент
            Block nextHead = snake.GetNextHead(snake.Direction);
            //Не совпала ли она с едой
            bool isFood = false;
            foreach (Block food in foods)
            {
                //Если совпала - поедаем еду, удаляю из коллекции
                if (nextHead.Equals(food))
                {
                    isFood = true;
                    score++;
                    food.Erase();
                    foods.Remove(food);
                    //to-do сброс счётчика, для одинакового времени жизни еды
                    if (foods.Count == 0)
                    {
                        tactCount = tactCount / this.foodLifeTime;
                    }
                    break;
                }
            }
            //Перемещение змейки с учётом была ли съедена еда
            snake.Move(isFood);
            //Не замкнулась ли змейка
            if (snake.isCollision())
            {
                this.GameOver = true;
                return;
            }
            //Не вышли ли за игровою зону
            Block head = snake.GetHead();
            bool isInVerticalRange = GameAreaOffsetY < head.Top && head.Top < GameAreaOffsetY + GameAreaHeight - 1;
            bool isInHorizontalRange = GameAreaOffsetX < head.Left && head.Left < GameAreaOffsetX + GameAreaLength - 1;
            if (!isInVerticalRange || !isInHorizontalRange)
            {
                this.GameOver = true;
                return;
            }
        }
        #endregion

        #region Отрисовка еды
        private void DrawFood()
        {
            if (foods == null || foods.Count == 0)
            {
                throw new Exception("Еды нет!");
            }
            for (int i = 0; i < foods.Count; i++)
            {
                foods[i].Draw();
            }
        }
        private void ClearFood()
        {
            if (foods.Count != 0)
            {
                for (int i = 0; i < foods.Count; i++)
                {
                    foods[i].Erase();
                }
            }
        }
        #endregion

        #region Интерфейс игры
        private void ShowInfo()
        {
            DateTime currentTime = DateTime.Now;
            TimeSpan gameTime = currentTime.Subtract(this.gameStartTime);
            string time = String.Format("Время:\t{0:D2}:{1:D2}", gameTime.Minutes, gameTime.Seconds);
            string score = String.Format("Очки:\t{0}", this.score);
            string length = String.Format("Длина:\t{0}", snake.GetLength());
            string speed = String.Format("Скорость:\t{0} с/с", (int)(1000 / this.tactInterval) / this.moveTime);

            PsCon.PsCon.PrintString(time, GameAreaOffsetX + 1, GameAreaOffsetY + GameAreaHeight + 1, ConsoleColor.Yellow, ConsoleColor.Black);
            PsCon.PsCon.PrintString(score, GameAreaOffsetX + 1, GameAreaOffsetY + GameAreaHeight + 2, ConsoleColor.Yellow, ConsoleColor.Black);
            PsCon.PsCon.PrintString(length, GameAreaOffsetX + 1, GameAreaOffsetY + GameAreaHeight + 3, ConsoleColor.Yellow, ConsoleColor.Black);
            PsCon.PsCon.PrintString(speed, GameAreaOffsetX + 1, GameAreaOffsetY + GameAreaHeight + 4, ConsoleColor.Yellow, ConsoleColor.Black);
        }
        private int ShowMainMenu()
        {
            string[] menuItems = { "Новая игра", "Выход" };
            int menuTop = GameAreaOffsetY + GameAreaHeight + 1;
            int menuLeft = GameAreaOffsetX + GameAreaLength / 2 + 1;
            ConsoleMenu mainMenu = new VerticalConsoleMenu(menuItems, GameAreaOffsetY + GameAreaHeight + 1, GameAreaOffsetX + GameAreaLength / 2 + 1);
            mainMenu.Cleanable = true;
            return mainMenu.ShowMenu();
        }
        private void DrawInterface()
        {
            //Заголовок
            PsCon.PsCon.PrintString("S N A K E", GameAreaOffsetX + GameAreaLength / 2 - "S N A K E".Length / 2, 1, ConsoleColor.Green, ConsoleColor.Black);
            //Игровое поле
            PsCon.PsCon.PrintFrameDoubleLine(GameAreaOffsetX, GameAreaOffsetY, GameAreaLength, GameAreaHeight, ConsoleColor.Red, ConsoleColor.Black);
            //Ирнформационная панель
            PsCon.PsCon.PrintFrameDoubleLine(GameAreaOffsetX, GameAreaOffsetY + GameAreaHeight, GameAreaLength, PanelInfoHeight, ConsoleColor.White, ConsoleColor.Black);
            PsCon.PsCon.PrintVerticalLineDouble(GameAreaOffsetX + GameAreaLength / 2, GameAreaOffsetY + GameAreaHeight, PanelInfoHeight, -1, ConsoleColor.White, ConsoleColor.Black);
        }
        #endregion
    }
}
