import * as Actions from './actions';
import fetch from 'isomorphic-fetch';
import * as AjaxUtils from './../utils/ajax';
import {getVisitedRecipeIds} from '../utils/cookies.js';
import {isNullOrEmpty} from './../utils/arrays';

// --------------------------------- RECIPE ------------------------------------
function requestRecipe(id) {
    return {
        type: Actions.RECIPE_REQUEST,
        id: id
    }
}

function receiveRecipe(response) {
    return {
        type: Actions.RECIPE_SUCCESS,
        response: response
    }
}

function recipesFailed(e, dispatch) {
    AjaxUtils.logNetworkError(e);
    return {
        type: Actions.RECIPE_FAILURE,
        error: e
    };
}

function fetchRecipe(id) {
    return function (dispatch) {
        dispatch(requestRecipe(id));

        fetch(`/api/recipes/${id}`, {accept: 'application/json'})
            .then(AjaxUtils.processStatus)
            .then(AjaxUtils.parseJson)
            .then(data => {
                setTimeout(() => {
                    dispatch(receiveRecipe(data));
                }, 1100);
            })
            .catch(recipesFailed);
    }
}

export function fetchRecipeIfNeeded(id) {
    return function (dispatch, getState) {
        var state = getState();
        var recipe = state.recipes[id];
        var shouldFetch = recipe === undefined || recipe === null;

        if (shouldFetch) {
            return dispatch(fetchRecipe(id));
        } else {
            return Promise.resolve();
        }
    }
}


// --------------------------- SIMILAR-RECIPES ----------------------------------
function requestSimilarRecipes(id) {
    return {
        type: Actions.SIMILAR_RECIPES_REQUEST,
        id: id
    }
}

function receiveSimilarRecipes(response) {
    return {
        type: Actions.SIMILAR_RECIPES_SUCCESS,
        response: response
    }
}

function similarRecipesFailed(e) {
    AjaxUtils.logNetworkError(e);
    return {
        type: Actions.SIMILAR_RECIPES_FAILURE,
        error: e
    };
}

export function fetchSimilarRecipes(id) {
    return function (dispatch) {
        dispatch(requestSimilarRecipes(id));

        fetch(`/api/recommendations/SimilarRecipes?${getRecommendationsQueryString(id)}`, {accept: 'application/json'})
            .then(AjaxUtils.processStatus)
            .then(AjaxUtils.parseJson)
            .then(data => {
                setTimeout(() => {
                    dispatch(receiveSimilarRecipes(data));
                }, 1100);
            })
            .catch(similarRecipesFailed);
    }
}

// --------------------------- YOU-MAY-LIKE ----------------------------------
function requestYouMayLike(id) {
    return {
        type: Actions.YOU_MAY_LIKE_REQUEST,
        id: id
    }
}

function receiveYouMayLike(response) {
    return {
        type: Actions.YOU_MAY_LIKE_SUCCESS,
        response: response
    }
}

function youMayLikeFailed(e) {
    return {
        type: Actions.YOU_MAY_LIKE_FAILURE,
        error: e
    }
}

export function fetchYouMayLike(id) {
    return function (dispatch) {
        dispatch(requestYouMayLike(id));

        var visitedRecipeIds = getVisitedRecipeIds();
        let params = {
            pageSize: 6
        };

        let paramsString1 = AjaxUtils.getQueryParametersFromArray('visitedRecipeIds', visitedRecipeIds);

        let paramsString2 = AjaxUtils.getQueryParameters(params);

        fetch(`/api/recommendations/UserContext?${paramsString1}&${paramsString2}`, {accept: 'application/json'})
            .then(AjaxUtils.processStatus)
            .then(AjaxUtils.parseJson)
            .then(data => {
                setTimeout(() => {
                    dispatch(receiveYouMayLike(data));
                }, 1100);
            })
            .catch(youMayLikeFailed);
    }
}

// --------------------------- INGREDIENT-BASED ----------------------------------
function requestIngredientBased(id) {
    return {
        type: Actions.INGREDIENT_BASED_REQUEST,
        id: id
    }
}

function receiveIngredientBased(response) {
    return {
        type: Actions.INGREDIENT_BASED_SUCCESS,
        response: response
    }
}

function ingredientBasedFailed(e) {
    return {
        type: Actions.INGREDIENT_BASED_FAILURE,
        error: e
    }
}

export function fetchIngredientBased(id) {
    return function (dispatch) {
        dispatch(requestIngredientBased(id));

        fetch(`/api/recommendations/IngredientBased?${getRecommendationsQueryString(id)}`, {accept: 'application/json'})
            .then(AjaxUtils.processStatus)
            .then(AjaxUtils.parseJson)
            .then(data => {
                setTimeout(() => {
                    dispatch(receiveIngredientBased(data));
                }, 1100);
            })
            .catch(ingredientBasedFailed);
    }
}

// --------------------------- INGREDIENT-SEARCH ----------------------------------
function requestIngredientDatabase() {
    return {
        type: Actions.INGREDIENT_SEARCH_REQUEST,
    }
}

function receiveIngredientDatabase(response) {
    return {
        type: Actions.INGREDIENT_SEARCH_SUCCESS,
        response: response
    }
}

function ingredientDatabaseFailed(e) {
    return {
        type: Actions.INGREDIENT_SEARCH_FAILURE,
        error: e
    }
}

function fetchIngredientDatabase() {
    return function (dispatch) {
        dispatch(requestIngredientDatabase());

        fetch(`/api/search/IngredientSearch`, {accept: 'application/json'})
            .then(AjaxUtils.processStatus)
            .then(AjaxUtils.parseJson)
            .then(data => {
                setTimeout(() => {
                    dispatch(receiveIngredientDatabase(data));
                }, 1100);
            })
            .catch(ingredientDatabaseFailed);
    }
}

export function fetchIngredientDatabaseIfNeeded() {
    return function (dispatch, getState) {
        var state = getState();
        var ingredientDatabase = state.ingredientDatabase;
        var shouldFetch = isNullOrEmpty(ingredientDatabase);

        if (shouldFetch) {
            return dispatch(fetchIngredientDatabase());
        } else {
            return Promise.resolve();
        }
    }
}

// --------------------------- RECIPES-SEARCH ----------------------------------
function requestRecipeDatabase() {
    return {
        type: Actions.RECIPES_SEARCH_REQUEST,
    }
}

function receiveRecipeDatabase(response) {
    return {
        type: Actions.RECIPES_SEARCH_SUCCESS,
        response: response
    }
}

function recipeDatabaseFailed(e) {
    return {
        type: Actions.RECIPES_SEARCH_FAILURE,
        error: e
    }
}

function fetchRecipeDatabase() {
    return function (dispatch) {
        dispatch(requestRecipeDatabase());

        fetch(`/api/search/RecipeSearch`, {accept: 'application/json'})
            .then(AjaxUtils.processStatus)
            .then(AjaxUtils.parseJson)
            .then(data => {
                setTimeout(() => {
                    dispatch(receiveRecipeDatabase(data));
                }, 1100);
            })
            .catch(recipeDatabaseFailed);
    }
}

export function fetchRecipeDatabaseIfNeeded() {
    return function (dispatch, getState) {
        var state = getState();
        var recipeDatabase = state.recipeDatabase;
        var shouldFetch = isNullOrEmpty(recipeDatabase);

        if (shouldFetch) {
            return dispatch(fetchRecipeDatabase());
        } else {
            return Promise.resolve();
        }
    }
}


// ------- utils ------
function getRecommendationsQueryString(id) {
    let params = {
        currentRecipeId: id,
        pageSize: 5
    };

    let paramsString = AjaxUtils.getQueryParameters(params);
    return paramsString;
}
