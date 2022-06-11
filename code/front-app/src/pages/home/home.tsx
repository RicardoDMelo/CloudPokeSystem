import React from 'react';
import { GeneratePokemon } from '../../components/generate-pokemon/generatePokemon';
import { LastPokemons } from '../../components/last-pokemons/lastPokemons';
import './home.scss'

export function HomePage() {
    return (
        <section>
            <div className="row">
                <div className="column">
                    <GeneratePokemon />
                </div>
            </div>

            <div className="row">
                <div className="column">
                    <LastPokemons />
                </div>
            </div>
        </section>
    );
}