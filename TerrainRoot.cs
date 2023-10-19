using System.Collections.Generic;

namespace COIDataExport
{
    public class TerrainRoot
    {
        public string GameVersion { get; set; }
        public List<TerrainMaterial> TerrainMaterials { get; } = new List<TerrainMaterial>();
    }
}