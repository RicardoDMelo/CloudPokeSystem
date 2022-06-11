import React from 'react';
import './pokemonProp.scss'

export function PokemonProp(props: any) {
    return (
        <div className="row prop-container">
            <span className='column prop-label'>{props.label}</span>
            <span className='column prop-value'>{props.value}</span>
        </div>
    );
}