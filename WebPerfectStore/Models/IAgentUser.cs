using System.Security.Principal;

namespace WebPerfectStore.Models
{
    public interface IAgentUser : IPrincipal
    {
        int FId { get; set; }
        string Name { get; set; }
    }
}