import React from 'react';
import { Link } from 'react-router-dom';
import './Header.scss'

export class Header extends React.Component {
    render() {
        return (
            <Link to="/">
                <header className="container title">
                    <img alt="Pokeball Logo" src="/logo.png"></img>
                    <h1>Pokemon Incubator</h1>
                </header>
            </Link>);
    }
}