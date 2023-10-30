using Newtonsoft.Json.Linq;

namespace CCP.Utilities
{
    public class UtilityClass
    {


        public List<string> GetDifferences(string oldObject, string newObject)
        {
            List<string> diff = new List<string>(); 

            JObject obj1 = JObject.Parse(oldObject);
            JObject obj2 = JObject.Parse(newObject);
            foreach(var property in obj1.Properties()) 
            {
                JToken? token1 = property.Value;
                JToken? token2 = obj2[property.Name];

                if(!JToken.DeepEquals(token1, token2))
                {
                    diff.Add(string.Format("{0} changed from {1} to {2}",property.Name, token1, token2));
                }
            }
            return diff;
        }

    }
}
