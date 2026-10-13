
using System;
using System.Collections.Generic;
using System.Linq;

namespace MunicipalServicesApp
{
    public static class EventRepository
    {
        // Dictionary: retrieve events using their IDs.
        public static Dictionary<int, MunicipalEvent> EventsById
            = new Dictionary<int, MunicipalEvent>();

        // SortedDictionary: organise events by date.
        public static SortedDictionary<DateTime,
            List<MunicipalEvent>> EventsByDate
            = new SortedDictionary<DateTime,
                List<MunicipalEvent>>();

        // HashSet: store unique categories.
        public static HashSet<string> Categories
            = new HashSet<string>(
                StringComparer.OrdinalIgnoreCase);

        // Queue: process municipal announcements in FIFO order.
        public static Queue<MunicipalEvent> AnnouncementQueue
            = new Queue<MunicipalEvent>();

        static EventRepository()
        {
            LoadSampleEvents();
        }

        public static void AddEvent(MunicipalEvent municipalEvent)
        {
            if (municipalEvent == null)
            {
                throw new ArgumentNullException(
                    nameof(municipalEvent));
            }

            if (EventsById.ContainsKey(municipalEvent.EventId))
            {
                throw new ArgumentException(
                    "An event with this ID already exists.");
            }

            EventsById.Add(
                municipalEvent.EventId,
                municipalEvent);

            DateTime date = municipalEvent.EventDate.Date;

            if (!EventsByDate.ContainsKey(date))
            {
                EventsByDate[date] =
                    new List<MunicipalEvent>();
            }

            EventsByDate[date].Add(municipalEvent);

            Categories.Add(municipalEvent.Category);

            // Add announcements to the queue.
            if (string.Equals(
                municipalEvent.EventType,
                "Announcement",
                StringComparison.OrdinalIgnoreCase))
            {
                AnnouncementQueue.Enqueue(municipalEvent);
            }
        }

        // Return all events, sorted by date.
        public static List<MunicipalEvent> GetAllEvents()
        {
            return EventsByDate.Values
                .SelectMany(events => events)
                .OrderBy(e => e.EventDate)
                .ThenBy(e => e.Title)
                .ToList();
        }

        // Return the number of queued announcements.
        public static int GetAnnouncementCount()
        {
            return AnnouncementQueue.Count;
        }

        // Look at the next announcement without removing it.
        public static MunicipalEvent PeekNextAnnouncement()
        {
            if (AnnouncementQueue.Count == 0)
                return null;

            return AnnouncementQueue.Peek();
        }

        // Process the next announcement using FIFO.
        public static MunicipalEvent ProcessNextAnnouncement()
        {
            if (AnnouncementQueue.Count == 0)
                return null;

            // Remove the announcement at the front.
            MunicipalEvent announcement =
                AnnouncementQueue.Dequeue();

            // Move it to the back for future browsing.
            AnnouncementQueue.Enqueue(announcement);

            return announcement;
        }

        // Load fictional municipal test events.
        private static void LoadSampleEvents()
        {
            DateTime today = DateTime.Today;

            AddEvent(new MunicipalEvent
            {
                EventId = 1,
                Title = "Community Clean-Up Campaign",
                Category = "Environment",
                Location = "Soweto",
                EventDate = today.AddDays(5),
                Description =
                    "Residents are invited to participate " +
                    "in a community clean-up initiative.",
                EventType = "Event"
            });

            AddEvent(new MunicipalEvent
            {
                EventId = 2,
                Title = "Youth Skills Development Workshop",
                Category = "Education",
                Location = "Johannesburg",
                EventDate = today.AddDays(8),
                Description =
                    "A workshop focusing on digital skills " +
                    "and career development for young people.",
                EventType = "Event"
            });

            AddEvent(new MunicipalEvent
            {
                EventId = 3,
                Title = "Scheduled Water Maintenance",
                Category = "Service Delivery",
                Location = "Sandton",
                EventDate = today.AddDays(3),
                Description =
                    "Notice of planned water infrastructure " +
                    "maintenance in the area.",
                EventType = "Announcement"
            });

            AddEvent(new MunicipalEvent
            {
                EventId = 4,
                Title = "Municipal Career Expo",
                Category = "Employment",
                Location = "Roodepoort",
                EventDate = today.AddDays(12),
                Description =
                    "Career information and employment " +
                    "opportunities for residents.",
                EventType = "Event"
            });

            AddEvent(new MunicipalEvent
            {
                EventId = 5,
                Title = "Community Health Awareness Day",
                Category = "Health",
                Location = "Alexandra",
                EventDate = today.AddDays(15),
                Description =
                    "Community education and awareness " +
                    "about health and wellness.",
                EventType = "Event"
            });

            AddEvent(new MunicipalEvent
            {
                EventId = 6,
                Title = "Road Maintenance Notice",
                Category = "Service Delivery",
                Location = "Midrand",
                EventDate = today.AddDays(6),
                Description =
                    "Information about upcoming road " +
                    "maintenance and possible disruptions.",
                EventType = "Announcement"
            });
        }
    }
}
