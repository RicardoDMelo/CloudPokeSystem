using NUnit.Framework;
using PokemonSystem.Common.Enums;
using PokemonSystem.Incubator.Domain.SpeciesAggregate;
using PokemonSystem.Tests.Incubator.Builders;
using PokemonSystem.Tests.ValueObjects;
using System;

namespace PokemonSystem.Tests.Incubator.Domain
{
    public class EvolutionCriteriaTests
    {
        [Test]
        public void Create_EvolutionCriteria_By_Level()
        {
            EvolutionType evolutionType = EvolutionType.Level;
            var level = Levels.One;
            Species evolutionSpecies = new SpeciesBuilder().Build();

            var evolutionCriteria = EvolutionCriteria.CreateLevelEvolution(level, evolutionSpecies);

            Assert.AreEqual(evolutionType, evolutionCriteria.EvolutionType);
            Assert.AreEqual(level, evolutionCriteria.MinimumLevel);
            Assert.IsNull(evolutionCriteria.Item);
            Assert.AreEqual(evolutionSpecies, evolutionCriteria.Species);
        }

        [Test]
        public void Create_EvolutionCriteria_By_Item()
        {
            EvolutionType evolutionType = EvolutionType.Item;
            string itemName = "Moon Stone";
            Species evolutionSpecies = new SpeciesBuilder().Build();

            var evolutionCriteria = EvolutionCriteria.CreateItemEvolution(itemName, evolutionSpecies);

            Assert.AreEqual(evolutionType, evolutionCriteria.EvolutionType);
            Assert.IsNull(evolutionCriteria.MinimumLevel);
            Assert.AreEqual(itemName, evolutionCriteria.Item);
            Assert.AreEqual(evolutionSpecies, evolutionCriteria.Species);
        }

        [Test]
        public void Create_EvolutionCriteria_By_Trading()
        {
            EvolutionType evolutionType = EvolutionType.Trading;
            Species evolutionSpecies = new SpeciesBuilder().Build();

            var evolutionCriteria = EvolutionCriteria.CreateTradingEvolution(evolutionSpecies);

            Assert.AreEqual(evolutionType, evolutionCriteria.EvolutionType);
            Assert.IsNull(evolutionCriteria.MinimumLevel);
            Assert.IsNull(evolutionCriteria.Item);
            Assert.AreEqual(evolutionSpecies, evolutionCriteria.Species);
        }
    }
}