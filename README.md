# pv254-food-recipes-recommender
School project for course PV245 Recommender Systems (Masaryk University) for suggesting food recipes.

# Running last stable React app
Simply hit `F5` in VisualStudio whete you have `Recipes.sln` open. This launches the browser and you should see the homepage of React single page application. This is the last version that was "manually deployed".

To actually change something in client application do the following:

1. Change what you need in `WebApp/recipes-app`
2. Test your change by running app in development mode (see next section)
3. Run `npm run build` command in this folder
4. Manually copy conents of generated `src\WebApp\recipes-app\build` folder to the root of `src\Recipes\Recipes.Web` project. Namely:
 - copy contents of `img` folder to `img` folder
 - copy contents of `static` folder to `static` folder
 - copy contents of `index.html` file to `Views\Home\Index.cshtml` file
5. Try out your solution by hitting `F5`

# Running React app in development mode
1. Install [Node.js](https://nodejs.org/en/download/)
2. Run the .net application (Recipes.sln) simply by hitting F5 in VisualStudio
3. Check out the port that this .net application is currently running at
4. Open [{projectLocation}/pv254-food-recipes-recommender/src/WebApp/recipes-app/package.json](https://github.com/Suzii/pv254-food-recipes-recommender/blob/master/src/WebApp/recipes-app/package.json) and change the last line `"proxy": "http://localhost:13451"` to the actual port
5. Open command line or bash and run these commands:
 - `cd {projectLocation}/pv254-food-recipes-recommender/src/WebApp/recipes-app`
 - `npm install` (this may take a few minutes, as it will download all javascript dependencies)
 - `npm start`
6. Browser should open the `localhost:3000/` and you should see the app
7. Enjoy...

**Note:**  Web Application is written using `react-create-app` boilerplate. Lern more [here](https://github.com/facebookincubator/create-react-app)

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
