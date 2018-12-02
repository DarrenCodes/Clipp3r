using System.Threading.Tasks;
using Clipp3r.Core.Dtos;

namespace Clipp3r.Core.DomainLogic
{
    public interface IGetVideoMetadataUseCase
    {
        Task<VideoMetadataDto> GetVideoMetadataAsync(string videoFileName);
    }
}