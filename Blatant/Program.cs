using Blatant.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blatant
{
    class Program
    {
        static string promo;
        static string choice;
        static double? cost;
        static void Main(string[] args)
        {
            choice = "A"; 
            System.Console.WriteLine("**********GroceryCo***********");
            while (promo != "Y" && promo != "N")
            {
                Console.WriteLine("Are there any promotions at this time (Y/N?");
                promo = Console.ReadLine();
            }

            // Read each line of the file into a string array. Each element
            // of the array is one line of the file.

            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\Ryan\Documents\Blatant\Opening.txt");
            Option[] options = ReadFromFile(lines);
            while (choice.ToUpper() != "X")
            {
                // Display the file contents by using a foreach loop.

                foreach (string line in lines)
                {
                    // Use a tab to indent each line of the file.
                    Console.WriteLine("\t" + line);
                }

                //  ReadFromFile(lines);
                // Keep the console window open in debug mode.
                Console.WriteLine("Please enter an option. Or X to close");
                choice = System.Console.ReadLine();
                int parse;
                bool iparsed = int.TryParse(choice,out parse);
                if (iparsed)
                {
                    if (promo == "Y")
                        cost += options.SingleOrDefault(x => x.ID == parse).Discount;
                    else
                        cost += options.Where(x => x.ID == parse).Select(x => x.OptionPrice);
                Console.Write("Total: $" + cost + ".\n");
                }
            }
        }



        public static Option[] ReadFromFile(string[] file)
        {
            int count = file.Count();
            Option[] option = new Option[count];
            for (int i = 0; i < count; i++)
            {
                string[] size = file[i].Split(new char[0]);
                option[i] = new  Option ();
                option[i].ID = i;
                option[i].OptionName = size[0];
                option[i].OptionPrice = double.Parse(size[1].TrimStart('$'));
                option[i].Discount = double.Parse(size[2].TrimStart('$'));

            }
            return option;
        }
    }
}
