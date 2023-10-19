using System.Collections.Generic;
using Newtonsoft.Json;

namespace COIDataExport
{
    public class GameData
    {
        [JsonProperty("game_version")]
        public string GameVersion { get; set; }

        [JsonProperty("machines_and_buildings")]
        public List<MachineAndBuilding> MachinesAndBuildings { get; set; } = new List<MachineAndBuilding>();
    }
}