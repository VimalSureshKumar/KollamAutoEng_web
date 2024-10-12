using Microsoft.AspNetCore.Identity;

namespace KollamAutoEng_web.Areas.Identity.Data
{
    public class ApplicationRole : IdentityRole
    {
        // Property to hold the role value
        public int RoleValue { get; set; }

        public ApplicationRole(string roleName, int roleValue) : base(roleName)
        {
            RoleValue = roleValue;
        }

        // Parameterless constructor for EF Core compatibility
        public ApplicationRole() : base() { }
    }
}
