import { Move } from "./Move";
import { PokemonType } from "./PokemonType";
import { Stats } from "./Stats";

export class PokemonDetail {
    id: string;
    nickname: string | null;
    gender: string;
    speciesId: number;
    speciesName: string;
    type1: PokemonType;
    type2: PokemonType;
    level: number;
    learntMoves: Move[];
    stats: Stats;    
}