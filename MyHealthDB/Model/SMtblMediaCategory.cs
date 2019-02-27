using System;

namespace MyHealthDB.Model
{
    public class SMtblMediaCategory
    {
        public int Id { get; set; }
        public string CategoryTitle { get; set; }

        public DateTime LastUpdatedDate { get; set; }

        public bool IsDeleted { get; set; }
    }
}