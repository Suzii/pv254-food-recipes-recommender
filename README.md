# pv254-food-recipes-recommender
School project for course PV245 Recommender Systems (Masaryk University) for suggesting food recipes.

Application is running on http://recipes-recommender.azurewebsites.net.

# Data
Project works with recipes from [BBC Food Website](http://www.bbc.co.uk/food/recipes). Recipes were extracted from this website using a tool [BBC Recipe Web Scraper](https://github.com/xvitovs1/BBC-Recipe-Web-Scraper), original version of this tool can be found [here](https://github.com/Thomas-Rudge/BBC-Recipe-Web-Scraper).

# Contribution guidelines
 - Basic stuff as for all .net / C# projects (highly recommend to install ReSharper - student licences available)
 - Keep the project structure nice and tidy
 - Use constructor **Dependency Injection** - castle configuration is set up for all `Recipes.Service` classes that will be found in `Implementation` folder and all `Recipes.DAL` classes that have `Repository` in their name
 - Use **AutoMapper** - whenever you want to map one entity to another (e.g `DAL.Entity` to `Service.DTOs`) inject `IMapper` via constructor (Castle already set up) and then map it like this
```
public async Task<Recipe> GetRecipeAsync(int id)
{
    var recipe = await _recipesRepository.GetFullRecipeAsync(id);
    var recipeDto = _mapper.Map<DTOs.Recipe>(recipe);

    return recipeDto;
}
```

# How to run the site for the first time
1. git clone https://github.com/Suzii/pv254-food-recipes-recommender
2. Open in VS2015
3. Hit F5
4. Redirect to localhost:{port}/api/recipes ... you should see ugly xml containing 10-minutes-pizza recipe, if this is not the case probably there is some problem with DB (see. 5)
5. Open *Server Explorer* in VS, click on *DataConnections*
6. If you do not see any connections, right-click *DataConnections* -> *Add connection*
7. Enter *Database file name (new or existing)* -> **%repositoryLocation%\pv254-food-recipes-recommender\src\Recipes\Recipes.Web\App_Data\RecipesDB.mdf**
8. You should be asked if the file should be created, click Yes
9. goto: step 3
