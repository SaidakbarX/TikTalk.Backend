using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using TikTalk.Backend.Application.Interfaces.Services;

namespace TikTalk.Backend.Application.Services;

public class CloudinaryFileStorageService : IFileStorageService
{
    private readonly Cloudinary _cloudinary;

    public CloudinaryFileStorageService(Cloudinary cloudinary)
    {
        _cloudinary = cloudinary;
    }

    public async Task<string> SaveFileAsync(Stream stream, string folderName, string fileName)
    {
        // Fayl turi (video yoki image) ni aniqlash
        var extension = Path.GetExtension(fileName).ToLower();

        RawUploadResult uploadResult;

        if (extension == ".mp4" || extension == ".mov" || extension == ".avi")
        {
            // 🎥 Video fayl
            var videoParams = new VideoUploadParams
            {
                File = new FileDescription(fileName, stream),
                Folder = folderName
            };

            uploadResult = await _cloudinary.UploadAsync(videoParams);
        }
        else
        {
            // 🖼 Image fayl
            var imageParams = new ImageUploadParams
            {
                File = new FileDescription(fileName, stream),
                Folder = folderName
            };

            uploadResult = await _cloudinary.UploadAsync(imageParams);
        }

        if (uploadResult.Error != null)
            throw new Exception($"Cloudinary upload failed: {uploadResult.Error.Message}");

        return uploadResult.SecureUrl.ToString();
    }
}