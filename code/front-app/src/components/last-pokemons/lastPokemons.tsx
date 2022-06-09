import React, { useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { AppDispatch, RootState } from '../../state/store';
import { getLastPokemonsAsync } from '../../state/generatedPokemonsSlice';
import { Link } from 'react-router-dom';
import { Loading } from '../common/loading/loading';
import { PokemonLookup } from '../../models/PokemonLookup';

export function LastPokemons() {

    const dispatch = useDispatch<AppDispatch>();
    const state = useSelector((state: RootState) => state.generatedPokemons);

    useEffect(() => {
        if (!state.isLoaded) {
            dispatch(getLastPokemonsAsync());
        }
    });

    if (state.isLoaded) {
        return (
            <section>
                <h2>Last Pokemons</h2>
                <table>
                    <tbody>
                        {state.list.map((pokemon: PokemonLookup) => {
                            return (
                                <tr key={pokemon.id}>
                                    <td>
                                        <img src={`https://poke-images.s3.sa-east-1.amazonaws.com/${pokemon.speciesId}.png`}></img>
                                        <Link to={pokemon.id}>{pokemon.name}</Link>
                                    </td>
                                    <td>{pokemon.level}</td>
                                </tr>
                            );
                        })}
                    </tbody>
                </table>
            </section>
        );
    } else {
        return <Loading />;
    }
}