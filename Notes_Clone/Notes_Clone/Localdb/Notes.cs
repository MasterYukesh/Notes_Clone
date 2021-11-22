using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Notes_Clone.Localdb
{
    public class Notes
    {
        [PrimaryKey,AutoIncrement]
        public int ID { get; set; }

        public string TITLE { get; set; }
        public string INFO { get; set; }
        public string DATE { get; set; }
        public string COLOR { get; set; }
    }
}
