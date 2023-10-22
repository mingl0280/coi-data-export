using System.Collections.Generic;

namespace COIWorldMapChange
{
    public class TerrainRoot
    {
        public string GameVersion { get; set; }
        public List<TerrainMaterial> TerrainMaterials { get; } = new List<TerrainMaterial>();
    }
}