using System;

namespace EIS.Models
{
    public static class DBHelpers
    {
        public static object ToDBNullOrDefault(this object obj)
        {
            return obj ?? DBNull.Value;
        }
    }
    public class Event
    {
        public int EventId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public bool AllDay { get; set; }
        public string TextColor { get; set; }
        public string BackgroundColor { get; set; }
        public string BorderColor { get; set; }
        public string DisplayOrder { get; set; }
    }
}
