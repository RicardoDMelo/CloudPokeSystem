import { configureStore } from '@reduxjs/toolkit';
import generatedPokemonsSlice from './generatedPokemonsSlice';
import pokemonDetailSlice from './pokemonDetailSlice';

export const store = configureStore({
  reducer: {
    generatedPokemons: generatedPokemonsSlice,
    pokemonDetail: pokemonDetailSlice,
  },
});

export type RootState = ReturnType<typeof store.getState>
export type AppDispatch = typeof store.dispatch