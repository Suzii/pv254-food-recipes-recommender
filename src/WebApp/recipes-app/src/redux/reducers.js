import { combineReducers } from 'redux';
import * as actions from './actions';

function currentRecipe(state = {isFetching: false, id: undefined}, action) {
    switch(action.type) {
        case actions.RECIPE_REQUEST:
            return { isFetching: true, id: action.id};
        case actions.RECIPE_SUCCESS:
            return { ...state, isFetching: false};
        case actions.RECIPE_FAILURE:
            return { isFetching: false, id: undefined};
        default:
            return state;
    }
}

function recipes(state = {}, action) {
    switch(action.type) {
        case actions.RECIPE_SUCCESS:
            return { [action.response.id]: action.response};
        default:
            return state;
    }
}

function youMayLike(state = [], action) {
    switch(action.type) {
        case actions.YOU_MAY_LIKE_REQUEST:
        case actions.YOU_MAY_LIKE_FAILURE:
            return [];
        case actions.YOU_MAY_LIKE_SUCCESS:
            return action.response;
        default:
            return state;
    }
}

function similarRecipes(state = [], action) {
    switch(action.type) {
        case actions.SIMILAR_RECIPES_REQUEST:
        case actions.SIMILAR_RECIPES_FAILURE:
            return [];
        case actions.SIMILAR_RECIPES_SUCCESS:
            return action.response;
        default:
            return state;
    }
}

function ingredientBased(state = [], action) {
    switch(action.type) {
        case actions.INGREDIENT_BASED_REQUEST:
        case actions.INGREDIENT_BASED_FAILURE:
            return [];
        case actions.INGREDIENT_BASED_SUCCESS:
            return action.response;
        default:
            return state;
    }
}

function recipeDatabase(state = [], action) {
    switch(action.type) {
        case actions.RECIPES_SEARCH_REQUEST:
        case actions.RECIPES_SEARCH_FAILURE:
            return [];
        case actions.RECIPES_SEARCH_SUCCESS:
            return action.response;
        default:
            return state;
    }
}

function ingredientDatabase(state = [], action) {
    switch(action.type) {
        case actions.INGREDIENT_SEARCH_REQUEST:
        case actions.INGREDIENT_SEARCH_FAILURE:
            return [];
        case actions.INGREDIENT_SEARCH_SUCCESS:
            return action.response;
        default:
            return state;
    }
}

const rootReducer = combineReducers({
     currentRecipe,
     recipes,
     youMayLike,
     similarRecipes,
     ingredientBased,
     recipeDatabase,
     ingredientDatabase
 });

export default rootReducer;
