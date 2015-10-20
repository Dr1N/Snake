using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class Block
    {
        #region Координаты блока
        private int top;
        public int Top
        {
            get
            {
                return this.top;
            }
            set
            {
                if (0 <= value && value < Console.WindowHeight)
                {
                    this.top = value;
                }
                else
                {
                    throw new Exception("Top координата блока выходит за пределы окна");
                }
            }
        }

        private int left;
        public int Left
        {
            get
            {
                return this.left;
            }
            set
            {
                if (0 <= value && value < Console.WindowWidth)
                {
                    this.left = value;
                }
                else
                {
                    throw new Exception("Left координата блока выходит за пределы окна");
                }
            }
        }
        #endregion

        #region Внешний вид блока
        private string symbol = "■";
        public string Symbol
        {
            get
            {
                return this.symbol;
            }
            set
            {
                if (value.Length > 1)
                {
                    throw new Exception("Длина строки для блока больше одного символа");
                }
                this.symbol = value;
            }
        }

        private ConsoleColor color = ConsoleColor.White;
        public ConsoleColor Color
        {
            get
            {
                return this.color;
            }
            set
            {
                this.color = value;
            }
        }
        #endregion

        #region Конструкторы
        public Block() { }
        public Block(int top, int left)
        {
            this.Top = top;
            this.Left = left;
        }
        public Block(int top, int left, string symbol)
        {
            this.Top = top;
            this.Left = left;
            this.Symbol = symbol;
        }
        public Block(string symbol)
        {
            this.Symbol = symbol;
        }
        #endregion

        #region Отображение
        public void Draw()
        {
            Console.ForegroundColor = color;
            Console.SetCursorPosition(this.left, this.top);
            Console.Write(this.Symbol);
            Console.ResetColor();
        }
        public void Erase()
        {
            Console.SetCursorPosition(this.left, this.top);
            for (int i = 0; i < symbol.Length; i++)
            {
                Console.Write(" ");
            }

        }
        #endregion

        #region Переопределение базовых методов
        public override bool Equals(object obj)
        {
            if (!(obj is Block))
            {
                return false;
            }
            return ((Block)obj).Top == this.Top && ((Block)obj).Left == this.Left;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #endregion
    }
}
