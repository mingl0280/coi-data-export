using System;
using System.Linq;
using System.Reflection;
using System.Text;
using Mafi;
using Mafi.Base;
using Mafi.Core;
using Mafi.Core.Mods;
using Mafi.Core.Prototypes;
using Mafi.Core.Fleet;
using Mafi.Collections;
using Mafi.Core.Entities.Dynamic;
using System.IO;
using Mafi.Base.Prototypes.Machines.PowerGenerators;
using Mafi.Core.Buildings.Cargo;
using Mafi.Core.Buildings.Cargo.Modules;
using Mafi.Core.Buildings.ResearchLab;
using Mafi.Core.Buildings.Storages;
using Mafi.Core.Entities.Static.Layout;
using Mafi.Core.Factory.Machines;
using Mafi.Core.Products;
using Newtonsoft.Json;
using Mafi.Base.Prototypes.Machines;
using Mafi.Core.Entities.Static;

namespace COIDataExport
{
    public sealed class COIDataExport : DataOnlyMod, IMod
    {
        private static string _gameVersion = "";
        // Name of this mod. It will be eventually shown to the player.
        public override string Name => "COI Data Export";

        // Version, currently unused.
        public override int Version => 1;

        // Mod constructor that lists mod dependencies as parameters.
        // This guarantee that all listed mods will be loaded before this mod.
        // It is a good idea to depend on both `Mafi.Core.CoreMod` and `Mafi.Base.BaseMod`.
        public COIDataExport(CoreMod coreMod, BaseMod baseMod)
        {
            // You can use Log class for logging. These will be written to the log file
            // and can be also displayed in the in-game console with command `also_log_to_console`.
            Log.Info("COIDataExport: constructed.");
        }

        public override void RegisterPrototypes(ProtoRegistrator registrator)
        {
            Log.Info("COIDataExport: Register...");
        }

        private void PrintItems<T>(T target)
        {
            string name = $"{target.GetType().Name}: {target.GetType().FullName}";
            Log.Info(name);
            Log.Info("Members:");
            foreach (var member_info in target.GetType().GetMembers())
            {
                Log.Info(member_info.Name);
            }

            Log.Info("NestedTypes");
            foreach (var nested_type in target.GetType().GetNestedTypes())
            {
                Log.Info($"{nested_type.Name}: {nested_type.FullName}");
            }

            Log.Info("Non-static Fields");
            foreach (var field_info in target.GetType()
                         .GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            {
                Log.Info(
                    $"{field_info.Name}: {field_info.FieldType.Name} = {field_info.GetValue(field_info.FieldType)}");
            }

            Log.Info("Static Fields");
            foreach (var field_info in target.GetType()
                         .GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
            {
                Log.Info($"{field_info.Name}: {field_info.FieldType.Name} = {field_info.GetValue(null)}");
            }

            Log.Info("Properties");
            foreach (var prop_info in target.GetType()
                         .GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            {
                var value = prop_info.CanRead ? prop_info.GetValue(target) : "Unreadable";
                Log.Info($"{prop_info.Name}: {prop_info.PropertyType.Name} = {value}");
            }

            Log.Info("Static Properties");
            foreach (var prop_info in target.GetType()
                         .GetProperties(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
            {
                var value = prop_info.CanRead ? prop_info.GetValue(null) : "Unreadable";
                Log.Info($"{prop_info.Name}: {prop_info.PropertyType.Name} = {value}");
            }

            Log.Info("Public Methods");
            foreach (var method_info in target.GetType().GetMethods(BindingFlags.Public))
            {
                StringBuilder methodSignature = new StringBuilder();

                // Method return type
                methodSignature.Append($"{method_info.ReturnType.Name} ");

                // Method name
                if (method_info.IsGenericMethod)
                {
                    var genericArgs = method_info.GetGenericArguments();
                    var genericArgsString = string.Join(", ", genericArgs.Select(arg => arg.Name));
                    methodSignature.Append($"{method_info.Name}<{genericArgsString}>");
                }
                else
                {
                    methodSignature.Append(method_info.Name);
                }

                // Method parameters
                var paramsInfo = method_info.GetParameters();
                var paramsString = string.Join(", ",
                    paramsInfo.Select(param => $"{param.ParameterType.Name} {param.Name}"));
                methodSignature.Append($"({paramsString})");

                Log.Info(methodSignature.ToString());
            }
        }

        /*
         * -------------------------------------
         * Main Mod Code
         * -------------------------------------
         * The logic runs within the RegisterDepencies stage due to me not being able to get the code running correctly otherwise.
         * This feels like it might not be the right place for it, but it works so...
        */

        void IMod.RegisterDependencies(DependencyResolverBuilder depBuilder, ProtosDb protosDb, bool gameWasLoaded)
        {
            Log.Info("COIDataExport: Main Module Entry");
            _gameVersion = typeof(Mafi.Base.BaseMod).GetTypeInfo().Assembly.GetName().Version.ToString();

            GenerateShipUpgrades(protosDb);

            Log.Info("COIExport: Ships OK");

            VehiclesExportStruct vehicles_export = new VehiclesExportStruct() { GameVersion = _gameVersion };

            foreach (var vehicle_id in VehicleUtilities.GetAllVehicles())
            {
                var proto = protosDb.Get(vehicle_id);
                var entity = (DrivingEntityProto)proto.Value;
                Vehicle vehicle_item = new Vehicle(entity);

                foreach (var cost in ((DrivingEntityProto)proto.Value).CostToBuild.Products)
                {
                    vehicle_item.Costs.Add(new Cost(cost));
                }
                vehicles_export.Vehicles.Add(vehicle_item);
            }

            Log.Info("COIExport: Vehicles OK");
            WriteJson(JsonConvert.SerializeObject(vehicles_export, Formatting.Indented), "vehicles");

            GameData machines_data = new GameData() { GameVersion = _gameVersion };

            foreach (var machine_id in MachineUtilites.GetAllMachines())
            {
                MachineAndBuilding item = GetMachineAndBuildingInfo(protosDb, machine_id);
                machines_data.MachinesAndBuildings.Add(item);
            }

            Log.Info("COIExport: Machines OK");

            foreach (var all_item in MachineUtilites.GetAllItems<StaticEntityProto.ID>())
            {
                machines_data.MachinesAndBuildings.Add(GetMachineAndBuildingInfo(protosDb, all_item));
            }

            Log.Info("COIExport: Static Entities OK");

            foreach (var all_item in MachineUtilites.GetAllItems<CargoDepotProto.ID>())
            {
                machines_data.MachinesAndBuildings.Add(GetMachineAndBuildingInfo(protosDb, all_item));
            }

            Log.Info("COIExport: Cargo Depot OK");

            foreach (var all_item in MachineUtilites.GetAllItems<CargoDepotModuleProto.ID>())
            {
                machines_data.MachinesAndBuildings.Add(GetMachineAndBuildingInfo(protosDb, all_item));
            }
            Log.Info("COIExport: CargoDepotModuleProto OK");

            WriteJson(JsonConvert.SerializeObject(machines_data, Formatting.Indented), "machines_and_buildings");


            TerrainRoot terrains = new TerrainRoot() { GameVersion = _gameVersion };

            foreach (var terrain in protosDb.All<TerrainMaterialProto>())
            {
                var terrain_ent = terrain;
                Log.Info($"Terrain_ent == null: {terrain_ent}");
                terrains.TerrainMaterials.Add(new TerrainMaterial(terrain_ent));
            }
            Log.Info("Terrains OK");
            WriteJson(JsonConvert.SerializeObject(terrains, Formatting.Indented), "terrains");
        }

        private static MachineAndBuilding GetMachineAndBuildingInfo(ProtosDb protosDb, Proto.ID machine_id)
        {
            var proto = protosDb.Get(machine_id);
            var machine_proto = (StaticEntityProto)proto.Value;

            if (machine_proto == null)
            {
                return new MachineAndBuilding(machine_id.Value);
            }

            Log.Info($"machine: {machine_id.Value}, proto = {proto.Value.GetType().Name}");


            Log.Info("1");
            MachineAndBuilding item = new MachineAndBuilding(machine_proto);
            Log.Info("2");
            if (machine_proto.Graphics.GetType().IsAssignableFrom(typeof(LayoutEntityProto.Gfx)))
            {
                var g = (LayoutEntityProto)machine_proto;
                // Get the last category from the machine's Graphics.Categories list
                if (g.Graphics.Categories.IsNotEmpty)
                {
                    item.Category = g.Graphics.Categories[g.Graphics.Categories.Length - 1].Id
                        .Value;
                }
            }
            Log.Info($"Type: {machine_proto.GetType().Name}");
            try
            {
                var r = (MachineProto)machine_proto;
                foreach (var item_recipe in r.Recipes)
                {
                    item.Recipes.Add(new Recipe(item_recipe));
                }
            }
            catch
            {
                //ignored.
            }

            foreach (var entity_cost in machine_proto.Costs.Price.Products)
            {
                item.BuildCosts.Add(new Cost(entity_cost));
            }

            switch (proto.Value.GetType().Name)
            {
                case "ElectricityGeneratorFromMechPowerProto":
                    try
                    {
                        item.ElectricityGenerated = item.Recipes.SelectMany(r => r.Outputs).Max(o => o.Quantity);
                    }
                    catch
                    {
                        // ignored
                    }
                    break;
                case "MechPowerGeneratorFromProductProto":

                    break;
                case "SolarElectricityGeneratorProto":
                    var solar_item = proto.Value as SolarElectricityGeneratorProto;
                    if (solar_item != null)
                        item.ElectricityGenerated = solar_item.OutputElectricity.Value;
                    break;
                case "ElectricityGeneratorFromProductProto":
                    var egfp = proto.Value as ElectricityGeneratorFromProductProto;
                    if (egfp != null)
                        item.ElectricityGenerated = egfp.OutputElectricity.Value;
                    break;
                case "ResearchLabProto":
                    // (60 / item.DurationForRecipe.Seconds) * item.StepsPerRecipe
                    var lab_item = proto.Value as ResearchLabProto;
                    if (lab_item != null)
                        item.ResearchSpeed = (60 / lab_item.DurationForRecipe.Seconds *
                                              lab_item.StepsPerRecipe).ToDouble();
                    break;
            }

            if (proto.Value.GetType().IsAssignableFrom(typeof(StorageBaseProto)))
            {
                var storage_item = proto.Value as StorageBaseProto;
                if (storage_item != null)
                {
                    item.StorageCapacity = storage_item.Capacity.Value;
                    item.StorageTransferSpeed = (storage_item.TransferLimit.Value / storage_item.TransferLimitDuration.Seconds * 60).ToDouble();
                }
            }

            return item;
        }

        private static void GenerateShipUpgrades(ProtosDb protosDb)
        {
            ShipUpgrades ship_upgrades = new ShipUpgrades() { GameVersion = _gameVersion };

            foreach (FleetEntityPartProto.ID upgrade_id in FleetUtilities.GetAllEngines())
            {
                Option<FleetEnginePartProto> item = protosDb.Get<FleetEnginePartProto>(upgrade_id);

                var engine_item = new Engine(item.Value);

                foreach (ProductQuantity cost in item.Value.Value.Products)
                {
                    engine_item.Costs.Add(new Cost(cost));
                }
                ship_upgrades.Engines.Add(engine_item);
            }

            foreach (FleetEntityPartProto.ID upgrade_id in FleetUtilities.GetAllWeapons())
            {
                Option<FleetWeaponProto> item = protosDb.Get<FleetWeaponProto>(upgrade_id);
                var weapon_item = new Weapon(item.Value);

                foreach (ProductQuantity cost in item.Value.Value.Products)
                {
                    weapon_item.Costs.Add(new Cost(cost));
                }
                ship_upgrades.Weapons.Add(weapon_item);
            }


            foreach (FleetEntityPartProto.ID upgrade_id in FleetUtilities.GetAllArmor())
            {
                Option<UpgradeHullProto> item = protosDb.Get<UpgradeHullProto>(upgrade_id);
                var armor_item = new Armor(item.Value);

                foreach (ProductQuantity cost in item.Value.Value.Products)
                {
                    armor_item.Costs.Add(new Cost(cost));
                }
                ship_upgrades.Armor.Add(armor_item);

            }

            // For Bridges
            foreach (FleetEntityPartProto.ID upgrade_id in FleetUtilities.GetAllBridges())
            {
                Log.Info(upgrade_id.Value);
                Option<FleetBridgePartProto> item = protosDb.Get<FleetBridgePartProto>(upgrade_id);
                var bridge_item = new Bridge(item.Value);

                foreach (ProductQuantity cost in item.Value.Value.Products)
                {
                    bridge_item.Costs.Add(new Cost(cost));
                }
                ship_upgrades.Bridges.Add(bridge_item);

            }

            // For Fuel Tanks
            foreach (FleetEntityPartProto.ID upgrade_id in FleetUtilities.GetAllFuelTanks())
            {
                    Option<FleetFuelTankPartProto> item = protosDb.Get<FleetFuelTankPartProto>(upgrade_id);
                    var tank_item = new FuelTank(item.Value);

                    foreach (ProductQuantity cost in item.Value.Value.Products)
                    {
                        tank_item.Costs.Add(new Cost(cost));
                    }
                    ship_upgrades.FuelTanks.Add(tank_item);
            }

            foreach (var upgrade_id in FleetUtilities.GetAllHulls())
            {
                Option<FleetEntityHullProto> item = protosDb.Get<FleetEntityHullProto>(upgrade_id);
                var hull_item = new ShipHull(item.Value);

                foreach (ProductQuantity cost in item.Value.Value.Products)
                {
                    hull_item.Costs.Add(new Cost(cost));
                }
                ship_upgrades.Hulls.Add(hull_item);
            }

            var serialized_json = JsonConvert.SerializeObject(ship_upgrades, Formatting.Indented);
            WriteJson(serialized_json, "ship_upgrades");
        }

        static void WriteJson(string serialized_json, string file_name)
        {
            var appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var path = $"{appdata}\\Captain of Industry\\Mods\\COIDataExport\\{file_name}.json";
            using (StreamWriter writer =
                   new StreamWriter(new FileStream(path, FileMode.Create, FileAccess.ReadWrite, FileShare.Read)))
            {
                writer.Write(serialized_json);
                writer.Flush();
            }
        }
    }
}