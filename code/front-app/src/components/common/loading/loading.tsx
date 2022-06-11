import './loading.scss';
import React from 'react'

export function Loading({ className = '' }) {
    return (
        <div className={`lds-dual-ring ${className}`}></div>
    );
}