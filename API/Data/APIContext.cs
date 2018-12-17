using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace API.Models
{
    public class APIContext : DbContext
    {
        public APIContext (DbContextOptions<APIContext> options)
            : base(options)
        {
        }

        public DbSet<API.Models.ReturnText> ReturnText { get; set; }
    }
}
