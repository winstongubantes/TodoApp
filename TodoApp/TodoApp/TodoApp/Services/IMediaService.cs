using System.Threading.Tasks;

namespace TodoApp.Services
{
    public interface IMediaService
    {
        Task<string> CapturePhotoAsync();
    }
}