# pv254-food-recipes-recommender
School project for course PV245 Recommender Systems (Masaryk University) for suggesting food recipes.

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
  - Entitis:  classes to be persisted in database (Recipe, IngredientUsage, Ingredient)
  - Repositories: classes to serve as connection between application and DB (right now only very simple RecipesRepository.cs)

Plus Initializer and AppContext files.

 - Recipes.Service
Contains folders:
  - DTOs: for bussiness objects
  - Mappings: AutoMapper definitions
  - Stores: Interfaces and implementations of all business logic
 
 - Recipes.Web
Project for all web related stuff and app configuration.
Right now everythnig there is just boilerplate code + some addition configuration for AutoMapper and Castle Dependency Injection setup.
The idea is that in the latter stages of tte poject we will have a RESTful WebApi + single page app in react, but thats months away right now.
