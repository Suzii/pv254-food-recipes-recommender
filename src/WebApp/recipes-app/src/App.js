import React from 'react';
import NavTab from './components/NavTab';

const App = (props) => {
    return (
        <div>
            <header>
                <NavTab/>
            </header>

            <section id="content">
                {props.children}
            </section>

            <footer className="footer">
                <div className="container">
                    <p className="text-muted">
                        Powered by <a href="http://www.bbc.co.uk/food/recipes/">BBC Food.</a>
                    </p>
                </div>
            </footer>
        </div>
    )
};

export default App;
