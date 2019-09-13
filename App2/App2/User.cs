using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace App2
{
    [Table("User")]
    class User
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        

        public User()
        {

        }

        public User(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }

      
    }
}
