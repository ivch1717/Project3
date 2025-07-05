using SkiaSharp;
namespace Project3
{
    /// <summary>
    /// Класс который рисует и сохраянет граф со всеми связями посетителя.
    /// </summary>
    public class DrawingGraphs
    {
        /// <summary>
        /// Холст на котором рисуется граф.
        /// </summary>
        private SKBitmap Bitmap;

        /// <summary>
        /// Размер холста.
        /// </summary>
        private readonly int Width = 1000;

        /// <summary>
        /// Метод который рисует точку.
        /// </summary>
        /// <param name="x"> Коррдината х.</param>
        /// <param name="y"> Коррдината у.</param>
        /// <param name="name"> Как точка будет подписана. </param>
        private void DrawingPoint(int x, int y, string name)
        {
            using SKCanvas canvas = new(Bitmap);
            using SKPaint paint = new();
            paint.Color = SKColors.Purple;
            canvas.DrawCircle(new SKPoint(x, y), 7, paint);
            SKPaint textPaint = new()
            {
                TextSize = 15,
                TextAlign = SKTextAlign.Center,
                Typeface = SKTypeface.FromFamilyName("Times New Roman", SKFontStyle.Bold)
            };
            canvas.DrawText(name,x, y-10, textPaint);
        }

        /// <summary>
        /// Метод который рисует линию.
        /// </summary>
        /// <param name="x"> Коррдината х 1 точки.</param>
        /// <param name="y"> Коррдината у 1 точки.</param>
        /// <param name="x2"> Коррдината х 2 точки.</param>
        /// <param name="y2"> Коррдината у 2 точки.</param>
        /// <param name="isFriend"> true - если они дружат, иначе враждуют. </param>
        private void DrawingLine(int x, int y, int x2, int y2, bool isFriend)
        {
            using SKCanvas canvas = new(Bitmap);
            using SKPaint paint = new();
            paint.Color = isFriend ? SKColors.Green : SKColors.Red;
            paint.StrokeWidth = 3;
            canvas.DrawLine(x, y, x2, y2, paint);
        }

        /// <summary>
        /// Метод который сохраняет изображение.
        /// </summary>
        public void SaveImage()
        {
            Console.WriteLine("Введите путь до файла куда вы хоите сохранить изображение");
            string path = Console.ReadLine();
            if (!path.EndsWith(".png", StringComparison.OrdinalIgnoreCase))
            {
                path += ".png";
            }
            using FileStream stream = new(path, FileMode.Create, FileAccess.Write);
            using SKImage image = SKImage.FromBitmap(Bitmap);
            using SKData encodedImage = image.Encode(SKEncodedImageFormat.Png, 100);
            encodedImage.SaveTo(stream);
        }

        /// <summary>
        /// Основной метод который вызывает отрисовку и сохранение графа.
        /// </summary>
        /// <param name="communications"> Массив всех связей поситителя. </param>
        public void Drawing(List<List<string>> communications)
        {
            if(communications.Count == 0)
            {
                Console.WriteLine("Нет подходящих данных");
                return;
            }
            Bitmap = new SKBitmap(Width, Width);
            using SKCanvas canvas = new(Bitmap);
            canvas.Clear(SKColors.White);
            double periud = 2 * Math.PI / communications.Count, angle = 0;
            for (int i = 0; i < communications.Count; i++)
            {
                DrawingLine(Width/2, Width/2, (Width/2) + (int)(Math.Cos(angle) * ((Width/2)-200)), (Width/2)+ (int)(Math.Sin(angle) * ((Width/2)-200)), communications[i][0] == "befriend");
                DrawingPoint((Width / 2) + (int)(Math.Cos(angle) * ((Width / 2) - 200)), (Width / 2) + (int)(Math.Sin(angle) * ((Width / 2) - 200)), communications[i][2]);
                angle += periud;
            }
            DrawingPoint(Width/2, Width/2, communications[0][1]);
            SaveImage();
        }
    }
}
