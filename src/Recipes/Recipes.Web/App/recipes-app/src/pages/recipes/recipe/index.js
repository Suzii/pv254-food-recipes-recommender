import React from 'react';
import Div from '../../../components/div';
import Recipe from './recipe';
import SimilarRecipes from './recommendations/similar-recipes';
import YouMayLike from './recommendations/you-may-like';
import Critique from './recommendations/critiquing';

class Index extends React.Component {

    static propTypes = {
        params: React.PropTypes.shape({ recipeId: React.PropTypes.string })
    };

    render() {
        return (
            <Div>

                <div className="container-fluid">
                    <div className="row">
                        <div className="col-xs-12 col-sm-10 col-md-8">
                            <Recipe { ...this.props }/>
                        </div>
                        <div className="col-xs-12 col-sm-2 col-md-4">
                            <div className="row">
                                <div className="col-xs-12">
                                    <SimilarRecipes { ...this.props }/>
                                </div>
                            </div>

                            <div className="row">
                                <div className="col-xs-12">
                                    <YouMayLike { ...this.props }/>
                                </div>
                            </div>
                        </div>
                    </div>

                    <hr/>
                    <div className="row">
                        <div className="col-xs-12">
                            <Critique/>
                        </div>
                    </div>
                </div>
            </Div>);
    }
}

export default Index;