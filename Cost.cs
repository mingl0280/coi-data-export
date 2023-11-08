using System.Collections.Generic;
using System.IO;
using Mafi.Core;
using Mafi.Core.Factory.Recipes;
using Mafi.Core.Products;
using Mafi.Unity;
using Mafi.Unity.Utils;
using UnityEngine;
using UnityEditor;
using Newtonsoft.Json;
using Mafi;

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
            StoreImage(input.Product);
            Quantity = input.Quantity.Value;
        }

        private void StoreImage(ProductProto proto)
        {
            if (!ExtMethods.CreatedImgs.Contains($"{proto.Id.Value}.png"))
            {
                IconProvider provider = new IconProvider(ExtMethods.GetMainInstance().AssetsDb);
                var go = provider.GetProductIconPooled(proto);
                go.SaveGameObjectToPng($"assets/products/{proto.Id.Value}.png");
                provider.ReturnIconToPool(ref go);
                ExtMethods.CreatedImgs.Add($"{proto.Id.Value}.png");
            }
        }

        public Cost(RecipeInput input)
        {
            Id = input.Product.Id.Value;
            Product = input.Product.Strings.Name.TranslatedString;
            StoreImage(input.Product);
            Quantity = input.Quantity.Value;
        }

        public Cost(RecipeOutput input)
        {
            Id = input.Product.Id.Value;
            Product = input.Product.Strings.Name.TranslatedString;
            StoreImage(input.Product);
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