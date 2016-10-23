import 'bootstrap/dist/css/bootstrap.css';
import './index.css';
import './sticky-footer.css';

import React from 'react';
import ReactDOM from 'react-dom';
import { Router, Route, browserHistory, IndexRoute } from 'react-router';

import App from './App';
import {default as Home} from './pages/home/index';
import Recipes from './pages/recipes/recipes';
import Index from './pages/recipes/index';
import {default as RecipeIndex} from './pages/recipes/recipe/index';
import {default as Contact} from './pages/contact/index';
import _404 from './pages/404';

ReactDOM.render(
    <Router history={browserHistory}>
        <Route path="/" component={App}>
            <IndexRoute component={Home} />
            <Route path="home" component={Home}/>
            <Route path="recipes" component={Recipes}>
                <IndexRoute component={Index} />
                <Route path="/recipes/:recipeId" component={RecipeIndex}/>
            </Route>
            <Route path="contact" component={Contact}/>
            <Route path="*" component={_404}/>
        </Route>
    </Router>,
    document.getElementById('root')
);
