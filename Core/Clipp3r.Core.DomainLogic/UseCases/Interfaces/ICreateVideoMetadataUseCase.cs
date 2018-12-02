using System.Threading.Tasks;
using Clipp3r.Core.Dtos;

namespace Clipp3r.Core.DomainLogic
{
    public interface ICreateVideoMetadataUseCase
    {
        Task<VideoMetadataDto> CreateVideoMetdataAsync(string videoFileName);
    }
}