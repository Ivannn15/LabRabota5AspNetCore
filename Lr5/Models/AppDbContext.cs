using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;


namespace Lr5.Models
{
    public class AppDbContext : DbContext
    {
        // Строка подключения
        private const string ConnectionString = "server=localhost;user=root;password=root;database=EfCoreDb;";


        // настройка параметров бд для подключения MySql
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        

        // Список сущностей таблицы книги включающий авторов
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }

        ////// Настройка отображения в базу данных
    }
}
