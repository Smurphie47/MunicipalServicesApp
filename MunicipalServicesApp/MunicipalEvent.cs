
using System;

namespace MunicipalServicesApp
{
    public class MunicipalEvent
    {
        public int EventId { get; set; }

        public string Title { get; set; }

        public string Category { get; set; }

        public string Location { get; set; }

        public DateTime EventDate { get; set; }

        public string Description { get; set; }

        public string EventType { get; set; }
    }
}
