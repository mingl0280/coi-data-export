using Mafi.Core.Products;
using Newtonsoft.Json;

namespace COIDataExport
{
    public class TerrainMaterial
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("mined_product")]
        public string MinedProduct { get; set; }
        
        [JsonProperty("mined_quantity_per_tile_cubed")]
        public double MinedQuantityPerTileCubed { get; set; }

        [JsonProperty("disruption_recovery_time")]
        public int DisruptionRecoveryTime { get; set; }

        [JsonProperty("is_forest_floor")]
        public bool IsForestFloor { get; set; }

        [JsonProperty("is_farmable")]
        public bool IsFarmable { get; set; }

        [JsonProperty("max_collapse_height_diff")]
        public int MaxCollapseHeightDiff { get; set; }

        [JsonProperty("min_collapse_height_diff")]
        public double MinCollapseHeightDiff { get; set; }

        [JsonProperty("mined_quantity_mult")]
        public double MinedQuantityMult { get; set; }
        

        public TerrainMaterial(TerrainMaterialProto material)
        {
            Id = material.Id.Value;
            Name = material.Strings.Name.ToString();
            MinedProduct = material.MinedProduct.Id.Value;
            MinedQuantityPerTileCubed = material.MinedQuantityPerTileCubed.Value.ToDouble();
            DisruptionRecoveryTime = material.DisruptionRecoveryTime.Seconds.ToIntCeiled();
            IsForestFloor = material.IsForestFloor;
            IsFarmable = material.IsFarmable;
            MaxCollapseHeightDiff = material.MaxCollapseHeightDiff.Value.ToIntFloored();
            MinCollapseHeightDiff = material.MinCollapseHeightDiff.Value.ToIntCeiled();
            MinedQuantityMult = material.MinedQuantityMult.ToDouble();
        }
    }
}