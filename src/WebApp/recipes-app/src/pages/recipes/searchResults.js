import React from 'react';
import { browserHistory} from 'react-router';
import {connect} from 'react-redux';

import Div from '../../components/Div';
import RecipeOverview from '../../components/RecipeOverview.js';
import {isNullOrEmpty} from '../../utils/arrays.js';

class SearchResults extends React.Component {
    static propTpyes = {
        results: React.PropTypes.array,
        isFetching: React.PropTypes.bool,
    };

    componentDidMount() {
        this.interval = window.setTimeout(() => {
            if(!this.props.isFetching && isNullOrEmpty(this.props.results)) {
                if (window.confirm('Something probably went wrong. Do you want to return to homepage?')) {
                    browserHistory.push('/');
                }
            } else {
                window.clearInterval(this.interval);
            }
        }, 5000);
    }

    componentWillUnmount() {
        window.clearInterval(this.interval);
    }

    render() {
        if (isNullOrEmpty(this.props.results)) {
            return <Div isLoading={true} loadingOffset="150px"/>;
        }

        const results = this.props.results.map((recipe, index) => <RecipeOverview showImage={true} key={index} {...recipe} recommendedBy={[recipe.recommenderType]} displayedRecipeId={null} />);

        return (
            <Div isLoading={ this.props.isFetching } loadingOffset="150px" className="container-fluid results" id="results">
                <h1 className="text-center text-capitalize">Search Results</h1>

                <div className="row">
                    <div className="col-xs-12 col-sm-12 col-md-offset-4 col-md-6">
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