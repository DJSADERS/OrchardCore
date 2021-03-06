using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OrchardCore.Environment.Extensions;
using OrchardCore.Recipes.Models;

namespace OrchardCore.Recipes.Services
{
    /// <summary>
    /// Finds recipes in the application content folder.
    /// </summary>
    public class ApplicationRecipeHarvester : IRecipeHarvester
    {
        private readonly IExtensionManager _extensionManager;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IOptions<RecipeHarvestingOptions> _recipeOptions;

        public ApplicationRecipeHarvester(IExtensionManager extensionManager,
            IHostingEnvironment hostingEnvironment,
            IOptions<RecipeHarvestingOptions> recipeOptions,
            ILogger<RecipeHarvester> logger)
        {
            _extensionManager = extensionManager;
            _hostingEnvironment = hostingEnvironment;
            _recipeOptions = recipeOptions;

            Logger = logger;
        }

        public IStringLocalizer T { get; set; }
        public ILogger Logger { get; set; }

        public Task<IEnumerable<RecipeDescriptor>> HarvestRecipesAsync()
        {
            return RecipeHarvester.HarvestRecipesAsync("Recipes", _recipeOptions.Value, _hostingEnvironment);
        }
    }
}