using Mafi.Core;
using Mafi.Core.Factory.Recipes;

namespace COIDataExport
{
    public class Cost
    {
        public Cost()
        {
            Product = "";
            Quantity = 0;
        }

        public Cost(ProductQuantity input)
        {
            Product = input.Product.Id.Value;
            Quantity = input.Quantity.Value;
        }

        public Cost(RecipeInput input)
        {
            Product = input.Product.Id.Value;
            Quantity = input.Quantity.Value;
        }

        public Cost(RecipeOutput input)
        {
            Product = input.Product.Id.Value;
            Quantity = input.Quantity.Value;
        }

        public string Product { get; }
        public int Quantity { get; }
    }
}