using System.Collections.Generic;
using Mafi.Localization;
using Newtonsoft.Json;

namespace COIWorldMapChange
{
    public class CategoryType
    {
        public CategoryType()
        {
            Id = "";
            NameEng = "";
        }

        public CategoryType(string id)
        {
            Id = id;
            NameEng = LocalizationManager.LoadLocalizedString0(id).ToString();
        }

        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string NameEng { get; set; }
        [JsonProperty("machines")]
        public HashSet<string> Machines { get; set; } = new HashSet<string>();
        [JsonProperty("recipes")] 
        public HashSet<string> Recipes { get; set; } = new HashSet<string>();
    }
}