using System.Collections.Generic;
using Mafi.Core.Fleet;

namespace COIDataExport
{
    public class FuelTank
    {
        public FuelTank(FleetFuelTankPartProto proto)
        {
            Id = proto.Id.Value;
            Name = proto.Strings.Name.ToString();
            AddedCapacity = proto.AddedFuelCapacity.Value;
            Costs = new List<Cost>();
        }
        public string Id { get; }
        public string Name { get; }
        public int AddedCapacity { get; }
        public List<Cost> Costs { get; }
    }
}