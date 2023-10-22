using System.Collections.Generic;
using Newtonsoft.Json;

namespace COIWorldMapChange
{
    public class ShipUpgrades
    {
        [JsonProperty("game_version")] public string GameVersion { get; set; }

        public List<ShipHull> Hulls { get; } = new List<ShipHull>();
        public List<Engine> Engines { get; } = new List<Engine>();
        public List<Weapon> Weapons { get; } = new List<Weapon>();
        public List<Armor> Armor { get; } = new List<Armor>();
        public List<Bridge> Bridges { get; } = new List<Bridge>();
        public List<FuelTank> FuelTanks { get; } = new List<FuelTank>();
    }
}