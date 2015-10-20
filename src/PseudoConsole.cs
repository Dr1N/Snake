/*
 * Fleim 2011 [fleim@inbox.ru] * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PsCon
{
    // Содержит в себе символы, используемые при выводе не закрашенного прямоугольника или квадрата состоящий из простой линии.
    struct FrameLine
    {
        public string topLeft;
        public string topRight;
        public string bottomLeft;
        public string bottomRight;
        public string lineX;
        public string lineY;

        public FrameLine(int n)
        {
            topLeft = "┌";
            topRight = "┐";
            bottomLeft = "└";
            bottomRight = "┘";
            lineX = "─";
            lineY = "│";
        }
    }

    // Содержит в себе символы, используемые при выводе не закрашенного прямоугольника или квадрата состоящий из двойной линии.
    struct FrameDoubleLine
    {
        public string topLeft;
        public string topRight;
        public string bottomLeft;
        public string bottomRight;
        public string lineA;
        public string lineB;

        public FrameDoubleLine(int n)
        {
            topLeft = "╔";
            topRight = "╗";
            bottomLeft = "╚";
            bottomRight = "╝";
            lineA = "═";
            lineB = "║";
        }
    }

    // Содержит в себе символы, используемые при выводе закрашенного прямоугольника или квадрата.
    struct Square
    {
        public string model1;
        public string model2;
        public string model3;
        public string model4;
        public string model5;

        public Square(int n)
        {
            model1 = "■";
            model2 = "█";
            model3 = "▓";
            model4 = "▒";
            model5 = "░";
        }
    }

    // Содержит в себе символы, используемые при выводе горизонтальноый линии.
    struct HorizontalLine
    {
        public string left;
        public string right;
        public string line;
        public string cross;

        public HorizontalLine(int n)
        {
            left = "├";
            right = "┤";
            line = "─";
            cross = "┼";
        }
    }

    // Содержит в себе символы, используемые при выводе двойной горизонтальноый линии.
    struct HorizontalLineDouble
    {
        public string left;
        public string right;
        public string line;
        public string cross;

        public HorizontalLineDouble(int n)
        {
            left = "╠";
            right = "╣";
            line = "═";
            cross = "╬";
        }
    }

    // Содержит в себе символы, используемые при выводе вертикальной линии.
    struct VerticalLine
    {
        public string top;
        public string bottom;
        public string line;
        public string cross;

        public VerticalLine(int n)
        {
            top = "┬";
            bottom = "┴";
            line = "│";
            cross = "┼";
        }
    }

    // Содержит в себе символы, используемые при выводе двойной вертикальной линии.
    struct VerticalLineDouble
    {
        public string top;
        public string bottom;
        public string line;
        public string cross;

        public VerticalLineDouble(int n)
        {
            top = "╦";
            bottom = "╩";
            line = "║";
            cross = "╬";
        }
    }

    static class PsCon
    {
        // Задает загаловок консольного приложения, размер буфера окна, цвет и фон текста.
        public static void OpenBuffer(string bufferName, int bufferSizeX, int bufferSizeY, ConsoleColor text, ConsoleColor background)
        {
            Console.Title = bufferName;
            Console.SetWindowSize(bufferSizeX, bufferSizeY);
            Console.ForegroundColor = text;
            Console.BackgroundColor = background;
            Console.Clear();
            Console.SetCursorPosition(0, 0);
        }

        // Выводит прямоугольник или квадрат, состоящий из простой линии, не закрашивается.
        public static void PrintFrameLine(int positionX, int positionY, int sizeX, int sizeY, ConsoleColor text, ConsoleColor background)
        {
            int SizeX = positionX + sizeX;
            int SizeY = positionY + sizeY;
            FrameLine f = new FrameLine(0);

            Console.ForegroundColor = text;
            Console.BackgroundColor = background;

            for (int y = positionY; y < SizeY; y++)
            {
                for (int x = positionX; x < SizeX; x++)
                {
                    if (y == positionY && x == positionX)
                    {
                        Console.SetCursorPosition(x, y);
                        Console.Write(f.topLeft);
                    }

                    if (y == positionY && x > positionX && x < SizeX - 1)
                    {
                        Console.SetCursorPosition(x, y);
                        Console.Write(f.lineX);
                    }

                    if (y == positionY && x == SizeX - 1)
                    {
                        Console.SetCursorPosition(x, y);
                        Console.Write(f.topRight);
                    }

                    if (y > positionY && y < SizeY - 1 && x == positionX || y > positionY && y < SizeY - 1 && x == SizeX - 1)
                    {
                        Console.SetCursorPosition(x, y);
                        Console.Write(f.lineY);
                    }

                    if (y == SizeY - 1 && x == positionX)
                    {
                        Console.SetCursorPosition(x, y);
                        Console.Write(f.bottomLeft);
                    }

                    if (y == SizeY - 1 && x > positionX && x < SizeX - 1)
                    {
                        Console.SetCursorPosition(x, y);
                        Console.Write(f.lineX);
                    }

                    if (y == SizeY - 1 && x == SizeX - 1)
                    {
                        Console.SetCursorPosition(x, y);
                        Console.Write(f.bottomRight);
                    }
                }
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }

        // Выводит прямоугольник или квадрат, состоящий из двойной линии, не закрашивается.
        public static void PrintFrameDoubleLine(int positionX, int positionY, int sizeX, int sizeY, ConsoleColor text, ConsoleColor background)
        {
            int SizeY = positionY + sizeY;
            int SizeX = positionX + sizeX;

            FrameDoubleLine f = new FrameDoubleLine(0);
            Console.ForegroundColor = text;
            Console.BackgroundColor = background;

            for (int y = positionY; y < SizeY; y++)
            {
                for (int x = positionX; x < SizeX; x++)
                {
                    if (y == positionY && x == positionX)
                    {
                        Console.SetCursorPosition(x, y);
                        Console.Write(f.topLeft);
                    }
                    if (y == positionY && x > positionX && x < SizeX - 1)
                    {
                        Console.SetCursorPosition(x, y);
                        Console.Write(f.lineA);
                    }
                    if (y == positionY && x == SizeX - 1)
                    {
                        Console.SetCursorPosition(x, y);
                        Console.Write(f.topRight);
                    }
                    if (y > positionY && y < SizeY - 1 && x == positionX || y > positionY && y < SizeY - 1 && x == SizeX - 1)
                    {
                        Console.SetCursorPosition(x, y);
                        Console.Write(f.lineB);
                    }
                    if (y == SizeY - 1 && x == positionX)
                    {
                        Console.SetCursorPosition(x, y);
                        Console.Write(f.bottomLeft);
                    }
                    if (y == SizeY - 1 && x > positionX && x < SizeX - 1)
                    {
                        Console.SetCursorPosition(x, y);
                        Console.Write(f.lineA);
                    }
                    if (y == SizeY - 1 && x == SizeX - 1)
                    {
                        Console.SetCursorPosition(x, y);
                        Console.Write(f.bottomRight);
                    }
                }
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }

        // Выводит закрашеный прямоугольник или квадрат.
        // Параметр model принемает значение от 0 до 4.
        public static void PrintSquare(int model, int positionX, int positionY, int sizeX, int sizeY, ConsoleColor text, ConsoleColor background)
        {
            int SizeX = positionX + sizeX;
            int SizeY = positionY + sizeY;
            Square sq = new Square(0);
            string square = "Error!";

            Console.ForegroundColor = text;
            Console.BackgroundColor = background;

            switch (model)
            {
                case 0:
                    square = sq.model1;
                    break;
                case 1:
                    square = sq.model2;
                    break;
                case 2:
                    square = sq.model3;
                    break;
                case 3:
                    square = sq.model4;
                    break;
                case 4:
                    square = sq.model5;
                    break;
            }

            for (int y = positionY; y < SizeY; y++)
            {
                for (int x = positionX; x < SizeX; x++)
                {
                    Console.SetCursorPosition(x, y);
                    Console.Write(square);
                }
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }

        // Выводит горизонтальную линию заданной длины, цвета и фона.
        // Имеет разделитель cross, если указан параметр -1, будет проигнорирован.
        // Метод перегружен, первым параметром следует указать true или false, тогда линия будет сплошная.
        public static void PrintHorizontalLine(int positionX, int positionY, int sizeX, int cross, ConsoleColor text, ConsoleColor background)
        {
            int SizeX = positionX + sizeX;
            HorizontalLine hdl = new HorizontalLine(0);

            Console.ForegroundColor = text;
            Console.BackgroundColor = background;

            for (int x = positionX; x < SizeX; x++)
            {
                if (x == positionX)
                {
                    Console.SetCursorPosition(x, positionY);
                    Console.Write(hdl.left);
                }

                if (x > positionX && x < SizeX - 1)
                {
                    Console.SetCursorPosition(x, positionY);
                    Console.Write(hdl.line);
                }

                if (x == SizeX - 1)
                {
                    Console.SetCursorPosition(x, positionY);
                    Console.Write(hdl.right);
                }
            }

            if (cross != -1)
            {
                Console.SetCursorPosition(cross, positionY);
                Console.Write(hdl.cross);
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }
        public static void PrintHorizontalLine(bool cansel, int positionX, int positionY, int sizeX, int cross, ConsoleColor text, ConsoleColor background)
        {
            int SizeX = positionX + sizeX;
            HorizontalLine hdl = new HorizontalLine(0);

            Console.ForegroundColor = text;
            Console.BackgroundColor = background;

            for (int x = positionX; x < SizeX; x++)
            {
                Console.SetCursorPosition(x, positionY);
                Console.Write(hdl.line);
            }

            if (cross != -1)
            {
                Console.SetCursorPosition(cross, positionY);
                Console.Write(hdl.cross);
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }

        // Выводит двойную горизонтальную линию заданной длины, цвета и фона.
        // Имеет разделитель cross, если указан параметр -1, будет проигнорирован.
        // Метод перегружен, первым параметром следует указать true или false, тогда линия будет сплошная.
        public static void PrintHorizontalDoubleLine(int positionX, int positionY, int sizeX, int cross, ConsoleColor text, ConsoleColor background)
        {
            int SizeX = positionX + sizeX;
            HorizontalLineDouble hdl = new HorizontalLineDouble(0);

            Console.ForegroundColor = text;
            Console.BackgroundColor = background;

            for (int x = positionX; x < SizeX; x++)
            {
                if (x == positionX)
                {
                    Console.SetCursorPosition(x, positionY);
                    Console.Write(hdl.left);
                }

                if (x > positionX && x < SizeX - 1)
                {
                    Console.SetCursorPosition(x, positionY);
                    Console.Write(hdl.line);
                }

                if (x == SizeX - 1)
                {
                    Console.SetCursorPosition(x, positionY);
                    Console.Write(hdl.right);
                }
            }

            if (cross != -1)
            {
                Console.SetCursorPosition(cross, positionY);
                Console.Write(hdl.cross);
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }
        public static void PrintHorizontalDoubleLine(bool cansel, int positionX, int positionY, int sizeX, int cross, ConsoleColor text, ConsoleColor background)
        {
            int SizeX = positionX + sizeX;
            HorizontalLineDouble hdl = new HorizontalLineDouble(0);

            Console.ForegroundColor = text;
            Console.BackgroundColor = background;

            for (int x = positionX; x < SizeX; x++)
            {
                Console.SetCursorPosition(x, positionY);
                Console.Write(hdl.line);
            }

            if (cross != -1)
            {
                Console.SetCursorPosition(cross, positionY);
                Console.Write(hdl.cross);
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }

        // Выводит вертикальную линию заданной длины, цвета и фона.
        // Имеет разделитель cross, если указан параметр -1, будет проигнорирован.
        // Метод перегружен, первым параметром следует указать true или false, тогда линия будет сплошная.
        public static void PrintVerticalLine(int positionX, int positionY, int sizeY, int cross, ConsoleColor text, ConsoleColor background)
        {
            int SizeY = positionY + sizeY;
            VerticalLine hdl = new VerticalLine(0);

            Console.ForegroundColor = text;
            Console.BackgroundColor = background;

            for (int y = positionY; y < SizeY; y++)
            {
                if (y == positionY)
                {
                    Console.SetCursorPosition(positionX, y);
                    Console.Write(hdl.top);
                }

                if (y > positionY && y < SizeY - 1)
                {
                    Console.SetCursorPosition(positionX, y);
                    Console.Write(hdl.line);
                }

                if (y == SizeY - 1)
                {
                    Console.SetCursorPosition(positionX, y);
                    Console.Write(hdl.bottom);
                }
            }

            if (cross != -1)
            {
                Console.SetCursorPosition(positionX, cross);
                Console.Write(hdl.cross);
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }
        public static void PrintVerticalLine(bool cansel, int positionX, int positionY, int sizeY, int cross, ConsoleColor text, ConsoleColor background)
        {
            int SizeY = positionY + sizeY;
            VerticalLine hdl = new VerticalLine(0);

            Console.ForegroundColor = text;
            Console.BackgroundColor = background;

            for (int y = positionY; y < SizeY; y++)
            {
                Console.SetCursorPosition(positionX, y);
                Console.Write(hdl.line);
            }

            if (cross != -1)
            {
                Console.SetCursorPosition(positionX, cross);
                Console.Write(hdl.cross);
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }

        // Выводит двойную вертикальную линию заданной длины, цвета и фона.
        // Имеет разделитель cross, если указан параметр -1, будет проигнорирован.
        // Метод перегружен, первым параметром следует указать true или false, тогда линия будет сплошная.
        public static void PrintVerticalLineDouble(int positionX, int positionY, int sizeY, int cross, ConsoleColor text, ConsoleColor background)
        {
            int SizeY = positionY + sizeY;
            VerticalLineDouble hdl = new VerticalLineDouble(0);

            Console.ForegroundColor = text;
            Console.BackgroundColor = background;

            for (int y = positionY; y < SizeY; y++)
            {
                if (y == positionY)
                {
                    Console.SetCursorPosition(positionX, y);
                    Console.Write(hdl.top);
                }

                if (y > positionY && y < SizeY - 1)
                {
                    Console.SetCursorPosition(positionX, y);
                    Console.Write(hdl.line);
                }

                if (y == SizeY - 1)
                {
                    Console.SetCursorPosition(positionX, y);
                    Console.Write(hdl.bottom);
                }
            }

            if (cross != -1)
            {
                Console.SetCursorPosition(positionX, cross);
                Console.Write(hdl.cross);
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }
        public static void PrintVerticalLineDouble(bool cansel, int positionX, int positionY, int sizeY, int cross, ConsoleColor text, ConsoleColor background)
        {
            int SizeY = positionY + sizeY;
            VerticalLineDouble hdl = new VerticalLineDouble(0);

            Console.ForegroundColor = text;
            Console.BackgroundColor = background;

            for (int y = positionY; y < SizeY; y++)
            {
                Console.SetCursorPosition(positionX, y);
                Console.Write(hdl.line);
            }

            if (cross != -1)
            {
                Console.SetCursorPosition(positionX, cross);
                Console.Write(hdl.cross);
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }

        // Выводит заданное количество символов с указанной позицый, указанного цвета и фона.
        // Метод перегружен, первым параметром следует указать true или false, тогда символы будут выводиться вертикально.
        public static void PrintCountChar(string ch, int positionX, int positionY, int size, ConsoleColor text, ConsoleColor background)
        {
            int SizeX = positionX + size;

            Console.ForegroundColor = text;
            Console.BackgroundColor = background;

            for (int x = positionX; x < SizeX; x++)
            {
                Console.SetCursorPosition(x, positionY);
                Console.Write(ch);
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }
        public static void PrintCountChar(bool cansel, string ch, int positionX, int positionY, int size, ConsoleColor text, ConsoleColor background)
        {
            int SizeY = positionY + size;

            Console.ForegroundColor = text;
            Console.BackgroundColor = background;

            for (int y = positionY; y < SizeY; y++)
            {
                Console.SetCursorPosition(positionX, y);
                Console.Write(ch);
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }

        // Выводит текст или символ заданного цвета и фона в заданной позиции.
        public static void PrintString(string str, int X, int Y, ConsoleColor text, ConsoleColor background)
        {
            Console.ForegroundColor = text;
            Console.BackgroundColor = background;

            Console.SetCursorPosition(X, Y);
            Console.Write(str);

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }

        // Выводит целую переменную заданного цвета и фона в заданной позиции.
        public static void PrintCount(int count, int X, int Y, ConsoleColor text, ConsoleColor background)
        {
            Console.ForegroundColor = text;
            Console.BackgroundColor = background;

            Console.SetCursorPosition(X, Y);
            Console.Write(count);

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }

        // Выводит дробную переменную заданного цвета и фона в заданной позиции.
        public static void PrintDouble(double count, int X, int Y, ConsoleColor text, ConsoleColor background)
        {
            Console.ForegroundColor = text;
            Console.BackgroundColor = background;

            Console.SetCursorPosition(X, Y);
            Console.Write(count);

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }

        // Метод возвращает случайное число, перегружен.
        // Первый метод возвращает от 0 до 100.
        // Второй метод имеет два параметры на вход. Минимальное и максимальное значение, между указанными значениями будет згенирировано число.
        public static int Rand()
        {
            int point;
            Random r = new Random();
            point = r.Next(0, 100);

            return point;
        }
        public static int Rand(int min, int max)
        {
            int point;
            Random r = new Random();
            point = r.Next(min, max);

            return point;
        }
    }
}