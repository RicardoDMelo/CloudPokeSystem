import 'milligram';
import React from 'react';
import { Route, Routes } from 'react-router-dom';
import { Header } from '../components/header/header';
import { HomePage } from '../pages/home/home';
import { ViewPage } from '../pages/view/view';
import './app.scss';

export class App extends React.Component {
    render() {
        return (
            <div className="container page">
                <Header />
                <Routes>
                    <Route path="/" element={<HomePage />} />
                    <Route path=":pokemonId" element={<ViewPage />} />
                </Routes>
            </div>
        );
    }
}
