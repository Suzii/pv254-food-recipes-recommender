import React from 'react';

import Div from '../../../components/Div';

import Ingredients from './ingredients';
import Instructions from './instructions';
import RecipeMetadata from './recipeMetadata';

class Recipe extends React.Component {

    static propTypes = {
        params: React.PropTypes.shape({ recipeId: React.PropTypes.string })
    }
    state = {
        isLoading: true
    }

    constructor(props) {
        super(props);
        fetch(`/api/recipes/${props.params.recipeId}`, {accept: 'application/json'})
            .then(response => response.json())
            .then(data => {
                setTimeout(() => this.recipeReceived(data), 1100);
            });
    }

    recipeReceived(data) {
        console.log('Recipe: ', data);
        this.setState({
            isLoading: false,
            recipe: data
        });
    }

    render() {
        var recipe = this.state.recipe;
        if (!recipe) {
            return <Div isLoading={true}/>;
        }

        return (
            <Div isLoading={this.state.isLoading} className="recipe" id="recipe">
                <div className="row">
                    <div className="recipe-teaser">
                        <img src={ recipe.imageUrl } alt="Recipe teaser." className="img-responsive recipe-img" />
                            <div className="caption">
                                <h1>{ recipe.title }</h1>
                            </div>
                    </div>
                </div>

                <div className="row">
                    <div className="col-xs-12 col-sm-8 col-md-8 col-lg-6">

                        <Ingredients ingredients={ recipe.ingredients }/>

                    </div>

                    <div className="col-xs-12 col-sm-4 col-md-4 col-lg-6">
                        <RecipeMetadata { ...recipe } />
                    </div>
                </div>

                <div className="row">
                    <div className="col-xs-12">
                        <Instructions instructions={ recipe.instructions }/>
                    </div>
                </div>
            </Div>);
    }
}

export default Recipe;