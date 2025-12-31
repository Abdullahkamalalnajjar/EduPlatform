using Microsoft.AspNetCore.Http;

namespace Project.Service.Abstracts
{
    public interface IFileService
    {
        public Task<string> UploadImage(string Location, IFormFile file);
        public Task<string> UploadFile(string Location, IFormFile file);
    }
}