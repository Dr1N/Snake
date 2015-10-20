using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class Snake : IEnumerable
    {
        //Направления движения
        public enum DIRECTION
        {
            NORTH,
            EAST,
            SOUTH,
            WEST
        }

        #region Параметры змейки
        private int startSnakeLength = 5;                       //Начальная длина змейки
        private List<Block> snake = new List<Block>();          //Контейнер блоков змейки

        private DIRECTION currentDirection = DIRECTION.NORTH;   //Направление движения
        public DIRECTION Direction
        {
            get
            {
                return this.currentDirection;
            }
            set
            {
                //Изменить направление, если оно не направлено внутрь змейки
                Block newHeadPosition = this.GetNextHead(value);
                Block neck = this.GetNeck();
                if (newHeadPosition.Equals(this.GetNeck()))
                {
                    return;
                }
                this.currentDirection = value;
            }
        }
        #endregion

        #region Конструкторы
        /// <summary>
        /// Создание змейки, длина по умолчанию, положение - центр игрового поля, направление - по умолчанию на север
        /// </summary>
        public Snake()
        {
            int left = Game.GameAreaOffsetX + Game.GameAreaLength / 2;
            int top = Game.GameAreaOffsetY + Game.GameAreaHeight / 2;

            for (int i = 0; i < startSnakeLength; i++)
            {
                snake.Add(new Block(top + i, left));
            }
        }
        /// <summary>
        /// Создание змейки в указанной позиции
        /// </summary>
        /// <param name="top">координата Y</param>
        /// <param name="left">Координата X</param>
        public Snake(int top, int left)
        {
            for (int i = 0; i < startSnakeLength; i++)
            {
                snake.Add(new Block(top + i, left));
            }
        }
        #endregion

        #region Функции отображения змейки
        /// <summary>
        /// Отобразить змейку
        /// </summary>
        public void Draw()
        {
            if (snake == null || snake.Count == 0)
            {
                throw new Exception("Ошибка. В змейке нет блоков");
            }
            foreach (Block block in snake)
            {
                block.Draw();
            }
        }
        /// <summary>
        /// Очистить змейку
        /// </summary>
        public void Erase()
        {
            if (snake == null || snake.Count == 0)
            {
                throw new Exception("Ошибка. В змейке нет блоков");
            }
            foreach (Block block in snake)
            {
                block.Erase();
            }
        }
        #endregion

        #region Манипуляции со змейкой
        /// <summary>
        /// Получить голову змейки
        /// </summary>
        /// <returns>Блок, который являетс головой змейки</returns>
        public Block GetHead()
        {
            if (snake == null || snake.Count == 0)
            {
                throw new Exception("Ошибка. В змее нет блоков");
            }

            return snake.First();
        }
        /// <summary>
        /// Получение блока, который будет новой головой змейки при перемещении в указанном направлении
        /// </summary>
        /// <param name="direction">Направление движения змейки</param>
        /// <returns>Ссылку на объект Block, который должна занять голова змейки после перемещения</returns>
        public Block GetNextHead(DIRECTION direction)
        {
            Block head = this.GetHead();
            Block newHead = new Block();
            switch (direction)
            {
                case DIRECTION.NORTH:
                    newHead.Left = head.Left;
                    newHead.Top = head.Top - 1;
                    break;
                case DIRECTION.EAST:
                    newHead.Left = head.Left + 1;
                    newHead.Top = head.Top;
                    break;
                case DIRECTION.SOUTH:
                    newHead.Left = head.Left;
                    newHead.Top = head.Top + 1;
                    break;
                case DIRECTION.WEST:
                    newHead.Left = head.Left - 1;
                    newHead.Top = head.Top;
                    break;
                default:
                    break;
            }
            return newHead;
        }
        /// <summary>
        /// Вернуть "шею" змейки, блок предшетсвующий голове
        /// </summary>
        /// <returns>Ссылка на блок идущий следующим за головой</returns>
        private Block GetNeck()
        {
            if (snake == null || snake.Count == 0)
            {
                throw new Exception("Ошибка. В змейке нет блоков");
            }
            return snake[1];
        }
        /// <summary>
        /// Получить хвост змейки
        /// </summary>
        /// <returns>Ссылка на последний блок змейки</returns>
        private Block GetTail()
        {
            if (snake == null || snake.Count == 0)
            {
                throw new Exception("Ошибка. В змее нет блоков");
            }
            return snake.Last();
        }
        /// <summary>
        /// Добавить блок в начало змейки (в голову)
        /// </summary>
        /// <param name="head">Ссылка на блок, что станет головой змейки</param>
        private void AddHead(Block head)
        {
            if (head == null)
            {
                throw new Exception("Ссылка на объект указывает на null");
            }
            snake.Insert(0, head);
        }
        /// <summary>
        /// Удалить последний блок змейки (хвост)
        /// </summary>
        private void RemoveTail()
        {
            if (snake == null || snake.Count == 0)
            {
                throw new Exception("Ошибка. В змее нет блоков");
            }
            snake.Remove(snake.Last());
        }
        /// <summary>
        /// Получение длины змейки
        /// </summary>
        /// <returns>Количество блоков в змейке</returns>
        public int GetLength()
        {
            if (snake != null)
            {
                return snake.Count;
            }
            return 0;
        }
        #endregion

        #region Перемещение змейки
        /// <summary>
        /// Перемещение змеи в текущем направлении
        /// </summary>
        /// <param name="isFood">Если змейка "съела" еду, хвост не удаляем</param>
        public void Move(bool isFood)
        {
            Block newHead = this.GetNextHead(this.currentDirection);
            this.Erase();
            this.AddHead(newHead);
            if (!isFood)
            {
                this.RemoveTail();
            }
            this.Draw();
        }
        /// <summary>
        /// Проверка не "замкнулась ли змейка"
        /// </summary>
        /// <returns>true - змейка замкнулась, false - нет</returns>
        public bool isCollision()
        {
            //Не совпали ли блоки внутри змейки, начинаем с 3го по индексу
            Block head = this.GetHead();
            for (int i = 2; i < this.snake.Count; i++)
            {
                if (head.Equals(this.snake[i]))
                {
                    return true;
                }
            }
            return false;
        }

        public void ChangeDirection(ConsoleKeyInfo key)
        {
            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    this.Direction = Snake.DIRECTION.NORTH;
                    break;
                case ConsoleKey.RightArrow:
                    this.Direction = Snake.DIRECTION.EAST;
                    break;
                case ConsoleKey.DownArrow:
                    this.Direction = Snake.DIRECTION.SOUTH;
                    break;
                case ConsoleKey.LeftArrow:
                    this.Direction = Snake.DIRECTION.WEST;
                    break;
                default:
                    break;
            }
        }
        #endregion

        public IEnumerator GetEnumerator()
        {
            return snake.GetEnumerator();
        }
    }
}
