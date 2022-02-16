using PokemonSystem.Common.SeedWork;
using PokemonSystem.Common.ValueObjects;
using System;
using System.Collections.Generic;

namespace PokemonSystem.Learning.Domain.PokemonAggregate
{
    public class Pokemon : Entity, IAggregateRoot
    {
        public Pokemon(Species pokemonSpecies, Level level, List<Move> learntMoves)
        {
            PokemonSpecies = pokemonSpecies ?? throw new ArgumentNullException(nameof(pokemonSpecies));
            Level = level;
            _learntMoves = learntMoves ?? throw new ArgumentNullException(nameof(learntMoves));
        }

        public Species PokemonSpecies { get; private set; }
        public Level Level { get; private set; }
        protected List<Move> _learntMoves { get; set; }
        public IReadOnlyCollection<Move> LearntMoves { get => _learntMoves.AsReadOnly(); }
    }
}
