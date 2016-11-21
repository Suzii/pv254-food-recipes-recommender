import React from 'react';
import {connect} from 'react-redux';
import {browserHistory} from 'react-router';

import IngredientSearchAutocompleteWrapper from './IngredientSearch';
import {searchByIngredientFilter} from '../redux/actionCreators.js';

class IngredientBasedSearch extends React.Component {

    static propTypes = {
        search: React.PropTypes.func.isRequired,
    };

    constructor(props) {
        super(props);

        this.ingredientId = null;
        this.timeTo = null;
        this.isVegetarian = null;
    }

    render() {
        return (
            <div className="row ingredient-based-search">
                <form className="form-horizontal">
                    <div className="form-group">
                        <label htmlFor="ingredient" className="col-sm-2 control-label">Ingredient</label>
                        <div className="col-sm-10">
                            <IngredientSearchAutocompleteWrapper onIngredientSelected={id => this.ingredientId = id} />
                        </div>
                    </div>
                    <div className="form-group">
                        <label htmlFor="availableTimeTo" className="col-sm-2 control-label">Max cooking time</label>
                        <div className="col-sm-10">
                            <input type="number" className="form-control" id="availableTimeTo" placeholder="10 minutes"
                                    ref={ (node) => this.timeTo = node }/>
                        </div>
                    </div>
                    <div className="form-group">
                        <div className="col-sm-offset-2 col-sm-10">
                            <div className="checkbox">
                                <label>
                                    <input type="checkbox"
                                           ref={ (node) => this.isVegetarian = node }/>
                                    Only vegetarian
                                </label>
                            </div>
                        </div>
                    </div>
                    <div className="form-group">
                        <div className="col-sm-offset-2 col-sm-10">
                            <button type="submit" className="btn btn-default" onClick={(e) => this.submit(e) }>Search</button>
                        </div>
                    </div>
                </form>
            </div>
        );
    }

    submit(event) {
        event.preventDefault();

        var query = {
            ingredientIds: [this.ingredientId],
            totalTimeTo: this.timeTo.value,
            isVegetarian: this.isVegetarian.checked,
            pageSize: 10
        };

        this.props.search(query);

        console.log('Submitting', query);

        browserHistory.push('/search')
    }
}
const mapDispatchToProps = (dispatch) => {
    return {
        search: (query) => dispatch(searchByIngredientFilter(query)),
    }
};

export default connect((store) => { return {}}, mapDispatchToProps)(IngredientBasedSearch);