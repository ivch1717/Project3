namespace ClassLibrary1
{
    /// <summary>
    /// Класс отвечающий за парсинг Json файла и обратный распарсинг.
    /// </summary>
    public static class JsonParser
    {
        /// <summary>
        /// Метод который начинает парсинг файла.
        /// </summary>
        /// <param name="json"> Json файл. </param>
        /// <returns> Объект являющийся распаршеным jsonом. </returns>
        public static object Parse(string json)
        {
            int index = 0;
            return ParseElement(json, ref index);
        }

        /// <summary>
        /// Парсит элемент, вызывая парсинг подходящего типа.
        /// </summary>
        /// <param name="json"> Данные из json.</param>
        /// <param name="index"> Индекс в файле. </param>
        /// <returns> Объект который является распаршеным элементом. </returns>
        /// <exception cref="JsonErrorFormatExeption"> Неправильным форматом данных в Json.</exception>
        private static object ParseElement(string json, ref int index)
        {
            SkipWhitespace(json, ref index);
            switch (json[index])
            {
                case '[':
                    index++;
                    return ParseArray(json, ref index);
                case '{':
                    index++;
                    return ParseDict(json, ref index);
                case '"':
                    index++;
                    return ParseString(json, ref index);
                default:
                    if (char.IsDigit(json[index]) || json[index] == '-')
                    {
                        if (json[index] == '-')
                        {
                            // Отрицательный числа.
                            index++;
                            return -1 * ParseNumber(json, ref index);
                        }
                        else
                        {
                            return ParseNumber(json, ref index);
                        }
                    }
                    throw new JsonErrorFormatExeption();

            }
        }

        /// <summary>
        /// Парсит массив.
        /// </summary>
        /// <param name="json"> Данные из json.</param>
        /// <param name="index"> Индекс в файле. </param>
        /// <returns> Распаршенный массив. </returns>
        /// <exception cref="JsonErrorFormatExeption"> Неправильным форматом данных в Json. </exception>
        private static List<object> ParseArray(string json, ref int index)
        {
            List<object> result = [];
            SkipWhitespace(json, ref index);
            while (index < json.Length)
            {
                if (json[index] == ']')
                {
                    index++;
                    return result;
                }
                else if (json[index] == ',')
                {
                    index++;
                }
                else
                {
                    result.Add(ParseElement(json, ref index));
                }
                SkipWhitespace(json, ref index);
            }
            throw new JsonErrorFormatExeption();
        }

        /// <summary>
        /// Парсит словарь.
        /// </summary>
        /// <param name="json"> Данные из json.</param>
        /// <param name="index"> Индекс в файле. </param>
        /// <returns> Распаршенный словарь. </returns>
        /// <exception cref="JsonErrorFormatExeption"> Неправильным форматом данных в Json.</exception>
        private static Dictionary<string, object> ParseDict(string json, ref int index)
        {
            Dictionary<string, object> result = [];
            bool isKey = true;
            string key = "";
            SkipWhitespace(json, ref index);
            while (index < json.Length)
            {
                if (json[index] == '}')
                {
                    index++;
                    return result;
                }
                if (json[index] == ',')
                {
                    index++;
                }
                else if (isKey)
                {
                    key = ((string)ParseElement(json, ref index)).ToLower();
                    isKey = false;
                    SkipWhitespace(json, ref index);
                    // Пропускает : если оно есть, иначе вызывает ошибку.
                    if (json[index] != ':')
                    {
                        throw new JsonErrorFormatExeption();
                    }
                    index++;
                }
                else
                {
                    result[key] = ParseElement(json, ref index);
                    isKey = true;
                }

                SkipWhitespace(json, ref index);
            }
            throw new JsonErrorFormatExeption();
        }

        /// <summary>
        /// Парсит строку.
        /// </summary>
        /// <param name="json"> Данные из json. </param>
        /// <param name="index"> Индекс в файле.</param>
        /// <returns> Распаршенная строка. </returns>
        /// <exception cref="JsonErrorFormatExeption"> Неправильным форматом данных в Json. </exception>
        private static string ParseString(string json, ref int index)
        {
            string result = "";
            while (index < json.Length)
            {
                if (json[index] == '"')
                {
                    index++;
                    return result;
                }
                result += json[index];
                index++;
            }
            throw new JsonErrorFormatExeption();
        }

        /// <summary>
        /// Парсит число.
        /// </summary>
        /// <param name="json"> Данные из json. </param>
        /// <param name="index"> Индекс в файле. </param>
        /// <returns> Распаршенное число. </returns>
        private static int ParseNumber(string json, ref int index)
        {
            int x = 0;
            while (index < json.Length && char.IsDigit(json[index]))
            {
                x *= 10;
                x += json[index] - '0';
                index++;
            }
            return x;
        }

        /// <summary>
        /// Пропускает все пробелы в файле.
        /// </summary>
        /// <param name="json"> Данные из json. </param>
        /// <param name="index"> Индекс в файле. </param>
        private static void SkipWhitespace(string json, ref int index)
        {
            while (index < json.Length && char.IsWhiteSpace(json[index]))
            {
                index++;
            }
        }

        /// <summary>
        /// Выводит данные.
        /// </summary>
        /// <param name="json"> Данные. </param>
        public static void WriteJson(string json)
        {
            Console.WriteLine(json);
        }

        /// <summary>
        /// Считывает файл.
        /// </summary>
        /// <param name="IsInputRedirected"> true-если считывается из файла, иначе из консоли.</param>
        /// <returns> Строку со считанным файлом. </returns>
        public static string ReadJson(bool IsInputRedirected)
        {
            if (IsInputRedirected)
            {
                return Console.In.ReadToEnd();
            }
            Console.WriteLine("В конце считывания введите строчку 'END'");
            string json = "";
            string s = Console.ReadLine();
            while (s is not null and not "END")
            {
                json += s;
                s = Console.ReadLine();
            }
            return json;
        }

        /// <summary>
        /// Приводит элент к json формату.
        /// </summary>
        /// <param name="x"> Элемент который надо привести к json формату. </param>
        /// <returns> Строку с json форматом. </returns>
        /// <exception cref="JsonErrorFormatExeption">  Неправильным форматом данных в Json. </exception>
        public static string ElementToJson(object x)
        {
            return x is string s
                ? "\u0022" +  s + "\u0022"
                : x is int v
                    ? v.ToString()
                    : x.GetType().IsGenericType && x.GetType().GetGenericTypeDefinition() == typeof(Dictionary<,>)
                                    ? x is Dictionary<object, object> dictionary
                                                    ? DictToJson(dictionary)
                                                    : DictToJson(((Dictionary<string, object>)x).ToDictionary(kvp => (object)kvp.Key, kvp => (object)kvp.Value))
                                    : x is List<object> d ? ListToJson(d) : throw new JsonErrorFormatExeption();
        }

        /// <summary>
        /// Приводит список к json формату.
        /// </summary>
        /// <param name="d"> Список объектов. </param>
        /// <returns> Список в json формате. </returns>
        private static string ListToJson(List<object> d)
        {
            string result = "[";
            for (int i = 0; i<d.Count; i++)
            {
                result += ElementToJson(d[i]);
                if (i != d.Count-1)
                {
                    result += ", ";
                }
            }
            return result + "]";
        }

        /// <summary>
        /// Приводит словарь к json формату.
        /// </summary>
        /// <param name="d"> Словарь объектов. </param>
        /// <returns> Слвоарь в json формате. </returns>
        private static string DictToJson(Dictionary<object, object> d)
        {
            string result = "{";
            List<object> keys = [.. d.Keys];
            for (int i = 0; i<keys.Count; i++)
            {
                if (d[keys[i]] != "")
                {
                    result += ElementToJson(keys[i]) + " : " + ElementToJson(d[keys[i]]);
                }
                if (i != d.Count-1)
                {
                    result += ", ";
                }
            }
            return result + "}";
        }
    }

}
