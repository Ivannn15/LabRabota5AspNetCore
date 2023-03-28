using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Lr5.Models
{
    public class MyViewModel
    {
        public Book Book { get; set; }
        public DbSet<Author> Authors{ get; set; }
    }
}
