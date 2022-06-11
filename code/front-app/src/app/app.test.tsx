import { render } from '@testing-library/react';
import React from 'react';
import { Provider } from 'react-redux';
import { store } from '../state/store';
import { App } from './app';

test('renders learn react link', () => {
    const { getByRole } = render(
        <Provider store={store}>
            <App />
        </Provider>
    );

    expect(getByRole('heading')).toBeInTheDocument();
});
