import React from 'react';
import NavTab from './components/nav-tab';

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
                        Lorem ipsum
                    </p>
                </div>
            </footer>
        </div>
    )
};

export default App;
