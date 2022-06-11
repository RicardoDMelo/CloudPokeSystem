import React, { useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { AppDispatch, RootState } from '../../state/store';
import { getLastPokemonsAsync } from '../../state/generatedPokemonsSlice';
import { Link } from 'react-router-dom';
import { Loading } from '../common/loading/loading';
import { PokemonLookup } from '../../models/PokemonLookup';
import './lastPokemons.scss'

export function LastPokemons() {

    const dispatch = useDispatch<AppDispatch>();
    const state = useSelector((state: RootState) => state.generatedPokemons);

    useEffect(() => {
        if (!state.isLoaded) {
            dispatch(getLastPokemonsAsync());
        }
    });

    const getImageName = (speciesId: number): string => {
        return ('000' + speciesId).slice(-4);
    }

    if (state.isLoaded) {
        return (
            <section>
                <h2>Last Pokemons</h2>
                <table className='pokemons-table'>
                    <thead>
                        <tr>
                            <th>Pokemon</th>
                            <th className='level-cell'>Level</th>
                        </tr>
                    </thead>
                    <tbody>
                        {state.list.map((pokemon: PokemonLookup) => {
                            return (
                                <tr key={pokemon.id}>
                                    <td className='link-cell'>
                                        <Link to={pokemon.id}>
                                            <img
                                                alt={pokemon.name}
                                                src={`https://poke-images.s3.sa-east-1.amazonaws.com/${getImageName(pokemon.speciesId)}.png`}></img>
                                            {pokemon.name}
                                        </Link>
                                    </td>
                                    <td className='level-cell'>
                                        {pokemon.level}
                                    </td>
                                </tr>
                            );
                        })}
                    </tbody>
                </table>
            </section>
        );
    } else {
        return (
            <section className='loading-container'>
                <Loading />
            </section>
        );
    }
}