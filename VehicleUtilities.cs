using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Mafi.Base;
using Mafi.Core.Entities.Dynamic;

namespace COIDataExport
{
    public class VehicleUtilities : GetAllFieldsUtilities
    {
        public static List<DynamicEntityProto.ID> GetAllVehicles()
        {
            var v_list = GetAllFieldsOfType<DynamicEntityProto.ID>(typeof(Ids.Vehicles));
            var sub_types = typeof(Ids.Vehicles).GetNestedTypes();
            foreach (var sub_type in sub_types)
            {
                var sub_type_fields = sub_type.GetFields(BindingFlags.Public | BindingFlags.Static);
                if (sub_type_fields.Any(pred => pred.Name.Contains("Id")))
                {
                    v_list.Add((DynamicEntityProto.ID)sub_type.GetField("Id", BindingFlags.Public | BindingFlags.Static).GetValue(sub_type));
                }
            }
            return v_list;
        }
    }
}