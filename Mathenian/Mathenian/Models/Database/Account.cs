using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Mathenian.Models
{
    public class Account
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Completion { get; set; }
        public int TotalQuestionsCompleted { get; set; }
        public int TotalQuestionsAttempted { get; set; }
        public int DailyStreak { get; set; }
        public DateTime LastLoggedIn { get; set; }
        public int IsDarkMode { get; set; }
    }
}
