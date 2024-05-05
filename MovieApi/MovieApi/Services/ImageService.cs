using MovieApi.Services.Interface;

namespace MovieApi.Services
{
    public class ImageService : IImageService
    {
        private static string imageFolderPath = "C:\\Users\\dimon\\OneDrive\\Рабочий стол\\Study\\Универ\\Курс 3\\Семестр 6\\БД\\Курсовая\\Data";
        public StreamContent GetImage(string path)
        {
            var fullPath = Path.Combine(imageFolderPath, path);
            StreamContent stream = null!;
            try
            {
                if (File.Exists(fullPath))
                {
                    stream = new StreamContent(File.OpenRead(fullPath));
                }else
                {
                    throw new Exception();
                }

            }catch (Exception e)
            { 
                stream = null!;
            }

            return stream;
        }
    }
}
