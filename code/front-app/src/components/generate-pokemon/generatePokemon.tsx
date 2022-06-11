import React from 'react';
import { useDispatch } from 'react-redux';
import { GeneratePokemonRequest } from '../../models/GeneratePokemonRequest';
import { generateNewPokemonAsync } from '../../state/generatedPokemonsSlice';
import { AppDispatch } from '../../state/store';
import { AsyncButton } from '../common/async-button/asyncButton';
import './generatePokemon.scss'


export function GeneratePokemon() {

    const dispatch = useDispatch<AppDispatch>();
    const initialState: GeneratePokemonRequest = { nickname: "", levelToGrow: 50 };
    const [state, setState] = React.useState<GeneratePokemonRequest>(initialState);
    const [showLoading, setLoadingState] = React.useState(false);

    const submitForm = (e: React.FormEvent) => {
        e.preventDefault();
        setLoadingState(true);

        dispatch(generateNewPokemonAsync(state))
            .then(() => {
                setState(initialState);
                setLoadingState(false);
            });
    }

    return (
        <section className="generate-section">

            <h2>Generate a Random Pokemon</h2>
            <p>Want to try your luck? Give a nickname and a desired level, and get a new random pokemon!</p>

            <form onSubmit={submitForm}>
                <label htmlFor="nickname">Nickname</label>
                <input aria-label="Nickname Input" name="nickname"
                    onChange={(ev) => setState({ ...state, nickname: ev.target.value })}
                    value={state.nickname}></input>

                <label htmlFor="levelToGrow">Level</label>
                <input name="levelToGrow" type="number" aria-label="Level To Grow Input"
                    onChange={(ev) => setState({ ...state, levelToGrow: parseInt(ev.target.value) })}
                    value={state.levelToGrow}></input>

                <AsyncButton isLoading={showLoading} type="submit" value="Generate Pokemon" aria-label="Generate Pokemon Button" />
            </form>
        </section>
    );
}