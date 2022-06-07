import React from 'react';
import { getByRole, render } from '@testing-library/react';
import { Provider } from 'react-redux';
import { store } from '../state/store';
import { App } from './App';

test('renders learn react link', () => {
    const { getByRole } = render(
        <Provider store={store}>
            <App />
        </Provider>
    );

    expect(getByRole('heading')).toBeInTheDocument();
});
