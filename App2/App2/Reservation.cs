using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace App2
{
    [Table ("Reservation")]
    class Reservation
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int UserInfo_Id { get; set; }
        public int Reservation_Id { get; set; }
        public int PersonNumber { get; set; }
        public int Resort { get; set; }
        public int SkiDays { get; set; }
        public int Prize { get; set; }

        public Reservation()
        {}

        public Reservation (int personNumber, int resort, int skiDays, int prize)
        {
            this.PersonNumber = personNumber;
            this.Resort = resort;
            this.SkiDays = skiDays;
            this.Prize = prize;

        }

 
    }
}
