
import React, { useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { useParams } from 'react-router-dom';
import { getPokemonAsync } from '../../state/pokemonDetailSlice';
import { AppDispatch, RootState } from '../../state/store';
import { Loading } from '../common/loading/loading';
import { PokemonProp } from './pokemon-prop/pokemonProp';

export function PokemonView() {

    const params = useParams();
    const dispatch = useDispatch<AppDispatch>();
    const state = useSelector((state: RootState) => state.pokemonDetail);

    useEffect(() => {
        if (state.value.id !== params.pokemonId || !state.isLoaded) {
            dispatch(getPokemonAsync(params.pokemonId));
        }
    });

    if (state.isLoaded) {
        return (
            <div>
                <h2>{state.value.nickname === '' ? state.value.speciesName : state.value.nickname}</h2>
                <PokemonProp label="Species Name" value={state.value.speciesName} />
                <PokemonProp label="Level" value={state.value.level} />
                <PokemonProp label="Gender" value={state.value.gender} />
            </div>
        );
    } else {
        return <Loading />;
    }
}