import React from 'react'
import { Loading } from '../loading/loading';

export function AsyncButton(props: any) {
    if (props.isLoading) {
        return <Loading />;
    } else {
        return <input type={props.type} value={props.value} aria-label={props["aria-label"]} />;
    }
}