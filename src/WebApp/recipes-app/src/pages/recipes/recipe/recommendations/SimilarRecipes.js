import React from 'react';
import RecipeOverview from '../../../../components/RecipeOverview';
import Div from '../../../../components/Div';
import PureComponent from '../../../../components/PureComponent.js';
import {isNullOrEmpty} from '../../../../utils/arrays.js';

const recommendationShape = {
    id: React.PropTypes.number.isRequired,
    title: React.PropTypes.string.isRequired,
    cookTimeInMinutes: React.PropTypes.number,
    prepTimeInMinutes: React.PropTypes.number,
    imageUrl: React.PropTypes.string,
    isVegetarian: React.PropTypes.bool,
    recommenderType: React.PropTypes.number.isRequired
};

function mergePriorityLists(list_A, list_B) {
    const a_listWithScore = list_A.map((item, index) => ({...item, score: index * 10}));
    const b_listWithScore = list_B.map((item, index) => ({...item, score: index * 10}));

    var mergedList = [];
    for(let i = 0; i < a_listWithScore.length; i++) {
        const a_item = a_listWithScore[i];
        const resultItem = Object.assign({}, a_item, {recommendedBy: [a_item.recommenderType]});
        mergedList.push(resultItem);
    }

    for(let j = 0; j < b_listWithScore.length; j++) {
        const b_item = b_listWithScore[j];
        const exstingIndex = mergedList.indexOf(i => i.id === b_listWithScore[j].id);
        if(exstingIndex !== -1) {
            mergedList[exstingIndex].recommendedBy.push(b_item.recommenderType);
            mergedList[exstingIndex].score += b_item.score;
        }
        else {
            const resultItem = Object.assign({}, b_item, {recommendedBy: [b_item.recommenderType]});
            mergedList.push(resultItem);
        }
    }

    var sortedMergedList = mergedList.sort((a, b) => a.score - b.score);

    return sortedMergedList.slice(0, 10);
}


/*
    Receives two lists of ten recommendations each.
    Should merge them according to prioritized ordering and display first 10 recommendations.
    RecommendedBy flag has to be correctly maintained and if the recommendatin was placed in both lists,
    RecommendedBy has to contain both recommender types.
 */
class SimilarRecipes extends React.Component {

    static propTypes={
        currentRecipeId: React.PropTypes.number.isRequired,
        similarRecipesRecommendations: React.PropTypes.arrayOf(React.PropTypes.shape(recommendationShape)).isRequired,
        ingredientBasedRecommendations: React.PropTypes.arrayOf(React.PropTypes.shape(recommendationShape)).isRequired,
    };


    render() {
        const isLoading = isNullOrEmpty(this.props.similarRecipesRecommendations) || isNullOrEmpty(this.props.ingredientBasedRecommendations);

        const recommendationsList = (isLoading)? [] : mergePriorityLists(this.props.ingredientBasedRecommendations, this.props.similarRecipesRecommendations);
        const recommendations = recommendationsList.map((recipe, index) => <RecipeOverview key={index} {...recipe}  displayedRecipeId={this.props.currentRecipeId} />);

        console.log(recommendationsList);

        return (
            <div>
                <h2>Similar recipes</h2>
                <Div isLoading={isLoading} loadingOffset="100px" className="recommendations recommendations-right">
                    { recommendations }
                </Div>
            </div>
        );
    }
}

export default PureComponent(SimilarRecipes);