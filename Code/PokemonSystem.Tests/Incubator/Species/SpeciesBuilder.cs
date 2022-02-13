using PokemonSystem.Common.Enums;
using PokemonSystem.Common.ValueObjects;
using PokemonSystem.Incubator.SpeciesAggregate;
using System.Collections.Generic;

namespace PokemonSystem.Tests.Incubator
{
    public class SpeciesBuilder
    {
        private int _number = 128;
        private string _name = "Tauros";
        private Typing _type = new Typing(PokemonType.Normal);
        private Stats _baseStats = new Stats(1, 2, 3, 4, 5, 6);
        private Species _evolutionSpecies = null;
        private double _maleFactor = 0.6;

        private MoveSetBuilder _moveSetBuilder;
        public SpeciesBuilder()
        {
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
        public SpeciesBuilder WithEvolutionSpecies(Species evolutionSpecies)
        {
            _evolutionSpecies = evolutionSpecies;
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
            return new Species(
                _number,
                _name,
                _type,
                _baseStats,
                _maleFactor,
                _evolutionSpecies,
                _moveSetBuilder.Build()
            );
        }
    }
    public class MoveSetBuilder
    {
        private List<MoveByLevel> _moveSet = new List<MoveByLevel>();

        public MoveSetBuilder()
        {
            _moveSet.Add(new MoveByLevel(new Level(1), new Move("Tackle", PokemonType.Normal, MoveCategory.Physical, 60, 0.9, 30)));
            _moveSet.Add(new MoveByLevel(new Level(2), new Move("Tail Whip", PokemonType.Normal, MoveCategory.Status, null, 1, 30)));
        }

        public MoveSetBuilder AddMove(MoveByLevel moveByLevel)
        {
            _moveSet.Add(moveByLevel);
            return this;
        }

        public MoveSetBuilder ResetMoves()
        {
            _moveSet.Clear();
            return this;
        }

        public List<MoveByLevel> Build()
        {
            return _moveSet;
        }
    }

}