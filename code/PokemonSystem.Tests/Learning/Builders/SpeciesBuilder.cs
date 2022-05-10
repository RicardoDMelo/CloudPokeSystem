using PokemonSystem.Common.Enums;
using PokemonSystem.Common.ValueObjects;
using PokemonSystem.Learning.Domain.PokemonAggregate;

namespace PokemonSystem.Tests.Learning.Builders
{
    public class SpeciesBuilder
    {
        private uint _number = 128;
        private string _name = "Tauros";

        private MoveSetBuilder _moveSetBuilder = new MoveSetBuilder();

        public SpeciesBuilder()
        {
            Reset();
        }

        public void Reset()
        {
            _number = 128;
            _name = "Tauros";
        }

        public SpeciesBuilder WithNumber(uint number)
        {
            _number = number;
            return this;
        }

        public SpeciesBuilder WithName(string name)
        {
            _name = name;
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
                _moveSetBuilder.Build()
            );
            Reset();
            return species;
        }
    }
}