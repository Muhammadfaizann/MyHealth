using System;

namespace MyHealthDB
{
    public class MediaCategory : DBEntityBase
    {
        public string CategoryTitle { get; set; }

        public DateTime LastUpdatedDate { get; set; }
    }
}