using System;
using System.Threading.Tasks;

namespace Clipp3r.Core.DomainLogic
{
    public interface IUpdateVideoMetadataUseCase
    {
        Task UpdateLastClippedDateAsync(Guid videoMetadataGuid);
    }
}