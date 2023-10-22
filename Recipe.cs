using System.Collections.Generic;
using Mafi.Core.Factory.Recipes;
using Newtonsoft.Json;

namespace COIDataExport
{
    public class Recipe
    {
        public Recipe(RecipeProto recipe)
        {
            Id = recipe.Id.Value;
            foreach (var recipe_all_input in recipe.AllUserVisibleInputs)
            {
                Inputs.Add(new Cost(recipe_all_input));
            }

            foreach (var recipe_all_output in recipe.AllUserVisibleOutputs)
            {
                Outputs.Add(new Cost(recipe_all_output));
            }

            Name = recipe.Strings.Name.ToString();
            Duration = recipe.Duration.Seconds.ToIntCeiled();

        }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("duration")]
        public int Duration { get; set; }

        [JsonProperty("inputs")]
        public List<Cost> Inputs { get; set; } = new List<Cost>();

        [JsonProperty("outputs")]
        public List<Cost> Outputs { get; set; } = new List<Cost>();
        
    }
}