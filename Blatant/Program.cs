using Blatant.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blatant
{
    class Program
    {
        private static string promo;
        private static string choice;
        private static string groupMember;
        private static string str = string.Empty;
        private static IList<Option> groceryList;
        static Program()
        {
            groceryList = new List<Option>();
        }
        static void Main(string[] args)
        {
            choice = "A";            
            System.Console.WriteLine("*************GroceryCo**************");
            str += "*************GroceryCo**************\n";
            while (promo != "Y" && promo != "N")
            {
                System.Console.WriteLine("Are there any promotions at this time (Y/N)?");
                str += "Are there any promotions at this time (Y/N)?\n";
                promo = Console.ReadLine().ToUpper();

                System.Console.WriteLine("Is the customer a group member (Y/N)?");
                str += "Is the customer a group member (Y/N)?\n";
                groupMember = Console.ReadLine().ToUpper();
            }

            // Read each line of the file into a string array. Each element
            // of the array is one line of the file.
            try
            {
                string[] lines = System.IO.File.ReadAllLines(@"../Opening.txt");
                while (choice.ToUpper() != "X")
                {
                    // Display the file contents by using a foreach loop.
                    Option[] options = ReadFromFile(lines);
                    foreach (Option option in options)
                    {
                        // Use a tab to indent each line of the file.
                        if (promo == "Y")
                        {
                            Console.WriteLine("\t" + option.ID + " " + option.OptionName + " - $" + option.Discount);
                            str += "\t" + option.ID + " " + option.OptionName + " - $" + option.Discount + "\n";
                        }
                        else
                        {
                            Console.WriteLine("\t" + +option.ID + " " + option.OptionName + " - $" + option.OptionPrice);
                            str += "\t" + option.ID + " " + option.OptionName + " - $" + option.OptionPrice + "\n";
                        }
                    }

                    // Keep the console window open in debug mode.
                    Console.WriteLine("Please enter an option. Or X to close and print the receipt.");
                    str += "Please enter an option. Or X to close and print the receipt.";
                    choice = System.Console.ReadLine();
                    if (lines.Count() > 0)
                    {
                        int parse;
                        bool iparsed = int.TryParse(choice, out parse);
                        if (iparsed && options.Where(x => x.ID == parse).Count() > 0)
                        {
                            var groceryItem = options.Where(x => x.ID == parse).SingleOrDefault();
                            groceryList.Add(groceryItem);
                            Recalculate();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            if (PrintToFile() == true)
            {
                Console.WriteLine("Print Complete");
                Console.ReadLine();
            }
        }

        public static bool PrintToFile()
        {
            Console.WriteLine("Please enter the name of your printer: ");
            var printerName = Console.ReadLine();
            Print.SendStringToPrinter(printerName, str);
            return true;
        }

        /// <summary>
        /// Recalculate total wih group promo.  Ex. Buy 3 for $2.00
        /// </summary>
        public static void Recalculate()
        {
            decimal? cost = 0;
            int i = 0;           
            foreach (var groceryItem in groceryList)
            {
                if (promo == "Y")
                {
                    cost = cost + groceryItem.Discount;
                }
                else
                {
                    if (promo == "N" && groupMember == "Y" && groceryList.Where(x => x.ID == groceryItem.ID).Count() % 3 == 0)
                    {
                        i++;
                        if (i % 3 == 0)
                        {
                            Console.WriteLine("$2.00 for three " + groceryItem.OptionName + " promotion applied for group member.");
                            groceryItem.OptionPrice = 2;
                        }
                        else
                        {
                            groceryItem.OptionPrice = 0;
                        }
                    }
                    cost = cost + groceryItem.OptionPrice;
                }
            }
            Console.Write("Total: $" + cost + ".\n\n\n");
            str += "Total: $" + cost + ".\n";
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
