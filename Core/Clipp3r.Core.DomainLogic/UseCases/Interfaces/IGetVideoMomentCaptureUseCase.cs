using Clipp3r.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clipp3r.Core.DomainLogic
{
    public interface IGetVideoMomentCaptureUseCase
    {
        Task<List<VideoMomentCaptureDto>> GetVideoMomentCaptureListAsync(Guid videoMetadataGuid);
    }
}