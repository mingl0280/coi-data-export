using System.Collections.Generic;
using System.Reflection;
using Mafi.Core.Fleet;

namespace COIDataExport
{
    public class Bridge
    {
        public Bridge(FleetBridgePartProto proto)
        {
            Id = proto.Id.Value;
            Name = proto.Strings.Name.ToString();
            HpUpgrade = ((UpgradableIntProto)typeof(FleetBridgePartProto)
                .GetField("m_hpUpgrade", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(proto)).BonusValue;
            RadarUpgrade = ((UpgradableIntProto)typeof(FleetBridgePartProto)
                .GetField("m_radarRange", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(proto)).BonusValue;
            ExtraCrewNeeded = proto.ExtraCrew.BonusValue;
            Costs = new List<Cost>();
        }
        public string Id { get; }
        public string Name { get; }
        public int HpUpgrade { get; }
        public int RadarUpgrade { get; }
        public int ExtraCrewNeeded { get; }
        public List<Cost> Costs { get; }
    }
}