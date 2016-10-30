import React from 'react';

class IngredientBasedSearch extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            ingredients: [],
            timeTo: null
        }

        this.ingredientInput;
        this.timeToInput;
        this.onlyVegetarianCheckbox;
    }

    render() {
        return (
            <div className="row ingredient-based-search">
                <h2>Search by ingredients and available time</h2>

                <form className="form-horizontal">
                    <div className="form-group">
                        <label htmlFor="ingredient" className="col-sm-2 control-label">Ingredient</label>
                        <div className="col-sm-10">
                            <input type="ingredient" className="form-control" id="ingredient" placeholder="Spaghetti..." ref={ (node) => this.ingredientInput = node }/>
                        </div>
                    </div>
                    <div className="form-group">
                        <label htmlFor="availableTimeTo" className="col-sm-2 control-label">Available time to</label>
                        <div className="col-sm-10">
                            <input type="availableTimeTo" className="form-control" id="availableTimeTo" placeholder="10" ref={ (node) => this.timeToInput = node }/>
                        </div>
                    </div>
                    <div className="form-group">
                        <div className="col-sm-offset-2 col-sm-10">
                            <div className="checkbox">
                                <label>
                                    <input type="checkbox" ref={ (node) => this.onlyVegetarianCheckbox = node }/>
                                     Only vegetarian
                                </label>
                            </div>
                        </div>
                    </div>
                    <div className="form-group">
                        <div className="col-sm-offset-2 col-sm-10">
                            <button type="submit" className="btn btn-default">Search</button>
                        </div>
                    </div>
                </form>
            </div>
        );
    }
}

export default IngredientBasedSearch;
