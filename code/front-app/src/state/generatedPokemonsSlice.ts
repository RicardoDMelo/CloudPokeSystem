import { AsyncThunk, createAsyncThunk, createSlice } from '@reduxjs/toolkit';
import { GeneratePokemonRequest } from '../models/GeneratePokemonRequest';
import { PokemonLookup } from '../models/PokemonLookup';
import { api } from './apiService';

interface GeneratedPokemonsState {
  isGenerating: boolean;
  isLoaded: boolean;
  list: Array<PokemonLookup>
}

const initialState: GeneratedPokemonsState = {
  isGenerating: false,
  isLoaded: false,
  list: new Array<PokemonLookup>()
}

export const generateNewPokemonAsync: AsyncThunk<PokemonLookup, GeneratePokemonRequest, {}> = createAsyncThunk<PokemonLookup, GeneratePokemonRequest>(
  'generatePokemonAsync',
  async (pokemonRequest) => {
    return await api.post<PokemonLookup>('incubator', pokemonRequest)
      .then((response) => {
        return response.data;
      })
      .catch((error) => {
        console.log(error);
        return null;
      });
  }
);

export const getLastPokemonsAsync: AsyncThunk<Array<PokemonLookup>, void, {}> = createAsyncThunk<Array<PokemonLookup>>(
  'getLastPokemonsAsync',
  async () => {
    const response: Array<PokemonLookup> = await api.get<Array<PokemonLookup>>('billspc')
      .then((response) => {
        if (response.status === 204) {
          return initialState.list;
        } else if (response.status === 200) {
          return response.data;
        }
      })
      .catch((error) => {
        console.log(error);
        return new Array<PokemonLookup>();
      });
    return response;
  }
);

export const generatedPokemonsSlice = createSlice({
  name: 'lastPokemons',
  initialState: initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addCase(generateNewPokemonAsync.pending, (state) => {
        state.isGenerating = true;
      })
      .addCase(generateNewPokemonAsync.fulfilled, (state, action) => {
        state.list.unshift(action.payload);
      })
      .addCase(getLastPokemonsAsync.pending, (state) => {
        state.isLoaded = initialState.isLoaded;
      })
      .addCase(getLastPokemonsAsync.fulfilled, (state, action) => {
        state.isLoaded = true;
        state.list = action.payload;
      });
  },
})

export default generatedPokemonsSlice.reducer