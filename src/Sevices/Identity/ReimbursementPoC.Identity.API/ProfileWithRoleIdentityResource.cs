using IdentityModel;
using IdentityServer4.Models;

namespace ReimbursementPoC.Identity.API
{
    public class ProfileWithRoleIdentityResource : IdentityResources.Profile
    {
        public ProfileWithRoleIdentityResource()
        {
            this.UserClaims.Add(JwtClaimTypes.Role);
        }
    }
}
