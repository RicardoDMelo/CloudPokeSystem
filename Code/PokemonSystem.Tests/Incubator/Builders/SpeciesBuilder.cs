using PokemonSystem.Common.Enums;
using PokemonSystem.Common.ValueObjects;
using PokemonSystem.Incubator.Domain.SpeciesAggregate;
using System.Collections.Generic;

namespace PokemonSystem.Tests.Incubator.Builders
{
    public class SpeciesBuilder
    {
        private int _number = 128;
        private string _name = "Tauros";
        private Typing _type = new Typing(PokemonType.Normal);
        private Stats _baseStats = new Stats(1, 2, 3, 4, 5, 6);
        private EvolutionCriteria _evolutionCriteria = null;
        private double _maleFactor = 0.6;

        private MoveSetBuilder _moveSetBuilder;

        public SpeciesBuilder()
        {
            Reset();
        }

        public void Reset()
        {
            _number = 128;
            _name = "Tauros";
            _type = new Typing(PokemonType.Normal);
            _baseStats = new Stats(1, 2, 3, 4, 5, 6);
            _evolutionCriteria = null;
            _maleFactor = 0.6;
            _moveSetBuilder = new MoveSetBuilder();
        }

        public SpeciesBuilder WithNumber(int number)
        {
            _number = number;
            return this;
        }
        public SpeciesBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public SpeciesBuilder WithType(Typing type)
        {
            _type = type;
            return this;
        }
        public SpeciesBuilder WithBaseStats(Stats baseStats)
        {
            _baseStats = baseStats;
            return this;
        }
        public SpeciesBuilder WithMaleFactor(double maleFactor)
        {
            _maleFactor = maleFactor;
            return this;
        }
        public SpeciesBuilder WithEvolutionCriteria(EvolutionCriteria evolutionCriteria)
        {
            _evolutionCriteria = evolutionCriteria;
            return this;
        }

        public SpeciesBuilder AddMove(MoveByLevel moveByLevel)
        {
            _moveSetBuilder.AddMove(moveByLevel);
            return this;
        }
        public SpeciesBuilder ResetMoves()
        {
            _moveSetBuilder.ResetMoves();
            return this;
        }
        public SpeciesBuilder WithMoveSet(List<MoveByLevel> moveSet)
        {
            _moveSetBuilder.ResetMoves();
            moveSet.ForEach(x => _moveSetBuilder.AddMove(x));
            return this;
        }

        public Species Build()
        {
            var species = new Species(
                _number,
                _name,
                _type,
                _baseStats,
                _maleFactor,
                _evolutionCriteria,
                _moveSetBuilder.Build()
            );
            Reset();
            return species;
        }
    }
}