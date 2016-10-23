import React from 'react';
import {Link} from 'react-router';

const Index = (props) => {
    return (
        <div>
            <div className="jumbotron">
                <h1>Recipes Recommender</h1>
                <p>School project for course PV245 Recommender Systems (Masaryk University) for suggesting food recipes.</p>
                <p>
                    <Link to="recipes" className="btn btn-lg btn-primary" role="button"> Let's cook </Link>
                </p>
            </div>

        </div>);
};

export default Index;