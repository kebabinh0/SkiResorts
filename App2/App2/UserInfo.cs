using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace App2
{
    [Table("UserInfo")]
    class UserInfo
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }

        public int User_Id { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Email { get; set; }

        public UserInfo()
        {

        }

        public UserInfo(string firstname, string lastname, string email)
        {

            this.First_Name = firstname;
            this.Last_Name = lastname;
            this.Email = email;
        }
        
    }
}
