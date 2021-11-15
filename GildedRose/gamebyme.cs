using System;
using System.Collections.Generic;
namespace gamebyme
{
    class Program
    {
        static void Main(string[] args)
        {
            List<item> items = new List<item>();

            // default items
            append_item("+5 Dexterity Vest", 10, 20);
            append_item("Aged Brie", 2, 0);
            append_item("Elixir of the Mongoose", 5, 7);
            append_item("Sulfuras, Hand of Ragnaros", 0, 80);
            append_item("Backstage passes to a TAFKAL80ETC concert", 15, 20);
            append_item("Conjured Mana Cake", 3, 6);
            // end of default items

            while (true)          //menu (main loop)
            {
                Console.Clear();
                print_items();
                Console.WriteLine("--------------------------------");
                Console.WriteLine("NEXT, ADD, REMOVE");
                Console.Write("Vyber Moznost: ");
                string x = Console.ReadLine().ToUpper();
                switch (x)
                {
                    case "ADD":
                        Console.Write("\n-----------------------\n\nZadaj názov itemu: ");
                        string temp_name = Console.ReadLine();
                        Console.Write("Zadaj za kolko dni by sa item mal predat: ");
                        int temp_sellin = int.Parse(Console.ReadLine());
                        Console.Write("Zadaj kvalitu: ");
                        int temp_quality = int.Parse(Console.ReadLine());
                        append_item(temp_name, temp_sellin, temp_quality);
                        break;
                    case "NEXT":
                        foreach (var item in items)
                        {
                            item.QualityUpdate();
                        }
                        break;
                    case "REMOVE":
                        Console.Clear();
                        for (int i = 0; i < items.Count; i++)
                        {
                            Console.Write($" {i} ");
                            items[i].print_item();
                        }
                        Console.Write("Zvol index: ");
                        int index = int.Parse(Console.ReadLine());
                        if (index < items.Count || index > 0) items.RemoveAt(index); else error_print();
                        break;
                    default:
                        error_print();
                        break;
                }
            }
            void error_print() { Console.WriteLine("----------WRONG INPUT----------"); System.Threading.Thread.Sleep(1000); }
            void print_items()
            {        //write all items on screen
                foreach (var item in items)
                {
                    item.print_item();
                }
            }
            void append_item(string name, int SellIn, int quality)
            {     //methot to add item
                if (name.Contains("Sulfuras")) { quality = 80; }
                else if (quality > 50) { quality = 50; }
                else if (quality < 0) { quality = 0; }
                items.Add(new item { name = name, SellIn = SellIn, quality = quality });
            }
        }
    }
    public class item
    {
        public string name;
        public int SellIn;
        public int quality;
        public void QualityUpdate()
        {
            if (!(name.Contains("Sulfuras")))
            {
                if (name.Contains("Aged Brie"))
                {
                    if (SellIn <= 0) { quality = 0; }
                    else if (SellIn <= 5) { quality += 3; }
                    else if (SellIn <= 10) { quality += 2; }
                    else { quality += 1; }
                }
                else if (name.Contains("Conjured")) { quality -= 2; }
                else { quality -= 1; }
                if (quality > 50) { quality = 50; }
                if (quality < 0) { quality = 0; }
                SellIn -= 1;
            }
        }
        public void print_item()
        {
            string temp_string = (SellIn < 0) ? " uz mal byt predany " : " by sa mal predat do ";
            Console.WriteLine($"item z menom \"{name}\" {temp_string} {Math.Abs(SellIn)} dni, a jeho kvalita je {quality}");
        }
    }
}