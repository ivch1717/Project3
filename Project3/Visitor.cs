using ClassLibrary1;
namespace Project3
{
    /// <summary>
    /// Класс посетителя.
    /// </summary>
    public struct Visitor: IJSONObject
    {
        private string Id { get; set; }
        private string Label { get; set; }
        private string Icon { get; set; }
        private string Desc { get; set; }
        private int Lifetime { get; set; }
        private string Inherits{  get; set; }
        private Dictionary<string, int> Aspects{  get; set; }
        private string Decayto { get; set; }
        private Dictionary<string, object> Xtriggers{  get; set; }
        private Dictionary<string, string> Appointing { get; set; }
        private string Dissatisfying { get; set; }
        private Dictionary<string, string> Xexts { get; set; }
        private string Comments { get; set; }

        /// <summary>
        /// Конструктор класса.
        /// </summary>
        /// <param name="data"> Все данные о посетители. </param>
        public Visitor(Dictionary<string, object> data)
        {
            foreach(string name in GetAllFields())
            {
                if (data.ContainsKey(name))
                {
                    SetField(name, JsonParser.ElementToJson(data[name]));
                }
                else
                {
                    SetField(name, default);
                }
            }
        }

        /// <summary>
        ///  Возвращает коллекцию строк, представляющую имена всех полей объекта JSON.
        /// </summary>
        /// <returns> коллекция строк, представляющую имена всех полей объекта JSON. </returns>
        public readonly IEnumerable<string> GetAllFields()
        {
            return ["id", "label", "icon", "desc", "lifetime", "inherits", "aspects", "decayto", "xtriggers", "appointing", "xexts", "dissatisfying", "comments"];
        }

        /// <summary>
        /// Возвращает значение поля с указанным именем fieldName в формате строки.В случае отсутствия поля с заданным именем, возвращает null.
        /// </summary>
        /// <param name="fieldName"> Имя поля. </param>
        /// <returns> Значения поля с именем fieldName. </returns>
        public readonly string? GetField(string value)
        {
            return value switch
            {
                "id" => $"\"{Id}\"",
                "label" => $"\"{Label}\"",
                "desc" => $"\"{Desc}\"",
                "icon" => $"\"{Icon}\"",
                "lifetime" => Lifetime.ToString(),
                "inherits" => $"\"{Inherits}\"",
                "aspects" => JsonParser.ElementToJson(Aspects.ToDictionary(kvp => (object)kvp.Key, kvp => (object)kvp.Value)),
                "decayto" => $"\"{Decayto}\"",
                "xtriggers" => JsonParser.ElementToJson(Xtriggers.ToDictionary(kvp => (object)kvp.Key, kvp => (object)kvp.Value)),
                "appointing" => JsonParser.ElementToJson(Appointing.ToDictionary(kvp => (object)kvp.Key, kvp => (object)kvp.Value)),
                "xexts" => JsonParser.ElementToJson(Xexts.ToDictionary(kvp => (object)kvp.Key, kvp => (object)kvp.Value)),
                "dissatisfying" => $"\"{Dissatisfying}\"",
                "comments" => $"\"{Comments}\"",
                _ => null
            };
        }

        /// <summary>
        /// Присваивает полю с именем fieldName значение value.
        /// </summary>
        /// <param name="fieldName"> Имя поля. </param>
        /// <param name="value"> Значения поля. </param>
        public void SetField(string fieldName, string value)
        {
            switch (fieldName)
            {
                case "id": Id = value != null ? value[1..^1] : ""; break;
                case "label": Label = value != null ? value[1..^1] : ""; break;
                case "desc": Desc = value != null ? value[1..^1] : ""; break;
                case "icon": Icon = value != null ? value[1..^1] : ""; break;
                case "lifetime": Lifetime = value != null ? int.Parse(value) : 0; break;
                case "inherits": Inherits = value != null ? value[1..^1] : ""; break;
                case "aspects":
                    Aspects = ((Dictionary<string, object>)JsonParser.Parse(value ?? "{}")).ToDictionary(kvp => kvp.Key, kvp => (int)kvp.Value);
                    break;
                case "decayto": Decayto = value != null ? value[1..^1] : ""; break;
                case "xtriggers":
                    Xtriggers = (Dictionary<string, object>)JsonParser.Parse(value ?? "{}");
                    break;
                case "appointing":
                    Appointing = ((Dictionary<string, object>)JsonParser.Parse(value ?? "{}")).ToDictionary(kvp => kvp.Key, kvp => (string)kvp.Value);
                    break;
                case "xexts":
                    Xexts = ((Dictionary<string, object>)JsonParser.Parse(value ?? "{}")).ToDictionary(kvp => kvp.Key, kvp => (string)kvp.Value);
                    break;
                case "dissatisfying": Dissatisfying = value != null ? value[1..^1] : ""; break;
                case "comments": Comments = value != null ? value[1..^1] : ""; break;
                default: throw new KeyNotFoundException();
            }
        }

        /// <summary>
        /// Приводит объект к формату json.
        /// </summary>
        /// <returns></returns>
        public override readonly string ToString()
        {
            string result = "{";
            foreach(string i in GetAllFields())
            {
                if(GetField(i) != "\"\"")
                {
                    result += $"\"{i}\" : {GetField(i)},\n";
                }
            }
            return result[..^2] + "}";
        }
    }
}
