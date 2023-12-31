﻿using System.Collections.Generic;
using Newtonsoft.Json;

namespace COIDataExport
{
    public class VehiclesExportStruct
    {
        [JsonProperty("game_version")]
        public string GameVersion { get; set; }

        [JsonProperty("vehicles")] 
        public List<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
    }
}