using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace PracticeNa.Data // Or whatever your root namespace is
{
    public class IdentityDatabaseContext : IdentityDbContext<IdentityUser>
    {
        public IdentityDatabaseContext(DbContextOptions<IdentityDatabaseContext> options)
            : base(options)
        {
        }
    }
}
