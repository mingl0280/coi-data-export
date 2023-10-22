using System.Collections.Generic;

namespace COIWorldMapChange
{
    public class CategoryRoot
    {
        public Dictionary<string, CategoryType> Categories { get; set; } = new Dictionary<string, CategoryType>();
    }
}