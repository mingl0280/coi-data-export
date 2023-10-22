using System.Collections.Generic;
using Newtonsoft.Json;

namespace COIWorldMapChange
{
    public class GameData
    {
        [JsonProperty("game_version")]
        public string GameVersion { get; set; }

        [JsonProperty("machines_and_buildings")]
        public List<MachineAndBuilding> MachinesAndBuildings { get; set; } = new List<MachineAndBuilding>();
    }
}