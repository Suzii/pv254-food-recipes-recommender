import React from 'react';
import { connect } from 'react-redux';

import RecipeOverview from './RecipeOverview';
import Div from './Div';
import PureComponent from './PureComponent.js';
import {isNullOrEmpty} from '../utils/arrays.js';
import {
    mostPopularRecipesFetch
} from '../redux/actionCreators.js';

const recommendationShape = {
    id: React.PropTypes.number.isRequired,
    title: React.PropTypes.string.isRequired,
    cookTimeInMinutes: React.PropTypes.number,
    prepTimeInMinutes: React.PropTypes.number,
    imageUrl: React.PropTypes.string,
    isVegetarian: React.PropTypes.bool,
    recommenderType: React.PropTypes.number.isRequired
};

class MostPopularRecipes extends React.Component {
    static propTypes={
        isFetching: React.PropTypes.bool.isRequired,
        mostPopularRecipes: React.PropTypes.arrayOf(React.PropTypes.shape(recommendationShape)).isRequired,
        count: React.PropTypes.number.isRequired,
        fetchMostPopularRecipes: React.PropTypes.func.isRequired,
    };

    constructor(props) {
        super(props);
        props.fetchMostPopularRecipes(props.count);
    }

    render() {
        const isLoading = isNullOrEmpty(this.props.mostPopularRecipes);

        const recommendationsList = this.props.mostPopularRecipes || [];
        const recommendations = recommendationsList.map((recipe, index) => ( <RecipeOverview key={index} {...recipe} recommendedBy={[recipe.recommenderType]} displayedRecipeId={null} />));
        return (
            <div>
                <h2 className="text-center">Top {this.props.count} recipes</h2>
                <Div isLoading={isLoading} loadingOffset="100px" className="recommendations recommendations-right">
                    { recommendations }
                </Div>
            </div>
        );
    }
}

const mapDispatchToProps = (dispatch) => {
    return {
        fetchMostPopularRecipes: (count) => dispatch(mostPopularRecipesFetch(count)),
    };
};

const mapStateToProps = (store) => {
    return {
        isFetching: store.mostPopular.isFetching,
        mostPopularRecipes: store.mostPopular.results
    }
};

const MostPopularRecipesWrapper = connect(mapStateToProps, mapDispatchToProps)(MostPopularRecipes);

export default PureComponent(MostPopularRecipesWrapper);
