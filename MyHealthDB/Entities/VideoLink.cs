using System;

namespace MyHealthDB
{
    public class VideoLink : DBEntityBase
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string UrlDisplayName { get; set; }
        public string Url { get; set; }

        public DateTime LastUpdatedDate { get; set; }

        public bool IsDeleted { get; set; }
    }
}
