using Mafi.Core;
using Mafi.Core.Factory.Recipes;
using Newtonsoft.Json;

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
            Id = input.Product.Id.Value;
            Product = input.Product.Strings.Name.TranslatedString;
            Quantity = input.Quantity.Value;
        }

        public Cost(RecipeInput input)
        {
            Id = input.Product.Id.Value;
            Product = input.Product.Strings.Name.TranslatedString;
            Quantity = input.Quantity.Value;
        }

        public Cost(RecipeOutput input)
        {
            Id = input.Product.Id.Value;
            Product = input.Product.Strings.Name.TranslatedString;
            Quantity = input.Quantity.Value;
        }
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("product_name")]
        public string Product { get; }

        [JsonProperty("quantity")]
        public int Quantity { get; }
    }
}