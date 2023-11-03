using CCP.Data;
using CCP.Models;
using Newtonsoft.Json.Linq;
using System;

namespace CCP.Utilities
{
    public class UtilityClass
    {
        private CCPContext _context;
        public UtilityClass(CCPContext context)
        {
            _context = context;
        }

        /// <summary>
        /// This method compares two json objects and returns a list of diffrences
        /// </summary>
        /// <param name="oldObject"></param>
        /// <param name="newObject"></param>
        /// <returns></returns>

        public List<string> GetDifferences(string oldObject, string newObject)
        {
            List<Country> countries = _context.Country.ToList();
            List<string> diff = new List<string>();

            JObject obj1 = JObject.Parse(oldObject);
            JObject obj2 = JObject.Parse(newObject);
            foreach (var property in obj1.Properties())
            {
                // Skip comparison for $id and $ref fields
                if (property.Name == "$id" || property.Name == "$ref")
                    continue;

                JToken? token1 = property.Value;
                JToken? token2 = obj2[property.Name];

                if (!JToken.DeepEquals(token1, token2))
                {
                    if (property.Name == "CountryID")
                    {
                        // Convert IDs to country names
                        string oldCountryName = countries.FirstOrDefault(c => c.ID == token1.ToObject<int>())?.Name ?? "Unknown";
                        string newCountryName = countries.FirstOrDefault(c => c.ID == token2.ToObject<int>())?.Name ?? "Unknown";

                        diff.Add(string.Format("Country changed from {0} to {1}", oldCountryName, newCountryName));
                    }
                    else
                    {
                        diff.Add(string.Format("{0} changed from {1} to {2}", property.Name, token1, token2));
                    }
                }
            }
            return diff;
        }

    }
}
