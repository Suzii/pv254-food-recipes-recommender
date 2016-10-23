import React from 'react';
import Tab from './tab';
import logo from '../../public/img/logo-inversed.png';

const NavTab = () => {
    return (
        <nav className="navbar navbar-inverse navbar-static-top">
            <div className="container">
                <div className="navbar-header">
                    <button type="button" className="navbar-toggle collapsed" data-toggle="collapse"
                            data-target="#navbar" aria-expanded="false" aria-controls="navbar">
                        <span className="sr-only">Toggle navigation</span>
                        <span className="icon-bar"></span>
                        <span className="icon-bar"></span>
                        <span className="icon-bar"></span>
                    </button>
                    <a className="navbar-brand" href="/Home"><img src={logo} alt="Logo" height={50}/></a>
                </div>
                <div id="navbar" className="navbar-collapse collapse">
                    <ul className="nav navbar-nav navbar-right">
                        <Tab to="/Home" onlyActiveOnIndex={true}>Home</Tab>
                        <Tab to="/Recipes">Recipes</Tab>
                        <Tab to="/Contact">Contact</Tab>
                    </ul>
                </div>
            </div>
        </nav>
    );
};

export default NavTab;
