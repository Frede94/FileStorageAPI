namespace FileStorageAPI.Services.Interefaces
{
    /// <summary> 
    /// Represents an uploaded file sent with a HTTP request. 
    /// </summary>
    public interface IFileForm
    {
        public interface IFormFile
        {
            /// <summary> 
            /// Gets the raw content of the uploaded file. 
            /// </summary> 
            Stream OpenReadStream(); 

            /// <summary> 
            /// Gets the name of the uploaded file, including its extension. 
            /// </summary> 
            string FileName { get; } 

            /// <summary> 
            /// Gets the content type of the uploaded file, as provided by the client. 
            /// </summary> 
            string ContentType { get; } 

            /// <summary> 
            /// Gets the length of the uploaded file, in bytes. 
            /// </summary> 
            long Length { get; } 
        }

    }
}
