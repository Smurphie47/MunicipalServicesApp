
using System;
using System.Collections.Generic;
using System.Linq;

namespace MunicipalServicesApp
{
    public static class RecommendationEngine
    {
        public static List<MunicipalEvent> GetRecommendations(
            IEnumerable<MunicipalEvent> allEvents,
            IDictionary<string, int> categoryPreferences,
            IEnumerable<string> recentSearches,
            int maximumResults = 5)
        {
            if (allEvents == null)
                throw new ArgumentNullException(nameof(allEvents));

            if (categoryPreferences == null)
                throw new ArgumentNullException(
                    nameof(categoryPreferences));

            if (recentSearches == null)
                throw new ArgumentNullException(
                    nameof(recentSearches));

            // Stack enumeration gives newest searches first.
            List<string> keywords = recentSearches
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .Take(10)
                .ToList();

            var rankedEvents = new List<
                KeyValuePair<MunicipalEvent, int>>();

            foreach (MunicipalEvent municipalEvent in allEvents)
            {
                // Recommend only current and upcoming events.
                if (municipalEvent.EventDate.Date < DateTime.Today)
                    continue;

                int score = 0;

                // 1. Category preference score.
                int frequency;

                if (municipalEvent.Category != null &&
                    categoryPreferences.TryGetValue(
                        municipalEvent.Category,
                        out frequency))
                {
                    score += frequency * 5;
                }

                // 2. Keyword relevance score.
                foreach (string keyword in keywords)
                {
                    if (ContainsText(
                        municipalEvent.Title, keyword))
                    {
                        score += 5;
                    }

                    if (ContainsText(
                        municipalEvent.Category, keyword))
                    {
                        score += 4;
                    }

                    if (ContainsText(
                        municipalEvent.Description, keyword))
                    {
                        score += 3;
                    }

                    if (ContainsText(
                        municipalEvent.Location, keyword))
                    {
                        score += 2;
                    }
                }

                // 3. Prioritise events happening soon.
                double daysUntilEvent =
                    (municipalEvent.EventDate.Date -
                     DateTime.Today).TotalDays;

                if (daysUntilEvent <= 7)
                {
                    score += 3;
                }
                else if (daysUntilEvent <= 14)
                {
                    score += 2;
                }
                else if (daysUntilEvent <= 30)
                {
                    score += 1;
                }

                rankedEvents.Add(
                    new KeyValuePair<MunicipalEvent, int>(
                        municipalEvent,
                        score));
            }

            // Sort by relevance score, highest first.
            // For equal scores, show the earliest event first.
            return rankedEvents
                .OrderByDescending(item => item.Value)
                .ThenBy(item => item.Key.EventDate)
                .ThenBy(item => item.Key.Title)
                .Take(Math.Max(0, maximumResults))
                .Select(item => item.Key)
                .ToList();
        }

        private static bool ContainsText(
            string source,
            string keyword)
        {
            if (string.IsNullOrWhiteSpace(source) ||
                string.IsNullOrWhiteSpace(keyword))
            {
                return false;
            }

            return source.IndexOf(
                keyword,
                StringComparison.OrdinalIgnoreCase) >= 0;
        }
    }
}
