using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace DailyHealthy.Models
{
    [Table("Daily")]
    class ConstantsModel
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public DateTime Datetime { get; set; }
        public string Name { get; set; }
        public int GLU_PC { get; set; }
        public int GLU_AC { get; set; }
        public string Description { get; set; }
        public string Path { get; set; }
    }
}
