using System.Collections.Generic;
using Mafi.Core.Entities.Static;
using Mafi.Core.Factory.Machines;
using Newtonsoft.Json;

namespace COIDataExport
{
    public class MachineAndBuilding
    {
        public MachineAndBuilding(string ID)
        {
            Id = ID;
            Name = ID;
            Workers = 0;
            MaintenanceCostUnits = "";
            MaintenanceCostQuantity = 0.0;
            ElectricityGenerated = 0;
            ElectricityConsumed = 0;
            ComputingConsumed = 0;
            ComputingGenerated = 0;
            StorageCapacity = 0;
            StorageTransferSpeed = 0;
            UnityCost = 0;
            ResearchSpeed = 0;
            BuildCosts = new List<Cost>();
            Recipes = new List<Recipe>();
        }

        public MachineAndBuilding(MachineProto proto)
        {
            Id = proto.Id.ToString();
            Name = proto.Strings.Name.ToString();
            Workers = proto.Costs.Workers;
            MaintenanceCostUnits = proto.Costs.Maintenance.Product.Id.Value;
            MaintenanceCostQuantity = proto.Costs.Maintenance.MaxMaintenancePerMonth.Value.ToDouble();
            ElectricityConsumed = proto.ConsumedPowerPerTick.Value;  // Default value
            ComputingConsumed = proto.ComputingConsumed.Value;    // Default value
            ComputingGenerated = 0;   // Default value
            StorageCapacity = 0;      // Default value
            StorageTransferSpeed = 0; // Default value
            UnityCost = 0;            // Default value
            ResearchSpeed = 0;        // Default value
            BuildCosts = new List<Cost>(); // To be filled
            Recipes = new List<Recipe>();  // To be filled
        }

        public MachineAndBuilding(StaticEntityProto proto)
        {
            Id = proto.Id.ToString();
            Name = proto.Strings.Name.ToString();
            Workers = proto.Costs.Workers;
            MaintenanceCostUnits = proto.Costs.Maintenance.Product.Id.Value;
            MaintenanceCostQuantity = proto.Costs.Maintenance.MaxMaintenancePerMonth.Value.ToDouble();
            ElectricityConsumed = 0;  // Default value
            ComputingConsumed = 0;    // Default value
            ComputingGenerated = 0;   // Default value
            StorageCapacity = 0;      // Default value
            StorageTransferSpeed = 0; // Default value
            UnityCost = 0;            // Default value
            ResearchSpeed = 0;        // Default value
            BuildCosts = new List<Cost>(); // To be filled
            Recipes = new List<Recipe>();  // To be filled
        }
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("category_loc_str")]
        public string CategoryLocalizedStr { get; set; }

        [JsonProperty("is_farm")]
        public bool IsFarm { get; set; } = false;

        [JsonProperty("is_storage")] 
        public bool IsStorage { get; set; } = false;

        [JsonProperty("is_mine")] 
        public bool IsMine { get; set; } = false;

        [JsonProperty("workers")]
        public int Workers { get; set; }

        [JsonProperty("maintenance_cost_units")]
        public string MaintenanceCostUnits { get; set; }

        [JsonProperty("maintenance_cost_quantity")]
        public double MaintenanceCostQuantity { get; set; }

        [JsonProperty("electricity_consumed")]
        public int ElectricityConsumed { get; set; }

        [JsonProperty("electricity_generated")]
        public int ElectricityGenerated { get; set; }

        [JsonProperty("computing_consumed")]
        public int ComputingConsumed { get; set; }

        [JsonProperty("computing_generated")]
        public int ComputingGenerated { get; set; }

        [JsonProperty("storage_capacity")]
        public int StorageCapacity { get; set; }

        [JsonProperty("transfer_speed_min")]
        public double StorageTransferSpeed { get; set; }

        [JsonProperty("unity_cost")]
        public double UnityCost { get; set; }

        [JsonProperty("research_speed")]
        public double ResearchSpeed { get; set; }

        [JsonProperty("build_costs")]
        public List<Cost> BuildCosts { get; set; }

        [JsonProperty("recipes")] public List<Recipe> Recipes { get; set; }
    }
}