import React from 'react';
import {connect} from 'react-redux';

import Div from '../../components/Div';
import RecipeOverview from '../../components/RecipeOverview.js';
import {isNullOrEmpty} from '../../utils/arrays.js';

class SearchResults extends React.Component {

    static propTpyes = {
        results: React.PropTypes.array,
        isFetching: React.PropTypes.bool,
    }

    render() {
        if (isNullOrEmpty(this.props.results)) {
            return <Div isLoading={true} loadingOffset="150px"/>;
        }

        const results = this.props.results.map((recipe, index) => <RecipeOverview key={index} {...recipe} displayedRecipeId={6} />);

        return (
            <Div isLoading={ this.props.isFetching } loadingOffset="150px" className="container-fluid results" id="results">
                <h1 className="text-center text-capitalize">SearchResults</h1>

                <div className="row">
                    <div className="col-xs-12 col-sm-12 col-md-12">
                        {results}
                    </div>
                </div>
            </Div>
        );
    }
};

const mapStateToProps = (store) => {
    return {
        results: store.search.results,
        isFetching: store.search.isFetching,
    };
};

const SearchResultsWrapper = connect(mapStateToProps)(SearchResults);

export default SearchResultsWrapper;