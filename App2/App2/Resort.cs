using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace App2
{
    class Resort
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int Id_reservation { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }


        public Resort()
        {}


        public Resort(string name, string text)
        {
            this.Name = name;
            this.Text = text;
        }

    }
}
