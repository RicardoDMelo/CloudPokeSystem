import { AsyncThunk, createAsyncThunk, createSlice } from '@reduxjs/toolkit';
import { api } from './apiService';

interface PokemonDetailState {
  isLoaded: boolean;
  value: PokemonDetail
}

const initialState: PokemonDetailState = {
  isLoaded: false,
  value: null
}

export const getPokemonAsync: AsyncThunk<PokemonDetail, string, {}> = createAsyncThunk<PokemonDetail, string>(
  'getPokemonAsync',
  async (pokemonId) => {
    return await api.get<PokemonDetail>(pokemonId)
      .then((response) => {
        if (response.status === 404) {
          return null;
        } else if (response.status === 200) {
          return response.data;
        }
      })
      .catch((error) => {
        console.log(error);
        return null;
      });
  }
);


export const pokemonDetailSlice = createSlice({
  name: 'pokemonDetail',
  initialState: initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addCase(getPokemonAsync.pending, (state) => {
        state.isLoaded = initialState.isLoaded;
      })
      .addCase(getPokemonAsync.fulfilled, (state, action) => {
        state.isLoaded = true;
        state.value = action.payload;
      });
  },
})

export default pokemonDetailSlice.reducer