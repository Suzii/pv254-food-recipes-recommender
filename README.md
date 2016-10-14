# pv254-food-recipes-recommender
School project for course PV245 Recommender Systems (Masaryk University) for suggesting food recipes.

# Contribution guidelines
 - Basic stuff as for all .net / C# projects (highly recommend to install ReSharper - student licences available)
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

# Data
Project works with recipes from [BBC Food Website](http://www.bbc.co.uk/food/recipes). Recipes were extracted from this website using a tool [BBC Recipe Web Scraper](https://github.com/xvitovs1/BBC-Recipe-Web-Scraper), original version of this tool can be found [here](https://github.com/Thomas-Rudge/BBC-Recipe-Web-Scraper).

# Current status
Project currently contains two solutions
## RecipeParser
Simple console application for parsing HTML file containing recipe such as can be found in data/ folder. 
File should adhere to recipe schema.org in order to be parseable by the application.

The idea is to reuse the parser for bulk import of all 11.000 HTML files in order to seed the database.

## Recipes
Main solution - currently contains projects:

 - Recipes.DAL
Contains folders:
  - Entities:  classes to be persisted in database (Recipe, IngredientUsage, Ingredient)
  - Repositories: classes to serve as connection between application and DB (right now only very simple RecipesRepository.cs)

Plus Initializer and AppContext files.

 - Recipes.Service
Contains folders:
  - DTOs: for bussiness objects
  - Mappings: AutoMapper definitions
  - Stores: Interfaces and implementations of all business logic
 
 - Recipes.Web
Project for all web related stuff and app configuration.
Right now everything there is just boilerplate code + some addition configuration for AutoMapper and Castle Dependency Injection setup.
The idea is that in the latter stages of the project we will have a RESTful WebApi + single page app in react, but that's months away right now.
