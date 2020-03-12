using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Mathenian.Models
{
    public class Score
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int Type { get; set; }
        public int Topic { get; set; }
        public int Mastery { get; set; }
        public int TotalCorrect { get; set; }
        public int TotalAttempt { get; set; }
    }
}
