import React from 'react';
import { GeneratePokemon } from '../components/generate-pokemon/generatePokemon';
import { LastPokemons } from '../components/last-pokemons/lastPokemons';

export function HomePage() {
    return (
        <section>
            <GeneratePokemon />
            <LastPokemons />
        </section>
    );
}