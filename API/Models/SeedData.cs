using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new APIContext(
                serviceProvider.GetRequiredService<DbContextOptions<APIContext>>()))
            {
                // Look for any movies.
                if (context.ReturnText.Count() > 0)
                {
                    return;   // DB has been seeded
                }

                context.ReturnText.AddRange(
                    new ReturnText
                    {
                        Text = "You are at rank 1"
                    }


                );
                context.SaveChanges();
            }
        }
    }
}
