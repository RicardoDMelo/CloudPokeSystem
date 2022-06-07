class PokemonDetail {
    id: string;
    nickname: string | null;
    gender: string;
    speciesName: string;
    level: number;
    learntMoves: Move[];
    stats: Stats;    
}