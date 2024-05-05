using Microsoft.AspNetCore.Mvc;

namespace MovieApi.Services.Interface
{
    public interface IVideoService
    {
        public FileStreamResult GetVideo(string path);
    }
}
