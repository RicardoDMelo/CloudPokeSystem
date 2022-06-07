import React from 'react';

export function PokemonProp(props: any) {
    return (
        <div>
            <span>{props.label}</span>
            <span>{props.value}</span>
        </div>
    );
}