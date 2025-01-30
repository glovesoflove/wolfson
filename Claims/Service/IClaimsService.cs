using Claims;

namespace Claims.Service
{
    public interface IClaimsService
    {
        Task<IEnumerable<Parser.Department>> Extract();
        Task<IEnumerable<Parser.Department>> ExtractHierarchy();
    }
}
