using System;
using System.Threading.Tasks;
using Clipp3r.Core.Entities;

namespace Clipp3r.Core.DomainLogic
{
    public interface IUpdateVideoMetadataProcess
    {
        Task<VideoMetadata> UpdateLastClippedDateAsync(Guid videoMetadataGuid);
    }
}