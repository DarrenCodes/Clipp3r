using Clipp3r.Core.Dtos;
using Clipp3r.Core.Entities;

namespace Clipp3r.Core.DomainLogic
{
    public interface IAddVideoMomentProcess
    {
        VideoMoment AddVideoMoment(string videoMomentName);
    }
}