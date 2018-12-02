using Clipp3r.Core.Entities;

namespace Clipp3r.Core.DomainLogic
{
    public interface IAddVideoMetadataProcess
    {
        VideoMetadata AddVideoMetadata(string videoFileName);
    }
}