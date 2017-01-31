using System;
using System.IO;
using System.Net;

namespace Blatant
{
    public class Print
    {
        public void PrinttoFile(string printerName, string fileName)
        {
            //// Redirect output to a file named Receipt.txt and write file list.
            //StreamWriter sw = File.AppendText(@".\Receipt.txt");
            //sw.AutoFlush = true;
            //Console.SetOut(sw);
            //sw.WriteLine(sw);

            var originalpos = Console.CursorTop;

            var k = Console.ReadKey();
            var i = 2;

            while (k.KeyChar != 'q')
            {

                if (k.Key == ConsoleKey.UpArrow)
                {

                    Console.SetCursorPosition(0, Console.CursorTop - i);
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.WriteLine("Option " + (Console.CursorTop + 1));
                    Console.ResetColor();
                    i++;

                }

                Console.SetCursorPosition(8, originalpos);
                k = Console.ReadKey();
            }
        }
    }
}