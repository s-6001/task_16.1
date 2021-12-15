using System;
using System.Text.Json;
using System.Text.Unicode;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using System.IO;

namespace task_16._1
{
    class Program
    {
        static void Main(string[] args)
        {
            int quantity = 5;   //задаем количетво товаров
            int[] codes = new int[quantity];    //массив с кодами товаров
            string[] names = new string[quantity];  // массив с названием товаром
            double[] prices = new double[quantity]; //массив с ценами товаров
            Product[] products = new Product[quantity]; //массив с продуктами
            
            for (int i = 0; i < quantity; i++)
            {
                bool mistake = true;    //ошибка?
                while (mistake == true) //пока mistake == true запрашиваем значение
                {
                    Console.WriteLine("Введите код товара {0}: ", i + 1);
                    try
                    {
                        codes[i] = Convert.ToInt32(Console.ReadLine()); //считывеам код товара
                        mistake = false;
                    }
                    catch
                    {
                        Console.WriteLine("Код товара должен быть в виде целого числа.");
                        mistake = true;
                    }
                }
                Console.WriteLine("Введите название товара {0}: ", i + 1);
                names[i] = Console.ReadLine();  //считывеам название товара
                mistake = true;
                while (mistake == true)
                {
                    Console.WriteLine("Введите цену товара {0}: ", i + 1);
                    try
                    {
                        prices[i] = Convert.ToDouble(Console.ReadLine());   //считывеам цену товара
                        mistake = false;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Не удалось распознать цену.");
                        mistake = true;
                    }
                }
                products[i] = new Product(codes[i], names[i], prices[i]);   //заполняем массив с продуктами
            }
            JsonSerializerOptions options = new JsonSerializerOptions() //задаем опции
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true
            };
            string jsonString = JsonSerializer.Serialize(products, options);    //сериализуем в переменную
            string path = "d:/Products.json";   //задаем путь
            if (File.Exists(path)==false) //Если файл не существует, то создаем его
            {
                File.Create(path).Close();
            }
            StreamWriter sw = new StreamWriter(path, false); //создаем экземпляр класса sw, перезаписываем файл
            sw.WriteLine(jsonString);   //записываем содержимое переменной в файл
            sw.Close(); //закрываем файл после записи
            Console.WriteLine("файл {0} сформирован.", path);
            Console.ReadKey();
        }
    }
    class Product
    {
        [JsonPropertyName("ProductCode")]
        public int ProductCode { get; set; }

        [JsonPropertyName("ProductName")]
        public string ProductName { get; set; }

        [JsonPropertyName("ProductPrice")]
        public double ProductPrice { get; set; }
        public Product(int c, string n, double p)
        {
            ProductCode = c;
            ProductName = n;
            ProductPrice = p;
        }
    }
}