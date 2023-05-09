using FileStorageAPI.Services.Interefaces;

public class FileForm : IFileForm
{
    private readonly IFormFile _formFile;

    public FileForm(IFormFile formFile)
    {
        _formFile = formFile;
    }

    public string FileName => _formFile.FileName;

    public string ContentType => _formFile.ContentType;

    public long Length => _formFile.Length;

    public Stream OpenReadStream()
    {
        return _formFile.OpenReadStream();
    }
}