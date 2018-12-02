using Clipp3r.Core.Dtos;
using Clipp3r.Core.Entities;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Clipp3r.Core.DomainLogic
{
    public interface IGetVideoMetadataProcess
    {
        Task<VideoMetadataDto> GetVideoMetadataAsync(string videoFileName);
    }
}