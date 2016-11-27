import '../../searchByIngredients.css';
import cover from '../../../public/img/search-by-ingredients-cover.png';

import React from 'react';
import IngredientBasedSearch from '../../components/IngredientBasedSearchForm';

const Index = (props) => {
    return (
        <div className="search-by-ingredients" id="search-by-ingredients">
            <div className="search-by-ingredients--cover">
                <img src={cover} alt="ingredients cover"/>
            </div>
            <div className="container">
                <div className="search-by-ingredients--cover__content">
                    <div className="search-by-ingredients--cover__search">
                        <div className="col-xs-12 col-sm-8 col-sm-offset-2 col-md-8 col-md-offset-2 col-lg-8 col-lg-offset-2">
                            <h1 className="search-by-ingredients--cover__heading">Search by ingredients</h1>
                            <IngredientBasedSearch/>
                        </div>
                    </div>

                </div>
            </div>
        </div>);
};

export default Index;