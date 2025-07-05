using ClassLibrary1;
namespace Project3
{
    /// <summary>
    /// Класс для работы с данными.
    /// </summary>
    public class WorkData
    {
        /// <summary>
        /// Массив посетителей.
        /// </summary>
        private List<Visitor> visitors = [];

        /// <summary>
        /// Метод для получения списка посетителей.
        /// </summary>
        /// <returns> Список посетителей. </returns>
        public List<Visitor> GetVisitors()
        {
            return visitors;
        }

        /// <summary>
        /// Создаёт список посетителей.
        /// </summary>
        /// <param name="json"> Файл json. </param>
        /// <exception cref="JsonErrorFormatExeption"> Неправильным форматом данных в Json. </exception>
        public void Creat_Visitors(string json)
        {
            if(!((Dictionary<string, object>) JsonParser.Parse(json)).ContainsKey("elements"))
            {
                throw new JsonErrorFormatExeption();
            }
            List<object> data = (List<object>)((Dictionary<string, object>) JsonParser.Parse(json))["elements"];
            visitors.Clear();
            foreach (object i in data)
            {
                visitors.Add(new Visitor((Dictionary<string, object>)i));
            }
        }

        /// <summary>
        /// Фильтрует данные.
        /// </summary>
        public void Filter()
        {
            while (true)
            {
                Console.WriteLine("Выберете по какому критерию фильтровать, введите название критерия:\n1) id\n2) label\n3) icon\n4) desc\n5) lifetime\n6) inherits\n" +
                "7) decayto\n8) dissatisfying\n9) comments");
                string name = Console.ReadLine();
                List<Visitor> copyVisitors = [];
                if(visitors[0].GetField(name) != null)
                {
                    Console.WriteLine("Введите через пробел подходящие параметры");
                    string[] values = Console.ReadLine().Split(" ");
                    foreach (Visitor i in visitors)
                    {
                        foreach (string v in values)
                        {
                            
                            if (name != "lifetime" && i.GetField(name) == "\"" +v + "\"")
                            {
                                copyVisitors.Add(i);
                                break;
                            }
                            if (name == "lifetime" && i.GetField(name) == v)
                            {
                                copyVisitors.Add(i);
                                break;
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Введите корректное имя");
                    continue;
                }
                visitors = copyVisitors;
                return;
            }
        }

        /// <summary>
        /// Сортирует данные.
        /// </summary>
        public void Sorting()
        {
            while (true)
            {
                Console.WriteLine("Выберете по какому критерию фильтровать, введите название критерия:\n1) id\n2) label\n3) icon\n4) desc\n5) lifetime\n6) inherits\n" +
                "7) decayto\n8) dissatisfying\n9) comments");
               string param = Console.ReadLine();
               if(visitors[0].GetField(param) != null) 
               {
                    visitors = [.. visitors.OrderBy(p => p.GetField(param))];
                    return;
               }
               else
               {
                    Console.WriteLine("Введите корректное имя.");
               }
            }
        }
    }
}
