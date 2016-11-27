import React from 'react';
import {connect} from 'react-redux';

import Div from '../../../components/Div';
import Recipe from './Recipe';
import SimilarRecipes from './recommendations/SimilarRecipes';
import YouMayLike from './recommendations/YouMayLike';
import PureComponent from '../../../components/PureComponent.js';

import {
    fetchRecipeIfNeeded,
    fetchIngredientBased,
    fetchSimilarRecipes,
    fetchYouMayLike
} from './../../../redux/actionCreators';

class Index extends React.Component {

    static propTypes = {
        params: React.PropTypes.shape({recipeId: React.PropTypes.string}),
        isFetching: React.PropTypes.bool.isRequired,
        recipe: React.PropTypes.any,
        youMayLikeRecommendations: React.PropTypes.any.isRequired,
        similarRecipesRecommendations: React.PropTypes.any.isRequired,
        ingredientBasedRecommendations: React.PropTypes.any.isRequired,
        fetchRecipe: React.PropTypes.func.isRequired,
        fetchAllRecommendations: React.PropTypes.func.isRequired
    };


    constructor(props) {
        super(props);
        props.fetchRecipe(props.params.recipeId);
        props.fetchAllRecommendations(props.params.recipeId);
    }

    componentWillReceiveProps(nextProps) {
        if (this.props.params.recipeId !== nextProps.params.recipeId) {
            nextProps.fetchRecipe(nextProps.params.recipeId);
            nextProps.fetchAllRecommendations(nextProps.params.recipeId);
        }
    }

    render() {
        var recipeId = parseInt(this.props.params.recipeId, 10);
        return (
            <Div className="container">
                <div className="row">
                    <div className="col-xs-12 col-sm-10 col-md-8">
                        <Recipe { ...this.props }/>
                    </div>
                    <div className="col-xs-12 col-sm-2 col-md-4">
                        <div className="row">
                            <div className="col-xs-12">
                                <SimilarRecipes
                                    currentRecipeId={ recipeId }
                                    similarRecipesRecommendations={ this.props.similarRecipesRecommendations }
                                    ingredientBasedRecommendations={ this.props.ingredientBasedRecommendations }
                                />
                            </div>
                        </div>
                    </div>
                </div>

                <hr/>
                <div className="row">
                    <div className="col-xs-12">
                        <YouMayLike
                            currentRecipeId={ recipeId }
                            recipes={ this.props.youMayLikeRecommendations }/>
                    </div>
                </div>
            </Div>);
    }
}

const mapDispatchToProps = (dispatch) => {
    return {
        fetchRecipe: (id) => dispatch(fetchRecipeIfNeeded(id)),
        fetchAllRecommendations: (id) => {
            dispatch(fetchSimilarRecipes(id));
            dispatch(fetchIngredientBased(id));
            dispatch(fetchYouMayLike(id));
        }
    }
};

const mapStateToProps = (store) => {
    return {
        isFetching: store.currentRecipe.isFetching,
        recipe: store.recipes[store.currentRecipe.id],
        youMayLikeRecommendations: store.youMayLike,
        similarRecipesRecommendations: store.similarRecipes,
        ingredientBasedRecommendations: store.ingredientBased
    }
};

const IndexWrapper = connect(mapStateToProps, mapDispatchToProps)(Index);

export default PureComponent(IndexWrapper);