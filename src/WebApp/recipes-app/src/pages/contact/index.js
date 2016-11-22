import React from 'react';

const Index = (props) => {
    return (
        <div className="container">
            <div className="jumbotron">
                <h2>Contact</h2>
                <p>Do you have any questions or comments? Don't hesitate to contact us!</p>
                <div className="list-group">
                    <div className="list-group-item">
                      <p className="list-group-item-text">Zuzana Dankovčíková</p>
                      <p className="list-group-item-text">
                        <span className="glyphicon glyphicon-envelope" aria-hidden="true"></span> <a href="mailto:name@email.com">TODO</a>
                      </p>
                    </div>
                    <div className="list-group-item">
                      <p className="list-group-item-text">Daniela Plachtová</p>
                      <p className="list-group-item-text">
                        <span className="glyphicon glyphicon-envelope" aria-hidden="true"></span> <a href="mailto:name@email.com">TODO</a>
                      </p>
                    </div>
                    <div className="list-group-item">
                      <p className="list-group-item-text">Martina Vitovská</p>
                      <p className="list-group-item-text">
                        <span className="glyphicon glyphicon-envelope" aria-hidden="true"></span> <a href="mailto:409920@mail.muni.cz">409920@mail.muni.cz</a>
                      </p>
                    </div>
                  </div>
            </div>

        </div>);
};

export default Index;