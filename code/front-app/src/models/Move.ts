import { MoveCategory } from "./MoveCategory";
import { PokemonType } from "./PokemonType";

export class Move {
    name: string;
    type: PokemonType;
    category: MoveCategory;
    power: number | null;
    accuracy: number;
    pp: number;
}
