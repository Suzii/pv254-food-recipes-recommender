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
                        <h2 className="text-center text-capitalize">You can help us finish the semester</h2>
                        <p className="lead"><b>This app for suggesting food recipes was created as a school project for course PV245 Recommender Systems (Masaryk University).</b></p>
                        <p className="lead">
                          As we need to to evaluate the quality of our recommender system, we collect and process data about your activity on this web page.
                          No worries, everything is completely anonymous and we only store info about what links were clicked, nothing about who did it.
                          We hope you are hungry, because we need you to view as many recipes as possible.
                        </p>
                        <p className="lead">
                          We would also like to ask you not to click on the recipes randomly,
                          that would not help us at all. To make things easier for you, just imagine your girlfriend/boyfriend is coming for dinner tonight and you want to surprise
                          them with a delicious meal. Therefore you need to choose the recipes carefully. We suggest you find the first recipe with our search tool and then you can
                          start viewing our recommendations. And don't forget, a dessert is a must!
                        </p>
                        <p className="lead">
                          We hope you will enjoy our Recipes Recommender. We really appreciate your help and we thank you for your time!
                        </p>
                    </div>

                    <div className="col-xs-12 col-sm-4 col-md-4">
                        <MostPopularRecipes count={10}/>
                    </div>
                </div>
            </div>
        </div>);
};

export default Index;