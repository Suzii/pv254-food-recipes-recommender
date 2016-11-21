import React from 'react';
import {Link} from 'react-router';

import RecipeSearchAutocomplete from '../../components/RecipesSearchAutocomplete';
import MostPopularRecipes from '../../components/MostPopularRecipes';

const Index = (props) => {

    return (
        <div className="container">
            <div className="jumbotron">
                <h1>Recipes Recommender</h1>
                <p>School project for course PV245 Recommender Systems (Masaryk University) for suggesting food recipes.</p>

                <p>TODO guidlines for UI testing</p>

                <p>
                    <Link to="recipes" className="btn btn-lg btn-primary" role="button"> Let's cook </Link>
                </p>
            </div>

            <div className="row">
                <div className="col-xs-12 col-sm-8 col-md-8">
                        <h2 className="text">Find the right recipe for you</h2>
                        <div className="col-lg-3">
                            <RecipeSearchAutocomplete/>
                        </div>
                </div>

                <div className="col-xs-12 col-sm-4 col-md-4">
                    <MostPopularRecipes count={10}/>
                </div>
            </div>

        </div>);
};

export default Index;