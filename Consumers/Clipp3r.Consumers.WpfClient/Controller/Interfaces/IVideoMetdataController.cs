using Clipp3r.Core.Dtos;
using System;
using System.Threading.Tasks;

namespace Clipp3r.Consumers.WPF
{
    public interface IVideoMetdataController
    {
        Task<VideoMetadataDto> CreateVideoMetadataAsync(string videoFileName);
        Task<VideoMetadataDto> GetVideoMetadataAsync(string videoMomentName);
        Task UpdateLastClippedDateAsync(Guid videoMetadataGuid);
    }
}