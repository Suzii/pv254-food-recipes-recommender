import './index.css';

import React from 'react';
import ReactDOM from 'react-dom';
import { Router, Route, browserHistory, IndexRoute } from 'react-router';

import App from './App';
import Home from './pages/home/home';
import Recipes from './pages/recipes/recipes';
import Index from './pages/recipes/index';
import Recipe from './pages/recipe/recipe';
import _404 from './pages/404';

ReactDOM.render(
    <Router history={browserHistory}>
        <Route path="/" component={App}>
            <IndexRoute component={Home} />
            <Route path="home" component={Home}/>
            <Route path="recipes" component={Recipes}>
                <IndexRoute component={Index} />
                <Route path="/recipes/:recipeId" component={Recipe}/>
            </Route>
            <Route path="*" component={_404}/>
        </Route>
    </Router>,
    document.getElementById('root')
);
