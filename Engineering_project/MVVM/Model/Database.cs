using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engineering_project.MVVM.Model
{
    public class Database : DbContext
    {
        public DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(connectionString:
               "Server=localhost;Port=5432;User Id=postgres;Password=123;Database=Expert system;");
            base.OnConfiguring(optionsBuilder);
        }

    }

    [Table("users")]
    public class Users : DbContext
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("user_id")]
        public int user_id { get; internal set; }

        [Column("nickname")]
        public string Username { get; internal set; }

        [Column("password")]
        public string Password { get; internal set; }

        [Column("email")]
        public string Email { get; internal set; }

        [Column("is_admin")]
        public bool IsAdmin { get; internal set; }

        public Users(string username, string password, string email, bool isAdmin)
        {
            Username = username;
            Password = password;
            Email = email;
            IsAdmin = isAdmin;
        }
    }




}
