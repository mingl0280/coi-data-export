using System.Collections.Generic;
using Mafi.Core.Fleet;

namespace COIDataExport
{
    public class Engine
    {
        public Engine(FleetEnginePartProto proto)
        {
            Id = proto.Id.Value;
            Name = proto.Strings.Name.ToString();
            FuelCapacity = proto.FuelCapacity.Value;
            ExtraCrewNeeded = proto.ExtraCrew.BonusValue;
            Costs = new List<Cost>();
        }
        public string Id { get; }
        public string Name { get; }
        public int FuelCapacity { get; }
        public int ExtraCrewNeeded { get; }
        public List<Cost> Costs { get; }
    }
}