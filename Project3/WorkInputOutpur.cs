using ClassLibrary1;
namespace Project3
{
    /// <summary>
    /// Класс для работы с данными.
    /// </summary>
    public static class WorkInputOutpur
    {
        /// <summary>
        /// Метод который считывает данные из файла.
        /// </summary>
        /// <returns> Строку с данными. </returns>
        public static string DataReading()
        {
            while (true)
            {
                Console.WriteLine("Откуда вы хотите считать данные?\n1. Консоли\n2. Файла");
                _ = int.TryParse(Console.ReadLine(), out int x);
                string json;
                if (x==1)
                {
                    json = JsonParser.ReadJson(false);
                    return json;
                }
                else if (x == 2)
                {
                    Console.WriteLine("Введите путь до файла");
                    string path = Console.ReadLine();
                    using StreamReader lg = new(path);
                    Console.SetIn(lg);
                    json = JsonParser.ReadJson(true);
                    lg.Close();
                    Console.SetIn(new StreamReader(Console.OpenStandardInput()));
                    Console.OutputEncoding = System.Text.Encoding.UTF8;
                    return json;
                }
                else
                {
                    Console.WriteLine("Введите корректное число 1 или 2");
                }
            }
        }

        /// <summary>
        /// Выводит данные.
        /// </summary>
        /// <param name="visitors"> Список всех посетителей. </param>
        public static void DataPrint(List<Visitor> visitors)
        {
            while (true)
            {
                Console.WriteLine("Куда вы хотите вывести данные?\n1. В консоль\n2. В файл");
                _ = int.TryParse(Console.ReadLine(), out int x);
                if (x==1)
                {
                    Print(visitors);
                    return;
                }
                else if (x == 2)
                {
                    Console.WriteLine("Введите путь до файла");
                    string path = Console.ReadLine();
                    TextWriter originalOutput = Console.Out;
                    using StreamWriter lg = new(path);
                    Console.SetOut(lg);
                    Print(visitors);
                    lg.Close();
                    Console.SetOut(originalOutput);
                    Console.OutputEncoding = System.Text.Encoding.UTF8;
                    return;
                }
                else
                {
                    Console.WriteLine("Введите корректное число 1 или 2");
                }
            }
        }

        /// <summary>
        /// Создаёт строку с пользователями формата json.
        /// </summary>
        /// <param name="visitors"> Список пользователей. </param>
        private static void Print(List<Visitor> visitors)
        {
            string result = "{";
            for (int i = 0; i < visitors.Count; i++)
            {
                result += visitors[i].ToString();
                if (i != visitors.Count - 1)
                {
                    result += ",\n";
                }
            }
            JsonParser.WriteJson(result + "}");
        }
    }
}
