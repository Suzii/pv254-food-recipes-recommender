import cookie from 'react-cookie';

const EXPIRATION_TIME_IN_MINUTES = 60;
const VISITED_RECIPES_COOKIE_NAME = 'visited_recipes';

function appendRecipeIdToCookies(recipeId) {
    var now = new Date();
    var expires = now.setMinutes(now.getMinutes() + EXPIRATION_TIME_IN_MINUTES);
    const opt = {
        expires: new Date(expires),
        path: '/'
    };

    var visitedRecipes = cookie.load(VISITED_RECIPES_COOKIE_NAME) || [];
    if(visitedRecipes.indexOf(recipeId) === -1) {
        visitedRecipes.push(recipeId);
    }

    cookie.save(VISITED_RECIPES_COOKIE_NAME, JSON.stringify(visitedRecipes), opt);
}

function getVisitedRecipeIds() {
    var recipeIds = cookie.load(VISITED_RECIPES_COOKIE_NAME);
    return recipeIds;
}

export {appendRecipeIdToCookies, getVisitedRecipeIds};