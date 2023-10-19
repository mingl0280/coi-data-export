using System.Collections.Generic;
using Mafi.Base;
using Mafi.Core.Entities.Static;
using Mafi.Core.Factory.Machines;
using Mafi.Core.Prototypes;

namespace COIDataExport
{
    public class MachineUtilites : GetAllFieldsUtilities
    {
        public static List<MachineProto.ID> GetAllMachines()
        {
            return GetAllFieldsOfTypeRecursive<MachineProto.ID>(typeof(Ids));
            //var v_list = GetAllFieldsOfType<MachineProto.ID>(typeof(Machines));
            //var b_list = GetAllFieldsOfType<MachineProto.ID>(typeof(Buildings));
            //v_list.AddRange(b_list);
            //return v_list;
        }

        public static List<StaticEntityProto.ID> GetAllStaticEntities()
        {
            return GetAllFieldsOfTypeRecursive<StaticEntityProto.ID>(typeof(Ids));
        }

        public static List<T> GetAllItems<T>()
        {
            return GetAllFieldsOfTypeRecursive<T>(typeof(Ids));
        }

        public static List<Proto.ID> GetAllTerrains()
        {
            return GetAllFieldsOfType<Proto.ID>(typeof(Ids.TerrainMaterials));
        }
    }
}