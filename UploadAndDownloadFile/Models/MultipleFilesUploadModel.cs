namespace UploadAndDownloadFile.Models
{
    public class MultipleFilesUploadModel
    {
        public List<IFormFile> Files { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Author { get; set; }
    }
}
