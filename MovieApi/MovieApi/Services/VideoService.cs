using Microsoft.AspNetCore.Mvc;
using MovieApi.Services.Interface;

namespace MovieApi.Services
{
    public class VideoService : IVideoService
    {
        private static string videoFolderPath = "C:\\Users\\dimon\\OneDrive\\Рабочий стол\\Study\\Универ\\Курс 3\\Семестр 6\\БД\\Курсовая\\Data\\";
        public FileStreamResult GetVideo(string path)
        {
            var videoPath = Path.Combine(videoFolderPath, path);

            try
            {
                if (File.Exists(videoPath))
                {
                    var stream = new FileStream(videoPath, FileMode.Open, FileAccess.Read, FileShare.Read);
                    return new FileStreamResult(stream, "video/mp4");

                }
                else
                {
                    throw new Exception();
                }

            }
            catch (Exception e)
            {
                return null!;
            }

        }
        private async void AddInHistory(int filmId, int userId)
        {
            //try
            //{
            //    var 
            //}
            //catch
            //{

            //}
        }
    }
}
