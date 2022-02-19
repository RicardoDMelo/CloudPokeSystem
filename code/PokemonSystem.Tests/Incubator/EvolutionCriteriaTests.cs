using NUnit.Framework;
using PokemonSystem.Common.Enums;
using PokemonSystem.Incubator.Domain.SpeciesAggregate;
using PokemonSystem.Tests.Incubator.Builders;
using PokemonSystem.Tests.ValueObjects;
using System;

namespace PokemonSystem.Tests.Incubator
{
    public class EvolutionCriteriaTests
    {
        [Test]
        public void Constructor_Null_Exception()
        {
            EvolutionType evolutionType = EvolutionType.Level;
            Species evolutionSpecies = new SpeciesBuilder().Build();

            Assert.Throws<ArgumentNullException>(() => new EvolutionCriteria(evolutionType, null, evolutionSpecies));
            Assert.Throws<ArgumentNullException>(() => new EvolutionCriteria(evolutionType, Levels.One, null));
        }

        [Test]
        public void Create_EvolutionCriteria_By_Level()
        {
            EvolutionType evolutionType = EvolutionType.Level;
            Species evolutionSpecies = new SpeciesBuilder().Build();

            var evolutionCriteria = new EvolutionCriteria(evolutionType, Levels.One, evolutionSpecies);

            Assert.AreEqual(evolutionType, evolutionCriteria.EvolutionType);
            Assert.AreEqual(Levels.One, evolutionCriteria.MinimumLevel);
            Assert.AreEqual(evolutionSpecies, evolutionCriteria.EvolutionSpecies);
        }

        [Test]
        public void Create_EvolutionCriteria_By_Item()
        {
            EvolutionType evolutionType = EvolutionType.Item;
            Species evolutionSpecies = new SpeciesBuilder().Build();

            var evolutionCriteria = new EvolutionCriteria(evolutionType, null, evolutionSpecies);

            Assert.AreEqual(evolutionType, evolutionCriteria.EvolutionType);
            Assert.IsNull(evolutionCriteria.MinimumLevel);
            Assert.AreEqual(evolutionSpecies, evolutionCriteria.EvolutionSpecies);
        }
    }
}