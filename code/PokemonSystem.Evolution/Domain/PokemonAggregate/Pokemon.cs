using PokemonSystem.Common.Enums;
using PokemonSystem.Common.SeedWork.Domain;
using PokemonSystem.Common.ValueObjects;
using PokemonSystem.Evolution.Domain.Events;
using System;

namespace PokemonSystem.Evolution.Domain.PokemonAggregate
{
    public class Pokemon : Entity, IAggregateRoot
    {
        public const uint MAX_EXPERIENCE = 1_000_000;

        public Pokemon(Species pokemonSpecies)
        {
            PokemonSpecies = pokemonSpecies ?? throw new ArgumentNullException(nameof(pokemonSpecies));
            Level = GetLevelFromExperience();
        }

        public Species PokemonSpecies { get; private set; }
        public Stats Stats { get => GetCalculcatedStats(); }
        public uint Experience { get; private set; }
        public Level Level { get; private set; }

        public bool TryAddExperience(uint gainedExperience)
        {
            if (Experience < MAX_EXPERIENCE)
            {
                var summedExperience = Experience + gainedExperience;
                if (summedExperience > MAX_EXPERIENCE)
                {
                    Experience = MAX_EXPERIENCE;
                }
                else
                {
                    Experience = summedExperience;
                }

                CheckForEvolution();
                return true;
            }
            return false;
        }

        #region Private Methods
        private void CheckForEvolution()
        {
            var oldLevel = Level;
            Level = GetLevelFromExperience();
            if (oldLevel < Level)
            {
                AddDomainEvent(new PokemonLevelRaisedDomainEvent(this));
                if (PokemonSpecies.EvolutionCriteria != null && PokemonSpecies.EvolutionCriteria.CanEvolveByLevel(Level))
                {
                    PokemonSpecies = PokemonSpecies.EvolutionCriteria.EvolutionSpecies;
                    AddDomainEvent(new PokemonEvolvedDomainEvent(this));
                }
            }
        }

        private Level GetLevelFromExperience()
        {
            var currentExperience = Experience == 0 ? 1 : Experience;
            return new Level((uint)Math.Round(Math.Pow(currentExperience, 1.0 / 3), 10));
        }

        private Stats GetCalculcatedStats()
        {
            return new Stats(
                CalculateHP(PokemonSpecies.BaseStats.HP),
                CalculateOtherStats(PokemonSpecies.BaseStats.Attack),
                CalculateOtherStats(PokemonSpecies.BaseStats.Defense),
                CalculateOtherStats(PokemonSpecies.BaseStats.SpecialAttack),
                CalculateOtherStats(PokemonSpecies.BaseStats.SpecialDefense),
                CalculateOtherStats(PokemonSpecies.BaseStats.Speed));
        }

        private uint CalculateHP(uint hp)
        {
            return (hp * 2 * Level.Value / 100) + Level.Value + 10;
        }

        private uint CalculateOtherStats(uint stat)
        {
            return (stat * 2 * Level.Value / 100) + 5;
        }
        #endregion
    }
}
