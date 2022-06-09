
import React, { useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { useParams } from 'react-router-dom';
import { Move } from '../../models/Move';
import { MoveCategory } from '../../models/MoveCategory';
import { PokemonDetail } from '../../models/PokemonDetail';
import { PokemonType } from '../../models/PokemonType';
import { getPokemonAsync } from '../../state/pokemonDetailSlice';
import { AppDispatch, RootState } from '../../state/store';
import { Loading } from '../common/loading/loading';
import { PokemonProp } from './pokemon-prop/pokemonProp';

export function PokemonView() {

    const params = useParams();
    const dispatch = useDispatch<AppDispatch>();
    const state = useSelector((state: RootState) => state.pokemonDetail);

    useEffect(() => {
        if (state.value?.id !== params.pokemonId || !state.isLoaded) {
            dispatch(getPokemonAsync(params.pokemonId));
        }
    });

    const getTypeText = (pokemon: PokemonDetail) => {
        if (pokemon.type2 === null) {
            return PokemonType[pokemon.type1];
        } else {
            return `${PokemonType[pokemon.type1]} | ${PokemonType[pokemon.type2]}`;
        }
    }

    if (state.isLoaded) {
        return (
            <div>
                <h2>{state.value.nickname === '' ? state.value.speciesName : state.value.nickname}</h2>
                <img src={`https://poke-images.s3.sa-east-1.amazonaws.com/${state.value.speciesId}.png`}></img>
                <PokemonProp label="Species Name" value={state.value.speciesName} />
                <PokemonProp label="Level" value={state.value.level} />
                <PokemonProp label="Gender" value={state.value.gender} />
                <PokemonProp label="Type" value={getTypeText(state.value)} />

                <h3>Stats</h3>
                <PokemonProp label="HP" value={state.value.stats.hp} />
                <PokemonProp label="Atack" value={state.value.stats.attack} />
                <PokemonProp label="Defense" value={state.value.stats.defense} />
                <PokemonProp label="Special Attack" value={state.value.stats.specialAttack} />
                <PokemonProp label="Special Defense" value={state.value.stats.specialDefense} />
                <PokemonProp label="Speed" value={state.value.stats.speed} />

                <h3>Moves</h3>
                <table>
                    <thead>
                        <tr>
                            <th>Name</th>
                            <th>Type</th>
                            <th>Category</th>
                            <th>Power</th>
                            <th>Accuracy</th>
                            <th>PP</th>
                        </tr>
                    </thead>
                    <tbody>
                        {state.value.learntMoves.map((move: Move) => {
                            return (
                                <tr key={move.name}>
                                    <td>{move.name}</td>
                                    <td>{PokemonType[move.type]}</td>
                                    <td>{MoveCategory[move.category]}</td>
                                    <td>{move.power}</td>
                                    <td>{move.accuracy}</td>
                                    <td>{move.pp}</td>
                                </tr>
                            );
                        })}
                    </tbody>
                </table>
            </div>
        );
    } else {
        return <Loading />;
    }
}