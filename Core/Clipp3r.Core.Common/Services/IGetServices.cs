namespace Clipp3r.Core.Common
{
    public interface IGetServices
    {
        TService GetRequiredService<TService>();
    }
}
