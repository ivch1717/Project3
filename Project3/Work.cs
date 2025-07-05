using ClassLibrary1;
namespace Project3
{
    /// <summary>
    /// Класс для работы с меню.
    /// </summary>
    public class Work
    {
        /// <summary>
        /// Делегат для метода обработки одного шага меню.
        /// </summary>
        /// <returns></returns>
        private delegate int IWork();

        /// <summary>
        /// Метод запуска работы и отлова ошибок.
        /// </summary>
        public void Invocation()
        {
            ErrorHandling(MainWork);
        }
        private WorkData WorkData { get; set; }
        /// <summary>
        /// Обработка одной итерации меню.
        /// </summary>
        private int MainWork()
        {
            Console.WriteLine("1. Ввести данные \n2. Отфильтровать данные\n3. Отсортировать данные\n" +
                 "4. Вывести список связей между поситителями\n5. Визуализировать связи между посетителями с помощью графа\n" +
                 "6. Вывести данные \n7. Выход");
            _ = int.TryParse(Console.ReadLine(), out int x);
            switch (x)
            {
                case 1:
                    WorkData.Creat_Visitors(WorkInputOutpur.DataReading());
                    break;
                case 2:
                    if (WorkData.GetVisitors().Count == 0)
                    {
                        Console.WriteLine("Вы ещё ничего не ввели");
                    }
                    else
                    {
                        WorkData.Filter();
                    }
                    break;
                case 3:
                    if (WorkData.GetVisitors().Count == 0)
                    {
                        Console.WriteLine("Вы ещё ничего не ввели");
                    }
                    else
                    {
                        WorkData.Sorting();
                    }
                    break;
                case 4:
                    if (WorkData.GetVisitors().Count == 0)
                    {
                        Console.WriteLine("Вы ещё ничего не ввели");
                    }
                    else
                    {
                        MainTask mainTask = new(WorkData.GetVisitors());
                        mainTask.Invocation();
                    }
                    break;
                case 5:
                    if (WorkData.GetVisitors().Count == 0)
                    {
                        Console.WriteLine("Вы ещё ничего не ввели");
                    }
                    else
                    {
                        MainTask mainTask2 = new(WorkData.GetVisitors());
                        DrawingGraphs pensel = new();
                        pensel.Drawing(mainTask2.GetFamiliar());
                    }
                    break;
                case 6:
                    if (WorkData.GetVisitors().Count == 0)
                    {
                        Console.WriteLine("Вы ещё ничего не ввели");
                    }
                    else
                    {
                        WorkInputOutpur.DataPrint(WorkData.GetVisitors());
                    }
                    break;
                case 7:
                    return 0;
                default:
                    Console.WriteLine("Введите корректное число от 1 до 7");
                    break;
            }
            return 1;
        }

        /// <summary>
        /// Обрабатывает все ошибки.
        /// </summary>
        /// <param name="mainWork"> Метод по работе меню. </param>
        private void ErrorHandling(IWork mainWork)
        {
            WorkData = new();
            while (true)
            {
                try
                {
                    if(mainWork() == 0)
                    {
                        break;
                    }
                }
                catch (Exception ex) when (ex is FormatException)
                {
                    Console.WriteLine("Ошибка формата данных");
                }
                catch (JsonErrorFormatExeption)
                {
                    Console.WriteLine("Json некоректного формата");
                }
                catch (KeyNotFoundException)
                {
                    Console.WriteLine("Введите корректные данные");
                }
                catch (Exception ex) when (ex is StackOverflowException or IndexOutOfRangeException)
                {
                    Console.WriteLine("Введите корректные данные");
                }
                catch (Exception ex) when (ex is IOException or FileNotFoundException or DirectoryNotFoundException or UnauthorizedAccessException)
                {
                    Console.WriteLine("Введите корректный путь к файлу");
                }
                catch (Exception ex) when (ex is ArgumentNullException or ArgumentException)
                {
                    Console.WriteLine("Введите корректные данные");
                }
                catch (System.InvalidCastException)
                {
                    Console.WriteLine("Json некоректного формата");
                }
                catch (NullReferenceException)
                {
                    Console.WriteLine("Введите корректные данные");
                }
            }
        }
    }
}
