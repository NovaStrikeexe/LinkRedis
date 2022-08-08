using LinkApi.Models;

namespace LinkApi.Data
{
    public interface ILinkRepo
    {
        string PackLink(string stringLink);
        string UnpackLink(string linkGuid);
        string DeleteLink(string linkGuid);
        int CountNumbersOfLinks();
    }
}