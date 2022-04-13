using NUnit.Framework;
using PokemonSystem.Common.ValueObjects;
using PokemonSystem.Evolution.Domain.PokemonAggregate;
using PokemonSystem.Tests.Evolution.Builders;
using PokemonSystem.Tests.ValueObjects;

namespace PokemonSystem.Tests.Evolution
{
    public class PokemonTests
    {
        private SpeciesBuilder? _speciesBuilder;
        private EvolutionCriteriaBuilder? _evolutionCriteriaBuilder;

        [SetUp]
        public void Setup()
        {
            _speciesBuilder = new SpeciesBuilder();
            _evolutionCriteriaBuilder = new EvolutionCriteriaBuilder();
        }

        [Test]
        public void Create_Pokemon_Level_One()
        {
            uint levelOneExperience = 1;

            var species = _speciesBuilder!.Build();

            var pokemon = new Pokemon(species, Levels.One);

            Assert.AreEqual(species, pokemon.PokemonSpecies);
            Assert.AreEqual(levelOneExperience, pokemon.Experience);
            Assert.AreEqual(Levels.One, pokemon.Level);
        }

        [Test]
        public void Create_Pokemon_Level_Ten()
        {
            uint levelTenExperience = 1_000;
            var species = _speciesBuilder!.Build();

            var pokemon = new Pokemon(species, Levels.Ten);

            Assert.AreEqual(species, pokemon.PokemonSpecies);
            Assert.AreEqual(levelTenExperience, pokemon.Experience);
            Assert.AreEqual(Levels.Ten, pokemon.Level);
        }

        [Test]
        public void Add_Experience_To_Pokemon_To_Level_10()
        {
            var species = _speciesBuilder!.Build();
            uint experience = 999;

            var pokemon = new Pokemon(species, Levels.One);
            pokemon.TryAddExperience(experience);

            Assert.AreEqual(experience + 1, pokemon.Experience);
            Assert.AreEqual(Levels.Ten, pokemon.Level);
        }

        [Test]
        public void Add_Experience_To_Pokemon_To_Almost_Level_10()
        {
            var species = _speciesBuilder!.Build();
            uint experience = 998;

            var pokemon = new Pokemon(species, Levels.One);
            pokemon.TryAddExperience(experience);

            Assert.AreEqual(experience + 1, pokemon.Experience);
            Assert.AreEqual(new Level(9), pokemon.Level);
        }

        [Test]
        public void Add_Experience_To_Pokemon_To_Level_100()
        {
            var species = _speciesBuilder!.Build();
            uint experience = 1_999_999;

            var pokemon = new Pokemon(species, Levels.One);
            pokemon.TryAddExperience(experience);

            Assert.AreEqual(Pokemon.MAX_EXPERIENCE, pokemon.Experience);
            Assert.AreEqual(Levels.Max, pokemon.Level);
        }

        [Test]
        public void Add_Experience_To_Pokemon_Already_Level_100()
        {
            var species = _speciesBuilder!.Build();
            uint experience = 999_999;

            var pokemon = new Pokemon(species, Levels.One);
            Assert.True(pokemon.TryAddExperience(experience));
            Assert.False(pokemon.TryAddExperience(experience));

            Assert.AreEqual(Pokemon.MAX_EXPERIENCE, pokemon.Experience);
            Assert.AreEqual(Levels.Max, pokemon.Level);
        }

        [Test]
        public void Pokemon_Should_Have_Stats()
        {
            var species = _speciesBuilder!.Build();

            var pokemon = new Pokemon(species, Levels.One);

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

            var pokemon = new Pokemon(species, Levels.One);

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
        public void Pokemon_Should_Not_Evolve()
        {
            var evolutionSpecies = _speciesBuilder!
                .WithName("Super Tauros")
                .WithNumber(10)
                .Build();

            var evolutionCriteria = _evolutionCriteriaBuilder!
                .WithMinimumLevel(Levels.Max)
                .WithSpecies(evolutionSpecies)
                .Build();

            var species = _speciesBuilder
                .WithEvolutionCriterias(new List<EvolutionCriteria>() { evolutionCriteria })
                .Build();

            uint experience = 8;

            var pokemon = new Pokemon(species, Levels.One);
            pokemon.TryAddExperience(experience);

            Assert.AreEqual(Levels.Two, pokemon.Level);
            Assert.AreNotEqual(evolutionSpecies, pokemon.PokemonSpecies);
        }

        [Test]
        public void Pokemons_Should_Sometimes_Evolve()
        {
            var evolutionSpecies = _speciesBuilder!
                .WithName("Super Tauros")
                .WithNumber(10)
                .Build();

            var evolutionCriteria = _evolutionCriteriaBuilder!
                .WithMinimumLevel(Levels.Two)
                .WithSpecies(evolutionSpecies)
                .Build();

            var species = _speciesBuilder
                .WithEvolutionCriterias(new List<EvolutionCriteria>() { evolutionCriteria })
                .Build();

            var pokemons = new List<Pokemon>();
            for (int i = 0; i < 5000; i++)
            {
                var pokemon = new Pokemon(species, Levels.Ten);
                pokemons.Add(pokemon);
            }

            var basePokemons = pokemons.Where(x => x.PokemonSpecies.Equals(species)).Count();
            var evolvedPokemons = pokemons.Where(x => x.PokemonSpecies.Equals(evolutionSpecies)).Count();

            Console.WriteLine($"Not Evolved Count:{basePokemons}");
            Console.WriteLine($"Evolved Count:{evolvedPokemons}");

            Assert.AreNotEqual(0, basePokemons);
            Assert.AreNotEqual(0, evolvedPokemons);
        }

        [Test]
        public void Low_Level_Pokemons_Should_Sometimes_Evolve_To_Last_Evolution()
        {
            var megaTauros = _speciesBuilder!
                .WithName("Mega Tauros")
                .WithNumber(20)
                .Build();

            var evolutionCriteriaSuperTauros = _evolutionCriteriaBuilder!
                .WithMinimumLevel(Levels.Twenty)
                .WithSpecies(megaTauros)
                .Build();

            var superTauros = _speciesBuilder!
                .WithName("Super Tauros")
                .WithNumber(10)
                .WithEvolutionCriterias(new List<EvolutionCriteria>() { evolutionCriteriaSuperTauros })
                .Build();

            var evolutionCriteriaTauros = _evolutionCriteriaBuilder!
                .WithMinimumLevel(Levels.Ten)
                .WithSpecies(superTauros)
                .Build();

            var species = _speciesBuilder
                .WithEvolutionCriterias(new List<EvolutionCriteria>() { evolutionCriteriaTauros })
                .Build();

            var pokemons = new List<Pokemon>();
            for (int i = 0; i < 5000; i++)
            {
                var pokemon = new Pokemon(species, Levels.Max);
                pokemons.Add(pokemon);
            }

            var basePokemons = pokemons.Where(x => x.PokemonSpecies.Equals(species)).Count();
            var firstEvolutionPokemons = pokemons.Where(x => x.PokemonSpecies.Equals(superTauros)).Count();
            var secondEvolutionPokemons = pokemons.Where(x => x.PokemonSpecies.Equals(megaTauros)).Count();

            Console.WriteLine($"Not Evolved Count:{basePokemons}");
            Console.WriteLine($"First Evolution Count:{firstEvolutionPokemons}");
            Console.WriteLine($"Second Evolution Count:{secondEvolutionPokemons}");

            Assert.AreNotEqual(0, basePokemons);
            Assert.AreNotEqual(0, firstEvolutionPokemons);
            Assert.AreNotEqual(0, secondEvolutionPokemons);
        }

        [Test]
        public void High_Level_Pokemons_Should_Sometimes_Evolve_To_Last_Evolution()
        {
            var megaTauros = _speciesBuilder!
                .WithName("Mega Tauros")
                .WithNumber(20)
                .Build();

            var evolutionCriteriaSuperTauros = _evolutionCriteriaBuilder!
                .WithMinimumLevel(new Level(60))
                .WithSpecies(megaTauros)
                .Build();

            var superTauros = _speciesBuilder!
                .WithName("Super Tauros")
                .WithNumber(10)
                .WithEvolutionCriterias(new List<EvolutionCriteria>() { evolutionCriteriaSuperTauros })
                .Build();

            var evolutionCriteriaTauros = _evolutionCriteriaBuilder!
                .WithMinimumLevel(new Level(40))
                .WithSpecies(superTauros)
                .Build();

            var species = _speciesBuilder
                .WithEvolutionCriterias(new List<EvolutionCriteria>() { evolutionCriteriaTauros })
                .Build();

            var pokemons = new List<Pokemon>();
            for (int i = 0; i < 1000; i++)
            {
                var pokemon = new Pokemon(species, Levels.Max);
                pokemons.Add(pokemon);
            }

            var basePokemons = pokemons.Where(x => x.PokemonSpecies.Equals(species)).Count();
            var firstEvolutionPokemons = pokemons.Where(x => x.PokemonSpecies.Equals(superTauros)).Count();
            var secondEvolutionPokemons = pokemons.Where(x => x.PokemonSpecies.Equals(megaTauros)).Count();

            Console.WriteLine($"Not Evolved Count:{basePokemons}");
            Console.WriteLine($"First Evolution Count:{firstEvolutionPokemons}");
            Console.WriteLine($"Second Evolution Count:{secondEvolutionPokemons}");

            Assert.AreNotEqual(0, basePokemons);
            Assert.AreNotEqual(0, firstEvolutionPokemons);
            Assert.AreNotEqual(0, secondEvolutionPokemons);
        }


        [Test]
        public void Evolved_Pokemon_Should_Have_Different_Stats()
        {
            var evolutionSpecies = _speciesBuilder!
                .WithName("Super Tauros")
                .WithNumber(10)
                .WithBaseStats(new Stats(10, 50, 60, 70, 80, 90))
                .Build();

            var evolutionCriteria = _evolutionCriteriaBuilder!
                .WithMinimumLevel(Levels.Two)
                .WithSpecies(evolutionSpecies)
                .Build();

            var species = _speciesBuilder
                .WithEvolutionCriterias(new List<EvolutionCriteria>() { evolutionCriteria })
                .Build();

            var pokemon = new Pokemon(species, Levels.One);

            Assert.AreEqual(11, pokemon.Stats.HP);
            Assert.AreEqual(5, pokemon.Stats.Attack);
            Assert.AreEqual(5, pokemon.Stats.Defense);
            Assert.AreEqual(5, pokemon.Stats.SpecialAttack);
            Assert.AreEqual(5, pokemon.Stats.SpecialDefense);
            Assert.AreEqual(5, pokemon.Stats.Speed);

            Pokemon pokemonEvolved;
            do
            {
                pokemonEvolved = new Pokemon(species, Levels.Max);
            } while (pokemonEvolved.PokemonSpecies == pokemon.PokemonSpecies);

            Assert.AreEqual(Levels.Max, pokemonEvolved.Level);
            Assert.AreEqual(evolutionSpecies, pokemonEvolved.PokemonSpecies);

            Assert.AreEqual(130, pokemonEvolved.Stats.HP);
            Assert.AreEqual(105, pokemonEvolved.Stats.Attack);
            Assert.AreEqual(125, pokemonEvolved.Stats.Defense);
            Assert.AreEqual(145, pokemonEvolved.Stats.SpecialAttack);
            Assert.AreEqual(165, pokemonEvolved.Stats.SpecialDefense);
            Assert.AreEqual(185, pokemonEvolved.Stats.Speed);
        }
    }
}