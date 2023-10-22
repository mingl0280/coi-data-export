using System.Collections.Generic;
using System.Reflection;
using Mafi.Core.Fleet;

namespace COIWorldMapChange
{
    public class Armor
    {
        public Armor(UpgradeHullProto proto)
        {
            Id = proto.Id.Value;
            Name = proto.Strings.Name.ToString();
            HpUpgrade = ((UpgradableIntProto)typeof(UpgradeHullProto)
                .GetField("m_hpUpgrade", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(proto)).BonusValue;
            ArmorUpgrade = ((UpgradableIntProto)typeof(UpgradeHullProto)
                .GetField("m_armorUpgrade", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(proto)).BonusValue;
            Costs = new List<Cost>();
        }
        public string Id { get; }
        public string Name { get; }
        public int HpUpgrade { get; }
        public int ArmorUpgrade { get; }
        public List<Cost> Costs { get; }
    }
}