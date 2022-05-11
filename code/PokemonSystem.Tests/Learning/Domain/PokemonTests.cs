using NUnit.Framework;
using PokemonSystem.Common.ValueObjects;
using PokemonSystem.Learning.Domain.PokemonAggregate;
using PokemonSystem.Learning.Domain.SpeciesAggregate;
using PokemonSystem.Tests.Learning.Builders;
using PokemonSystem.Tests.ValueObjects;

namespace PokemonSystem.Tests.Learning.Domain
{
    public class PokemonTests
    {
        private SpeciesBuilder? _speciesBuilder;
        private MoveSetBuilder? _moveSetBuilder;

        [SetUp]
        public void Setup()
        {
            _speciesBuilder = new SpeciesBuilder();
            _moveSetBuilder = new MoveSetBuilder();
        }

        [Test]
        public void Create_Pokemon()
        {
            var species = _speciesBuilder!.Build();
            var level = new Level(10);

            var pokemon = new Pokemon(species, level);

            Assert.AreEqual(species, pokemon.PokemonSpecies);
            Assert.AreEqual(level, pokemon.Level);
        }

        [Test]
        public void Created_Pokemon_Should_Learn_Moves()
        {
            _moveSetBuilder!.ResetMoves();
            _moveSetBuilder!.AddMove(new MoveByLevel(Levels.One, Moves.Move1));
            _speciesBuilder!.WithMoveSet(_moveSetBuilder.Build());
            var species = _speciesBuilder!.Build();
            var level = new Level(1);

            var pokemon = new Pokemon(species, level);

            Assert.AreEqual(pokemon.LearntMoves.Count, 1);
        }

        [Test]
        public void Created_Pokemon_Should_Learn_Four_Moves()
        {
            _moveSetBuilder!.ResetMoves();
            _moveSetBuilder!.AddMove(new MoveByLevel(Levels.One, Moves.Move1));
            _moveSetBuilder!.AddMove(new MoveByLevel(Levels.Two, Moves.Move2));
            _moveSetBuilder!.AddMove(new MoveByLevel(Levels.Three, Moves.Move3));
            _moveSetBuilder!.AddMove(new MoveByLevel(Levels.Four, Moves.Move4));
            _speciesBuilder!.WithMoveSet(_moveSetBuilder.Build());
            var species = _speciesBuilder!.Build();
            var level = new Level(4);

            var pokemon = new Pokemon(species, level);

            Assert.AreEqual(pokemon.LearntMoves.Count, 4);
        }

        [Test]
        public void Created_Pokemon_Should_Throw_Exception_Trying_To_Grow_To_Lower_Level()
        {
            var species = _speciesBuilder!.Build();

            var pokemon = new Pokemon(species, Levels.Ten);

            Assert.Throws<ArgumentOutOfRangeException>(() => pokemon.GrowToLevel(Levels.One));
        }

        [Test]
        public void Created_Pokemon_Could_Learn_Advanced_Moves()
        {
            _moveSetBuilder!.ResetMoves();
            _moveSetBuilder!.AddMove(new MoveByLevel(Levels.One, Moves.Move1));
            _moveSetBuilder!.AddMove(new MoveByLevel(Levels.Two, Moves.Move2));
            _moveSetBuilder!.AddMove(new MoveByLevel(Levels.Three, Moves.Move3));
            _moveSetBuilder!.AddMove(new MoveByLevel(Levels.Four, Moves.Move4));
            _moveSetBuilder!.AddMove(new MoveByLevel(Levels.Five, Moves.Move5));
            _speciesBuilder!.WithMoveSet(_moveSetBuilder.Build());
            var species = _speciesBuilder!.Build();
            var level = new Level(5);

            var pokemons = new List<Pokemon>();
            for (int i = 0; i < 100; i++)
            {
                var pokemon = new Pokemon(species, level);
                pokemons.Add(pokemon);
            }

            var learntBasicMoves = pokemons.Where(x => !x.LearntMoves.Values.Any(y => y == Moves.Move5)).Count();
            var learntAdvancedMoves = pokemons.Where(x => x.LearntMoves.Values.Any(y => y == Moves.Move5)).Count();

            Console.WriteLine($"Learnt Basic Moves Count: {learntBasicMoves}");
            Console.WriteLine($"Learnt Advanced Moves Count: {learntAdvancedMoves}");

            Assert.AreNotEqual(0, learntBasicMoves);
            Assert.AreNotEqual(0, learntAdvancedMoves);
        }

        [Test]
        public void Created_Pokemon_Could_Learn_TM_Moves()
        {
            _moveSetBuilder!.ResetMoves();
            _moveSetBuilder!.AddMove(new MoveByLevel(Levels.One, Moves.Move1));
            _moveSetBuilder!.AddMove(new MoveByLevel(null, Moves.Move1));
            _speciesBuilder!.WithMoveSet(_moveSetBuilder.Build());
            var species = _speciesBuilder!.Build();
            var level = new Level(1);

            var pokemons = new List<Pokemon>();
            for (int i = 0; i < 100; i++)
            {
                var pokemon = new Pokemon(species, level);
                pokemons.Add(pokemon);
            }

            var learntBaseMoves = pokemons.Where(x => x.LearntMoves.Count == 1).Count();
            var learntTmMoves = pokemons.Where(x => x.LearntMoves.Count == 2).Count();

            Console.WriteLine($"Learnt Base Moves Only Count: {learntBaseMoves}");
            Console.WriteLine($"Learnt TM Moves Count: {learntTmMoves}");

            Assert.AreNotEqual(0, learntBaseMoves);
            Assert.AreNotEqual(0, learntTmMoves);
        }
    }
}