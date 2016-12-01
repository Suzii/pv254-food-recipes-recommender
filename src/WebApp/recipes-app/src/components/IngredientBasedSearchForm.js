import React from 'react';
import {connect} from 'react-redux';
import {browserHistory} from 'react-router';

import IngredientSearchAutocompleteWrapper from './IngredientSearch';
import {searchByIngredientFilter} from '../redux/actionCreators.js';
import { isNullOrEmpty } from '../utils/arrays.js';

class IngredientBasedSearch extends React.Component {

    static propTypes = {
        search: React.PropTypes.func.isRequired,
    };

    constructor(props) {
        super(props);

        this.ingredientId = null;
        this.timeTo = null;
        this.isVegetarian = null;
        this.state = {selectedIngredients: []};
    }

    _ingredientAdded(id, name) {
        var selectedIngredients = this.state.selectedIngredients;
        if(isNullOrEmpty(selectedIngredients.filter(ingredient => ingredient.id === id))) {
            selectedIngredients.push({id, name});
            this.setState({selectedIngredients})
        } else {
            window.alert('Come on, this one is already in...');
        }
    }

    _ingredientRemoved(id) {
        var selectedIngredients = this.state.selectedIngredients;
        selectedIngredients = selectedIngredients.filter((ingredient) => ingredient.id !== id);
        this.setState({selectedIngredients})
    }

    render() {
        const selectedIngredients = this.state.selectedIngredients.map((ingredient, index) => (
            <span className="label label-default search-by-ingredients--selected-ingredients__ingredient" key={index}>
                {ingredient.name}
                &nbsp;<i className="glyphicon glyphicon-remove" onClick={() => this._ingredientRemoved(ingredient.id)}></i>
            </span>)
        );

        return (
            <div className="row ingredient-based-search">
                <form className="form-horizontal">
                    <div className="form-group">
                        <label htmlFor="ingredient" className="col-sm-2 control-label">Ingredient</label>
                        <div className="col-sm-10">
                            <IngredientSearchAutocompleteWrapper onIngredientSelected={(id, name) => this._ingredientAdded(id, name)} />
                        </div>
                    </div>
                    <div className="col-sm-12 col-md-offset-2 col-md-10">
                        <div className="search-by-ingredients--selected-ingredients ">
                            {selectedIngredients}
                        </div>
                    </div>

                    <div className="form-group">
                        <label htmlFor="availableTimeTo" className="col-sm-2 control-label">Max cooking time</label>
                        <div className="col-sm-10 controls">
                            <div className="input-group">
                            <input
                                type="number"
                                step="5"
                                min="10"
                                max="240"
                                className="form-control"
                                id="availableTimeTo"
                                placeholder="10 minutes"
                                ref={ (node) => this.timeTo = node }/>
                                <span className="input-group-addon">min</span>
                            </div>
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
        if(isNullOrEmpty(this.state.selectedIngredients)) {
            window.alert('Please, select some ingredients. If no autocomplete is provided, try refreshing the page.');
            return;
        }

        var time = this.timeTo.value;
        if(time && (time < 5 || time > 240)) {
            window.alert('Come on, give us a reasonable time...');
            return;
        }

        var query = {
            ingredientIds: this.state.selectedIngredients.map(ingredient => ingredient.id),
            totalTimeTo: time,
            isVegetarian: this.isVegetarian.checked,
            pageSize: 10
        };

        this.props.search(query);

        browserHistory.push('/search')
    }
}
const mapDispatchToProps = (dispatch) => {
    return {
        search: (query) => dispatch(searchByIngredientFilter(query)),
    }
};

export default connect((store) => { return {}}, mapDispatchToProps)(IngredientBasedSearch);