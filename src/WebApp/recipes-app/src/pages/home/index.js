import '../../homepage.css';

import React from 'react';
import {Link} from 'react-router';

import RecipeSearchAutocomplete from '../../components/RecipesSearchAutocomplete';
import MostPopularRecipes from '../../components/MostPopularRecipes';

const Index = (props) => {

    return (
        <div className="homepage" id="homepage">
            <div className="homepage--cover">
                <div className="container">
                    <div className="homepage--cover__content">
                        <h1 className="homepage--cover__heading">Find perfect recipe for today</h1>

                        <div className="homepage--cover__search">
                            <div className="col-xs-12 col-sm-6 col-sm-offset-3 col-md-6 col-md-offset-3 col-lg-4 col-lg-offset-4">
                                <RecipeSearchAutocomplete/>
                                <p className="homepage--cover__alternative"> - OR - </p>
                            </div>
                        </div>

                        <div className="homepage--cover__button">
                            <div className="col-sm-12">
                                <Link to="recipes" className="btn btn-lg btn-primary" role="button"> Search by ingredients </Link>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div className="container">
                <div className="row">
                    <div className="col-xs-12 col-sm-8 col-md-8">
                        <h2>TODO guidlines for UI testing</h2>
                        <p>School project for course PV245 Recommender Systems (Masaryk University) for suggesting food recipes.</p>
                    </div>

                    <div className="col-xs-12 col-sm-4 col-md-4">
                        <MostPopularRecipes count={10}/>
                    </div>
                </div>
            </div>
        </div>);
};

export default Index;