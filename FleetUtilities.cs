using System.CodeDom;
using System.Collections.Generic;
using Mafi.Base.Prototypes.Machines.PowerGenerators;
using Mafi.Core.Fleet;
using Mafi.Core.Vehicles.Jobs;
using static Mafi.Base.Ids;

namespace COIWorldMapChange
{
    public class FleetUtilities : GetAllFieldsUtilities
    {
        public static List<FleetEntityHullProto.ID> GetAllHulls()
        {
            return GetAllFieldsOfType<FleetEntityHullProto.ID>(typeof(Fleet.Hulls));
        }

        public static List<FleetWeaponProto.ID> GetAllEngines()
        {
            return GetAllFieldsOfType<FleetWeaponProto.ID>(typeof(Fleet.Engines));
        }
        public static List<FleetWeaponProto.ID> GetAllArmor()
        {
            return GetAllFieldsOfType<FleetWeaponProto.ID>(typeof(Fleet.Armor));
        }

        public static List<FleetWeaponProto.ID> GetAllBridges()
        {
            return GetAllFieldsOfType<FleetWeaponProto.ID>(typeof(Fleet.Bridges));
        }

        public static List<FleetWeaponProto.ID> GetAllFuelTanks()
        {
            return GetAllFieldsOfType<FleetWeaponProto.ID>(typeof(Fleet.FuelTanks));
        }

        public static List<FleetWeaponProto.ID> GetAllWeapons()
        {
            return GetAllFieldsOfType<FleetWeaponProto.ID>(typeof(Fleet.Weapons));
        }

    }

}