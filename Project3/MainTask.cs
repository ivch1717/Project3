using ClassLibrary1;
namespace Project3
{
    /// <summary>
    /// Класс для решения основной задачи.
    /// </summary>
    /// <param name="visitors"> Список всех посетителей. </param>
    public class MainTask(List<Visitor> visitors)
    {
        /// <summary>
        /// Список всех посетителей.
        /// </summary>
        private readonly List<Visitor> Visitors = visitors;

        /// <summary>
        /// Метод который смотрит есть ли подходящие данные и вызывает основную работу.
        /// </summary>
        public void Invocation()
        {
            Dictionary<string, List<string[,]>> result = CreaeTable();
            if (result["befriend"].Count + result["mistrust"].Count == 0)
            {
                Console.WriteLine("Нет подходящих данных");
                return;
            }
            PrintTable(result);
        }

        /// <summary>
        /// Ищет все связи определённого посетителя.
        /// </summary>
        /// <returns> Массив с типом связи и именами 2 человек. </returns>
        public List<List<string>> GetFamiliar()
        {
            Console.WriteLine("Введите id посетителя");
            string id = Console.ReadLine();
            Dictionary<string, List<string[,]>> data = CreaeTable();
            List<List<string>> result = [];
            foreach (string i in data.Keys)
            {
                foreach (string[,] j in data[i])
                {
                    if(j[0, 0] == id)
                    {
                        result.Add([i, j[1, 0], j[1, 1]]);
                    }
                    else if(j[0, 1] == id)
                    {
                        result.Add([i, j[1, 1], j[1, 0]]);
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Ищет имя человека по его id.
        /// </summary>
        /// <param name="id"> Id посетителя. </param>
        /// <returns> Имя человека. </returns>
        private string FindId(string id)
        {
            foreach (Visitor visitor in Visitors)
            {
                if (visitor.GetField("id") == $"\"{id}\"")
                {
                    string label = visitor.GetField("label");
                    return label[1..^1];
                }
            }
            return id;
        }

        /// <summary>
        /// Создаёт таблицу связей.
        /// </summary>
        /// <returns> Словарь со всеми связями. </returns>
        private Dictionary<string, List<string[,]>> CreaeTable()
        {
            Dictionary<string, List<string[,]>> result = [];
            result["befriend"] = [];
            result["mistrust"] = [];
            foreach (Visitor obj in Visitors)
            {
                if (obj.GetField("xexts") == "")
                {
                    continue;
                }
                foreach (string key in ((Dictionary<string, object>) JsonParser.Parse(obj.GetField("xexts"))).Keys)
                {
                    string[] value = key.Split('.');
                    if (value.Length == 3)
                    {
                        string[,] f = new string[2, 2];
                        f[0, 0] = value[1];
                        f[0, 1] = value[2];
                        f[1, 0] = FindId(value[1]);
                        f[1, 1] = FindId(value[2]);
                        result[value[0]].Add(f);
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Выводит таблицу.
        /// </summary>
        /// <param name="data"> Данные которые нужно вывести. </param>
        private void PrintTable(Dictionary<string, List<string[,]>> data)
        {
            int maxSize = 0, index = 0;

            foreach (string[,] obj in data["befriend"])
            {
                maxSize = new[] { maxSize, obj[0, 0].Length, obj[1, 0].Length, obj[0, 1].Length, obj[1, 1].Length }.Max() ;
            }
            maxSize++;
            Console.WriteLine($"┌{new string('─', maxSize)}┬{new string('─', maxSize)}┬{new string('─', maxSize)}┐");
            Console.WriteLine($"│ Статус{new string(' ', maxSize-7)}│ ID посетителя{new string(' ', maxSize-14)}│ Имя посетителя{new string(' ', maxSize-15)}│▒");
            Console.WriteLine($"├{new string('─', maxSize)}┼{new string('─', maxSize)}┼{new string('─', maxSize)}┤▒");
            foreach (string[,] obj in data["befriend"])
            {
                index++;
                Console.WriteLine($"│ Дружба{new string(' ', maxSize-7)}│ {obj[0, 0]}{new string(' ', maxSize-1-obj[0, 0].Length)}│ {obj[1, 0]}{new string(' ', maxSize-1-obj[1, 0].Length)}│▒");
                Console.WriteLine($"│{new string(' ', maxSize)}│ {obj[0, 1]}{new string(' ', maxSize-1-obj[0, 1].Length)}│ {obj[1, 1]}{new string(' ', maxSize-1-obj[1, 1].Length)}│▒");
                if (index != data["befriend"].Count + data["mistrust"].Count)
                {
                    Console.WriteLine($"├{new string('─', maxSize)}┼{new string('─', maxSize)}┼{new string('─', maxSize)}┤▒");
                }
            }
            foreach (string[,] obj in data["mistrust"])
            {
                index++;
                Console.WriteLine($"│ Недолюбливает{new string(' ', maxSize-14)}│ {obj[0, 0]}{new string(' ', maxSize-1-obj[0, 0].Length)}│ {obj[1, 0]}{new string(' ', maxSize-1-obj[1, 0].Length)}│▒");
                Console.WriteLine($"│{new string(' ', maxSize)}│ {obj[0, 1]}{new string(' ', maxSize-1-obj[0, 1].Length)}│ {obj[1, 1]}{new string(' ', maxSize-1-obj[1, 1].Length)}│▒");
                if (index != data["befriend"].Count + data["mistrust"].Count)
                {
                    Console.WriteLine($"├{new string('─', maxSize)}┼{new string('─', maxSize)}┼{new string('─', maxSize)}┤▒");
                }
            }
            Console.WriteLine($"└{new string('─', maxSize)}┴{new string('─', maxSize)}┴{new string('─', maxSize)}┘▒");
            Console.WriteLine(" " + new string('▒', (maxSize*3) + 4));
        }
    }
}