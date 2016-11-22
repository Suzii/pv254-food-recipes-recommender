import 'bootstrap/dist/css/bootstrap.css';
import 'babel-polyfill';
import './index.css';
import './sticky-footer.css';
import './autosuggest.css';

import React from 'react';
import ReactDOM from 'react-dom';
import {Router, Route, browserHistory, IndexRoute} from 'react-router';

import { createStore, applyMiddleware } from 'redux';
import thunk from 'redux-thunk';
import createLogger from 'redux-logger';
import { Provider } from 'react-redux';
import rootReducer from './redux/reducers';

import App from './App';
import {default as Home} from './pages/home/Index';
import Recipes from './pages/recipes/Recipes';
import Index from './pages/recipes/Index';
import Search from './pages/recipes/SearchResults';
import {default as RecipeIndex} from './pages/recipes/recipe/Index';
import {default as Contact} from './pages/contact/Index';
import _404 from './pages/404';

import { appendRecipeIdToCookies } from './utils/cookies.js';

var store = createStore(rootReducer, applyMiddleware(thunk, createLogger()));

ReactDOM.render(
    <Provider store={store}>
        <Router history={browserHistory}>
            <Route path="/" component={App}>
                <IndexRoute component={Home}/>
                <Route path="home" component={Home}/>
                <Route path="recipes" component={Recipes}>
                    <IndexRoute component={Index}/>
                    <Route path="/search" component={Search}/>
                    <Route path="/recipes/:recipeId" component={RecipeIndex} onEnter={(nextState, replace, callback) => {
                        var nextRecipeId = nextState.params.recipeId;
                        appendRecipeIdToCookies(nextRecipeId);
                        callback();
                    }}/>
                </Route>
                <Route path="contact" component={Contact}/>
                <Route path="*" component={_404}/>
            </Route>
        </Router>
    </Provider>,
    document.getElementById('root')
);
