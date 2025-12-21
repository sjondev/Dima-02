using Microsoft.AspNetCore.Identity;

namespace Dima.Api.Models;

public class User : IdentityUser<long>
{
    // Claims -> Afirmações
    // Policy
    public List<IdentityRole<long>>? Roles { get; set; }
}