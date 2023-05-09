namespace FileStorageAPI.Services.Interefaces
{
    /// <summary>
    /// A microservice that provides funtionality for uploading, downloading, and deleting files.
    /// </summary>
    public interface IFileStorage
    {
        /// <summary> 
        /// Uploads a file to the specified location. 
        /// </summary> 
        /// <param name "file">The file to upload.</param> 
        /// <param name "location">The location where the file should be stored.</param> 
        /// <returns>A unique identifier for the uploaded file </returns> 
        Task<string> UploadFileAsync(IFormFile file, string location); 

        /// <summary> 
        /// Downloads a file with the specified identifier. 
        /// </summary> 
        /// <param name "fileId">The unique identifier of the file to download.</param> 
        /// <returns>A stream containing the contents of the downloaded file.</returns> 
        /// <remarks> 
        /// This method can be used to download a file that was previously uploaded 
        /// via <see cref="UploadFileAsync"/> by another user or from another service. 
        /// </remarks> 
        Task<Stream> DownloadFileAsync(string fileId); 

        /// <summary> 
        /// Deletes a file with the specified identifier. 
        /// </summary> 
        /// <param name="fileId">The unique identifier of the file to delete.</param> 
        /// <returns>A boolean indicating whether the file was successfully deleted.</returns> 
        Task<bool> DeleteFileAsync(string fileId); 

    }
}
