﻿using Blatant.Models;
using System;
using System.Linq;

namespace Blatant
{
    class Program
    {
        private static string promo;
        private static string choice;
        private static decimal? cost = 0;
        static void Main(string[] args)
        {
            choice = "A";
            System.Console.WriteLine("*************GroceryCo**************");
            while (promo != "Y" && promo != "N")
            {
                System.Console.WriteLine("Are there any promotions at this time (Y/N)?");
                promo = Console.ReadLine();
            }

            // Read each line of the file into a string array. Each element
            // of the array is one line of the file.

            string[] lines = System.IO.File.ReadAllLines(@"../Opening.txt");
            Option[] options = ReadFromFile(lines);
            while (choice.ToUpper() != "X")
            {
                // Display the file contents by using a foreach loop.

                foreach (Option option in options)
                {
                    // Use a tab to indent each line of the file.
                    if (promo == "Y")
                    {
                        Console.WriteLine("\t" + option.ID + " " + option.OptionName + " - $" + option.Discount);
                    }
                    else
                    {
                        Console.WriteLine("\t" + +option.ID + " " + option.OptionName + " - $" + option.OptionPrice);
                    }
                }

                // Keep the console window open in debug mode.
                Console.WriteLine("Please enter an option. Or X to close and print the receipt.");

                choice = System.Console.ReadLine();
                if (lines.Count() > 0)
                {
                    int parse;
                    bool iparsed = int.TryParse(choice, out parse);
                    if (iparsed && options.Where(x => x.ID == parse).Count() > 0)
                    {
                        if (promo == "Y")
                            cost = cost + options.Where(x => x.ID == parse).Select(x => x.Discount).SingleOrDefault();
                        else
                            cost = cost + options.Where(x => x.ID == parse).Select(x => x.OptionPrice).SingleOrDefault();

                        Console.Write("Total: $" + cost + ".\n");
                    }
                }
            }
            //PrintToFile();
        }

        public static bool PrintToFile()
        {
            Print printer = new Print();
            printer.PrinttoFile("HP Office Jet", @".\Receipt.txt");
            return true;
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
                option[i].OptionPrice = decimal.Parse(size[1].TrimStart('$'));
                option[i].Discount = decimal.Parse(size[2].TrimStart('$'));
            }
            return option;
        }
    }
}
