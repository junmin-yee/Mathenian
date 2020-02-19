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
    }
}
