using Sellify.Application.Models.ImageManagement;

namespace Sellify.Application.Contracts.Infrasctructure;

public interface IManageImageService
{
    Task<ImageResponse> UploadImage(ImageData imageStream);
}