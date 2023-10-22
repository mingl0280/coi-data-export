using System.Collections.Generic;
using Mafi.Core.Fleet;

namespace COIWorldMapChange
{
    public class ShipHull
    {
        public ShipHull(FleetEntityHullProto proto)
        {
            Id = proto.Id.Value;
            Name = proto.Strings.Name.ToString();
            HitChanceWeight = proto.HitChanceWeight;
            BattlePriority = proto.BattlePriority;
            ExtraRoundsToEscape = proto.ExtraRoundsToEscape;
            Costs = new List<Cost>();
        }
        public string Id { get; }
        public string Name { get; }
        public int HitChanceWeight { get; }
        public int BattlePriority { get; }
        public List<Cost> Costs { get; }
        public int ExtraRoundsToEscape { get; }

        
    }
}