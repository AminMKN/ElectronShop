using _01_Framework.Application;

namespace ServiceHost
{
    public class FileUploader : IFileUploader
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileUploader(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public string Upload(IFormFile file, string path)
        {
            if (file != null && path != null)
            {
                var directoryPath = $"{_webHostEnvironment.WebRootPath}/Pictures/{path}";
                if (!Directory.Exists(directoryPath))
                    Directory.CreateDirectory(directoryPath);

                var fileName = $"{DateTime.Now.ToFileName()}-{file.FileName}";
                var filePath = $"{directoryPath}/{fileName}";
                using var output = File.Create(filePath);
                file.CopyTo(output);
                return $"{path}/{fileName}";
            }

            return string.Empty;
        }
    }
}
