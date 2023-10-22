using System.Collections.Generic;
using Mafi.Core.Entities.Dynamic;
using Newtonsoft.Json;

namespace COIWorldMapChange
{
    public class Vehicle
    {
        public Vehicle(DrivingEntityProto proto)
        {
            Id = proto.Id.Value;
            Name = proto.Strings.Name.ToString();
            Costs = new List<Cost>();
        }
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("costs")]
        public List<Cost> Costs { get; set; }
    }
}