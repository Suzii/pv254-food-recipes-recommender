import React from 'react';
import IngredientSearchWrapper from './IngredientSearch';

class IngredientBasedSearch extends React.Component {
    constructor(props) {
        super(props);

        this.name = null;
        this.timeFrom = null;
        this.isVegetarian = null;
    }

    render() {
        return (
            <div className="row ingredient-based-search">
                <h2>Search by ingredients and available time</h2>

                <form className="form-horizontal">
                    <div className="form-group">
                        <label htmlFor="ingredient" className="col-sm-2 control-label">Ingredient</label>
                        <div className="col-sm-10">
                            <IngredientSearchWrapper />
                        </div>
                    </div>
                    <div className="form-group">
                        <label htmlFor="availableTimeTo" className="col-sm-2 control-label">Available time to</label>
                        <div className="col-sm-10">
                            <input type="number" className="form-control" id="availableTimeTo" placeholder="10"
                                    ref={ (node) => this.timeFrom = node }/>
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
            ingredientIds: [],
            totalTimeTo: this.timeFrom.value,
            isVegetarian: this.isVegetarian.checked,
            pageSize: 10
        };

        console.log('Submitting', query);
    }
}

export default IngredientBasedSearch;