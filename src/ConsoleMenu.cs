using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    abstract class ConsoleMenu
    {
        //=========================== Поля ===========================

        private int top = -1;                                       //координаты
        private int left = -1;
        private string[] menuItems;                                 //пункты меню
        private bool coordinates = false;                           //флаг, true - меню с координатами, false - без, выводится в текущем месте курсора
        private bool numbered = true;                               //флаг, true - пукнты номерованы
        protected int activeItem = 1;                               //активный пункт при выводе меню
        private bool cleanable = false;                             //флаг, true - меню удаляется после выбора
        //цветовое оформление
        private ConsoleColor textColor = ConsoleColor.Gray;
        private ConsoleColor backColor = ConsoleColor.Black;
        private ConsoleColor activeTextColor = ConsoleColor.Black;
        private ConsoleColor activeBackColor = ConsoleColor.Gray;

        //=========================== Свойства ===========================

        public string[] MenuItems
        {
            get
            {
                return this.menuItems;
            }
            set
            {
                this.menuItems = value;
            }
        }
        public int Top
        {
            get
            {
                return this.top;
            }
            set
            {
                if (0 <= value && value < Console.WindowHeight)
                    this.top = value;
                else
                    this.top = -1;
            }
        }
        public int Left
        {
            get
            {
                return this.left;
            }
            set
            {
                if (0 <= value && value < Console.WindowWidth)
                    this.left = value;
                else
                    this.left = -1;
            }
        }
        public bool Coordinates
        {
            get
            {
                return this.coordinates;
            }
            set
            {
                this.coordinates = value;
            }
        }
        public bool Numbered
        {
            get
            {
                return this.numbered;
            }
            set
            {
                this.numbered = value;
            }
        }
        public int ActiveItem
        {
            get
            {
                return this.activeItem;
            }
            set
            {
                if (0 >= value || value > this.menuItems.Length)
                    return;
                this.activeItem = value;
            }
        }
        public bool Cleanable
        {
            get
            {
                return this.cleanable;
            }
            set
            {
                this.cleanable = value;
            }
        }
        public ConsoleColor TextColor
        {
            get
            {
                return this.textColor;
            }
            set
            {
                this.textColor = value;
            }
        }
        public ConsoleColor BackColor
        {
            get
            {
                return this.backColor;
            }
            set
            {
                this.backColor = value;
            }
        }
        public ConsoleColor ActiveTextColor
        {
            get
            {
                return this.activeTextColor;
            }
            set
            {
                this.activeTextColor = value;
            }
        }
        public ConsoleColor ActiveBackColor
        {
            get
            {
                return this.activeBackColor;
            }
            set
            {
                this.activeBackColor = value;
            }
        }

        //=========================== Методы ===========================

        public ConsoleMenu(string[] MenuItems)
        {
            this.MenuItems = MenuItems;
        }
        public ConsoleMenu(string[] MenuItems, int top, int left)
        {
            this.MenuItems = MenuItems;
            this.coordinates = true;
            this.Top = top;
            this.Left = left;
        }
        protected bool isValidMenu()
        {
            if (this.MenuItems == null)
            {
                return false;
            }
            if (this.Coordinates && (this.Top < 0 || this.Top >= Console.WindowHeight || this.Left < 0 || this.Left >= Console.WindowWidth))
            {
                return false;
            }
            return true;
        }
        public abstract int ShowMenu();
        protected abstract void ClearMenu();
    }
    class VerticalConsoleMenu : ConsoleMenu
    {
        public VerticalConsoleMenu(string[] menuItems)
            : base(menuItems) { }

        public VerticalConsoleMenu(string[] menuItems, int top, int left)
            : base(menuItems, top, left) { }

        public override int ShowMenu()
        {
            if (!this.isValidMenu())
            {
                Console.WriteLine("Ошибка. Невозможно вывести меню. Неверные параметры");
                return -1;
            }

            //начальные координаты вывода меню
            int cursorTop, cursorLeft;
            if (this.Coordinates)
            {
                cursorTop = this.Top;
                cursorLeft = this.Left;
            }
            else
            {
                cursorTop = Console.CursorTop;
                cursorLeft = Console.CursorLeft;
                //сохраняем координаты для очистки меню без координат 
                if (!this.Coordinates && this.Cleanable)
                {
                    this.Left = Console.CursorLeft;
                    this.Top = Console.CursorTop;
                }
            }
            //выводим меню и ждём выбора
            while (true)
            {
                int numberMenuItem = 1;
                int count = 0;
                foreach (string menuItem in this.MenuItems)
                {
                    Console.CursorTop = cursorTop + count;
                    Console.CursorLeft = cursorLeft;
                    //подсветка активного пункта
                    if (numberMenuItem == this.ActiveItem)
                    {
                        Console.ForegroundColor = this.ActiveTextColor;
                        Console.BackgroundColor = this.ActiveBackColor;
                    }
                    //номерация пунктов если установлено
                    if (this.Numbered)
                    {
                        Console.Write("{0}. {1}", numberMenuItem, menuItem);
                    }
                    else
                    {
                        Console.Write("{0}", menuItem);
                    }
                    Console.ResetColor();
                    ++numberMenuItem;
                    ++count;
                }
                //анализ нажатий на клавиатуру
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.Enter:
                        if (this.Cleanable)
                        {
                            this.ClearMenu();
                        }
                        return this.activeItem;
                    case ConsoleKey.Escape:
                        this.ClearMenu();
                        return 0;
                    case ConsoleKey.UpArrow:
                        --this.activeItem;
                        if (this.activeItem <= 0)
                        {
                            this.activeItem = this.MenuItems.Length;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        ++this.activeItem;
                        if (this.activeItem > this.MenuItems.Length)
                        {
                            this.activeItem = 1;
                        }
                        break;
                }
            }
        }

        protected override void ClearMenu()
        {
            int count = 0;
            foreach (string menuItem in this.MenuItems)
            {
                Console.CursorTop = this.Top + count;
                Console.CursorLeft = this.Left;
                int length = menuItem.Length;                       //длина строки пункта меню
                if (this.Numbered)
                {
                    length += count.ToString().Length + 2;          //учёт пробела и точки
                }
                for (int index = 0; index < length; ++index)
                {
                    Console.Write(" ");
                }
                ++count;
            }
            Console.CursorLeft = this.Left;
            Console.CursorTop = this.Top;
            //"обнуление" координат для меню без координат
            if (!this.Coordinates)
            {
                this.Top = -1;
                this.Left = -1;
            }
        }
    }
}