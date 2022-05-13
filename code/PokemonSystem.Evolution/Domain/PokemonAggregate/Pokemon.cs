using PokemonSystem.Common.Enums;
using PokemonSystem.Common.SeedWork.Domain;
using PokemonSystem.Common.ValueObjects;

namespace PokemonSystem.Evolution.Domain.PokemonAggregate
{
    public class Pokemon : Entity, IAggregateRoot
    {
        public const uint MAX_EXPERIENCE = 1_000_000;
        public const uint MIN_LEVEL = 1;
        public const uint MAX_LEVEL = 100;

        /*
         * About EVOLUTION_FACTOR
         * This factor goes from 0 to 1.
         * Lesser values turn evolutions more easily to occur.
         * Greater values turn them more rarer
        */
        private const double EVOLUTION_FACTOR = 0.9;

        public Pokemon(Guid id, Species pokemonSpecies, Level levelToGrow)
        {
            Id = id;
            PokemonSpecies = pokemonSpecies ?? throw new ArgumentNullException(nameof(pokemonSpecies));
            Level = new Level(1);
            Experience = GetExperienceFromLevel(levelToGrow);
            CheckForNewLevel();
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
                if (summedExperience >= MAX_EXPERIENCE)
                {
                    Experience = MAX_EXPERIENCE;
                }
                else
                {
                    Experience = summedExperience;
                }

                CheckForNewLevel();

                return true;
            }
            return false;
        }

        #region Private Methods

        private void CheckForNewLevel()
        {
            var oldLevel = Level;
            var newLevel = GetLevelFromCurrentExperience();

            if (oldLevel < newLevel)
            {
                for (uint levelIterator = oldLevel.Value + 1; levelIterator <= newLevel.Value; levelIterator++)
                {
                    Level = new Level(levelIterator);
                    AddDomainEvent(new PokemonLevelRaisedDomainEvent(this, Id, Level.Value, PokemonSpecies.Id));
                    CheckForEvolution();
                }
            }
        }

        private void CheckForEvolution()
        {
            var evolutions = PokemonSpecies.EvolutionCriterias
                .Where(x => x.EvolutionType == EvolutionType.Trading ||
                            x.EvolutionType == EvolutionType.Item ||
                           (x.EvolutionType == EvolutionType.Level && x.MinimumLevel! <= Level))
                .ToList();

            if (evolutions.Any())
            {
                var deltaMaxCurrent = (MAX_LEVEL - (Level.Value / 15));
                double currentRandomChance = Random.Shared.Next(0, (int)deltaMaxCurrent) / 100d;

                if (currentRandomChance > EVOLUTION_FACTOR)
                {
                    int index = Random.Shared.Next(evolutions.Count());
                    PokemonSpecies = evolutions[index].Species;
                    AddDomainEvent(new PokemonEvolvedDomainEvent(this, Id, Level.Value, PokemonSpecies.Id));
                }
            }
        }

        private Level GetLevelFromCurrentExperience()
        {
            var currentExperience = Experience == 0 ? 1 : Experience;
            return new Level((uint)Math.Round(Math.Pow(currentExperience, 1.0 / 3), 10));
        }

        private uint GetExperienceFromLevel(Level level)
        {
            return (uint)Math.Pow(level.Value, 3);
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
