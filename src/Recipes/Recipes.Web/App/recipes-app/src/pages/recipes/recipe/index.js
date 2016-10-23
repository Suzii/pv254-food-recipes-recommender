import React from 'react';

import Div from '../../../components/Div';

import Recipe from './recipe';

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
                                    <h3>Similar recipes</h3>
                                    TODO
                                </div>
                            </div>

                            <div className="row">
                                <div className="col-xs-12">
                                    <h3>Recommended</h3>
                                    TODO
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </Div>);
    }
}

export default Index;