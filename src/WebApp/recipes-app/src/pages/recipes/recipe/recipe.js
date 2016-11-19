import React from 'react';
import noImage from '../../../../public/img/no-image.png';
import Div from '../../../components/Div';
import Ingredients from './Ingredients';
import Instructions from './Instructions';
import RecipeMetadata from './RecipeMetadata';
import PureComponent from '../../../components/PureComponent.js';

class Recipe extends React.Component {
    static propTypes = {
        params: React.PropTypes.shape({ recipeId: React.PropTypes.string }),
        isFetching: React.PropTypes.bool.isRequired,
        recipe: React.PropTypes.any,
    };

    render() {
        var recipe = this.props.recipe;
        if (!recipe) {
            return <Div isLoading={true} loadingOffset="150px"/>;
        }

        return (
            <Div isLoading={ this.props.isFetching } loadingOffset="150px" className="container-fluid recipe" id="recipe">
                <div className="row">
                    <div className="recipe-teaser">
                        <img src={ recipe.imageUrl || noImage } alt="Recipe teaser." className="img-responsive recipe-img" />
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

export default PureComponent(Recipe);