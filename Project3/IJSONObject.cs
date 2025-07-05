namespace Project3
{
    /// <summary>
    /// Интерфейс который описывает поведения пользователя.
    /// </summary>
    public interface IJSONObject
    {
        /// <summary>
        ///  Возвращает коллекцию строк, представляющую имена всех полей объекта JSON.
        /// </summary>
        /// <returns> коллекция строк, представляющую имена всех полей объекта JSON. </returns>
        public IEnumerable<string> GetAllFields();

        /// <summary>
        /// Возвращает значение поля с указанным именем fieldName в формате строки.В случае отсутствия поля с заданным именем, возвращает null.
        /// </summary>
        /// <param name="fieldName"> Имя поля. </param>
        /// <returns> Значения поля с именем fieldName. </returns>
        public string? GetField(string fieldName);

        /// <summary>
        /// Присваивает полю с именем fieldName значение value.
        /// </summary>
        /// <param name="fieldName"> Имя поля. </param>
        /// <param name="value"> Значения поля. </param>
        public void SetField(string fieldName, string value);
    }
}
