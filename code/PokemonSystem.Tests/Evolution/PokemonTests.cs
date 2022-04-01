using NUnit.Framework;
using PokemonSystem.Common.Enums;
using PokemonSystem.Common.ValueObjects;
using PokemonSystem.Evolution.Domain.PokemonAggregate;
using PokemonSystem.Tests.Evolution.Builders;
using PokemonSystem.Tests.ValueObjects;
using System;

namespace PokemonSystem.Tests.Evolution
{
    public class PokemonTests
    {
        private SpeciesBuilder? _speciesBuilder;

        [SetUp]
        public void Setup()
        {
            _speciesBuilder = new SpeciesBuilder();
        }

        [Test]
        public void Create_Pokemon()
        {
            var species = _speciesBuilder!.Build();

            var pokemon = new Pokemon(species);

            Assert.AreEqual(species, pokemon.PokemonSpecies);
            Assert.Zero(pokemon.Experience);
            Assert.AreEqual(Levels.One, pokemon.Level);
        }

        [Test]
        public void Add_Experience_To_Pokemon_To_Level_10()
        {
            var species = _speciesBuilder!.Build();
            uint experience = 1_000;

            var pokemon = new Pokemon(species);
            pokemon.TryAddExperience(experience);

            Assert.AreEqual(experience, pokemon.Experience);
            Assert.AreEqual(Levels.Ten, pokemon.Level);
        }

        [Test]
        public void Add_Experience_To_Pokemon_To_Almost_Level_10()
        {
            var species = _speciesBuilder!.Build();
            uint experience = 999;

            var pokemon = new Pokemon(species);
            pokemon.TryAddExperience(experience);

            Assert.AreEqual(experience, pokemon.Experience);
            Assert.AreEqual(new Level(9), pokemon.Level);
        }

        [Test]
        public void Add_Experience_To_Pokemon_To_Level_100()
        {
            var species = _speciesBuilder!.Build();
            uint experience = 2_000_000;

            var pokemon = new Pokemon(species);
            pokemon.TryAddExperience(experience);

            Assert.AreEqual(Pokemon.MAX_EXPERIENCE, pokemon.Experience);
            Assert.AreEqual(Levels.Max, pokemon.Level);
        }

        [Test]
        public void Add_Experience_To_Pokemon_Already_Level_100()
        {
            var species = _speciesBuilder!.Build();
            uint experience = 1_000_000;

            var pokemon = new Pokemon(species);
            Assert.True(pokemon.TryAddExperience(experience));
            Assert.False(pokemon.TryAddExperience(experience));

            Assert.AreEqual(Pokemon.MAX_EXPERIENCE, pokemon.Experience);
            Assert.AreEqual(Levels.Max, pokemon.Level);
        }

        [Test]
        public void Pokemon_Should_Have_Stats()
        {
            var species = _speciesBuilder!.Build();

            var pokemon = new Pokemon(species);

            Assert.AreEqual(11, pokemon.Stats.HP);
            Assert.AreEqual(5, pokemon.Stats.Attack);
            Assert.AreEqual(5, pokemon.Stats.Defense);
            Assert.AreEqual(5, pokemon.Stats.SpecialAttack);
            Assert.AreEqual(5, pokemon.Stats.SpecialDefense);
            Assert.AreEqual(5, pokemon.Stats.Speed);
        }

        [Test]
        public void Pokemon_Stats_Should_Upgrade_On_Level_Up()
        {
            var species = _speciesBuilder!.Build();

            var pokemon = new Pokemon(species);

            Assert.AreEqual(11, pokemon.Stats.HP);
            Assert.AreEqual(5, pokemon.Stats.Attack);
            Assert.AreEqual(5, pokemon.Stats.Defense);
            Assert.AreEqual(5, pokemon.Stats.SpecialAttack);
            Assert.AreEqual(5, pokemon.Stats.SpecialDefense);
            Assert.AreEqual(5, pokemon.Stats.Speed);

            uint experience = 1_000_000;
            pokemon.TryAddExperience(experience);

            Assert.AreEqual(Levels.Max, pokemon.Level);
            Assert.AreEqual(112, pokemon.Stats.HP);
            Assert.AreEqual(9, pokemon.Stats.Attack);
            Assert.AreEqual(11, pokemon.Stats.Defense);
            Assert.AreEqual(13, pokemon.Stats.SpecialAttack);
            Assert.AreEqual(15, pokemon.Stats.SpecialDefense);
            Assert.AreEqual(17, pokemon.Stats.Speed);
        }

        [Test]
        public void Pokemon_Should_Evolve()
        {
            var evolutionSpecies = _speciesBuilder!
                .WithName("Super Tauros")
                .WithNumber(10)
                .Build();

            var species = _speciesBuilder
                .WithEvolutionCriteria(EvolutionType.Level, Levels.Two, evolutionSpecies)
                .Build();

            uint experience = 8;

            var pokemon = new Pokemon(species);
            pokemon.TryAddExperience(experience);

            Assert.AreEqual(Levels.Two, pokemon.Level);
            Assert.AreEqual(evolutionSpecies, pokemon.PokemonSpecies);
        }

        [Test]
        public void Evolved_Pokemon_Should_Have_Different_Stats()
        {
            var evolutionSpecies = _speciesBuilder!
                .WithName("Super Tauros")
                .WithNumber(10)
                .WithBaseStats(new Stats(10, 50, 60, 70, 80, 90))
                .Build();

            var species = _speciesBuilder
                .WithEvolutionCriteria(EvolutionType.Level, Levels.Two, evolutionSpecies)
                .Build();

            var pokemon = new Pokemon(species);

            Assert.AreEqual(11, pokemon.Stats.HP);
            Assert.AreEqual(5, pokemon.Stats.Attack);
            Assert.AreEqual(5, pokemon.Stats.Defense);
            Assert.AreEqual(5, pokemon.Stats.SpecialAttack);
            Assert.AreEqual(5, pokemon.Stats.SpecialDefense);
            Assert.AreEqual(5, pokemon.Stats.Speed);


            uint experience = 8;
            pokemon.TryAddExperience(experience);

            Assert.AreEqual(Levels.Two, pokemon.Level);
            Assert.AreEqual(evolutionSpecies, pokemon.PokemonSpecies);

            Assert.AreEqual(12, pokemon.Stats.HP);
            Assert.AreEqual(7, pokemon.Stats.Attack);
            Assert.AreEqual(7, pokemon.Stats.Defense);
            Assert.AreEqual(7, pokemon.Stats.SpecialAttack);
            Assert.AreEqual(8, pokemon.Stats.SpecialDefense);
            Assert.AreEqual(8, pokemon.Stats.Speed);
        }
    }
}