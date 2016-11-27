import '../pageNotFound.css';

import React from 'react';
import {Link} from 'react-router';

import RecipeSearchAutocomplete from '../components/RecipesSearchAutocomplete';

const _404 = (props) => {
    return (
        <div className="page-not-found" id="page-not-found">
            <div className="page-not-found--cover">
                <div className="container">
                    <div className="page-not-found--cover__content">
                        <h1 className="page-not-found--cover__heading">Oooops, this site does not exist...</h1>
                        <h2 className="page-not-found--cover__heading">Try searching for another recipe.</h2>

                        <div className="page-not-found--cover__search">
                            <div className="col-xs-12 col-sm-6 col-sm-offset-3 col-md-6 col-md-offset-3 col-lg-4 col-lg-offset-4">
                                <RecipeSearchAutocomplete/>
                                <p className="page-not-found--cover__alternative"> - OR - </p>
                            </div>
                        </div>

                        <div className="page-not-found--cover__button">
                            <div className="col-sm-12">
                                <Link to="recipes" className="btn btn-lg btn-primary" role="button"> Search by ingredients </Link>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>);
};

export default _404;