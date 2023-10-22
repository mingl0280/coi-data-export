using System.Collections.Generic;
using Mafi.Core.Fleet;

namespace COIWorldMapChange
{
    public class Weapon
    {
        public Weapon(FleetWeaponProto proto)
        {
            Id = proto.Id.Value;
            Name = proto.Strings.Name.ToString();
            Range = proto.Range;
            Damage = proto.Damage;
            ExtraCrewNeeded = proto.ExtraCrew.BonusValue;
            Costs = new List<Cost>();
        }
        public string Id { get; }
        public string Name { get; }
        public int Range { get; }
        public int Damage { get; }
        public int ExtraCrewNeeded { get; }
        public List<Cost> Costs { get; }
    }
}