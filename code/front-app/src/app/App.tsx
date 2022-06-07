import 'milligram';
import React from 'react';
import { Route, Routes } from 'react-router-dom';
import { Header } from '../components/header/Header';
import { HomePage } from '../pages/home';
import { ViewPage } from '../pages/view';
import './App.scss';

export class App extends React.Component {
    render() {
        return (
            <div>
                <Header />
                <Routes>
                    <Route path="/" element={<HomePage />} />
                    <Route path=":pokemonId" element={<ViewPage />} />
                </Routes>
            </div>
        );
    }
}
