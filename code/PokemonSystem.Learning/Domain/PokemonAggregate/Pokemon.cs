using PokemonSystem.Common.SeedWork.Domain;
using PokemonSystem.Common.ValueObjects;
using PokemonSystem.Learning.Domain.SpeciesAggregate;

namespace PokemonSystem.Learning.Domain.PokemonAggregate
{
    public class Pokemon : Entity, IAggregateRoot
    {
        private Pokemon(){}

        public Pokemon(Guid id, Species pokemonSpecies, Level level)
        {
            Id = id;
            PokemonSpecies = pokemonSpecies ?? throw new ArgumentNullException(nameof(pokemonSpecies));
            LearntMoves = new LearntMoves();
            Level = new Level(1);
            LearnLevelOneMoves();
            GrowToLevel(level);
        }

        public Species PokemonSpecies { get; protected set; }
        public Level Level { get; protected set; }
        public LearntMoves LearntMoves { get; protected set; }

        private const double TM_CHANCE = 0.02;
        private const double DEFAULT_CHANCE = 0.6;


        public void GrowToLevel(Level level)
        {
            if (level < Level)
            {
                throw new ArgumentOutOfRangeException(nameof(level));
            }

            var oldLevel = Level;
            Level = level;
            for (uint i = oldLevel.Value + 1; i <= Level.Value; i++)
            {
                var availableMoves = PokemonSpecies.MoveSet.Where(x => x.Level is null || x.Level.Value == i);
                foreach (var moveByLevel in availableMoves)
                {
                    TryToLearnMove(moveByLevel);
                }
            }
        }

        private void LearnLevelOneMoves()
        {
            var moves = PokemonSpecies.MoveSet.Where(x => x.Level is not null && x.Level.Value == 1).ToList();
            if (moves.Any())
            {
                var moveIndex = Random.Shared.Next(0, moves.Count - 1);
                var moveByLevel = PokemonSpecies.MoveSet.ElementAt(moveIndex);
                LearntMoves.AddMove(moveByLevel.Move);
                AddDomainEvent(new PokemonLearnedMovesDomainEvent(this));
            }

            var availableMoves = PokemonSpecies.MoveSet.Where(x => x.Level is null);
            foreach (var tmMoveByLevel in availableMoves)
            {
                TryToLearnMove(tmMoveByLevel);
            }
        }

        private void TryToLearnMove(MoveByLevel moveByLevel)
        {
            double chanceFactor;

            if (moveByLevel.Level is null)
            {
                chanceFactor = TM_CHANCE;
            }
            else
            {
                chanceFactor = DEFAULT_CHANCE;
            }

            if (moveByLevel.Level is null || LearntMoves.Values.Count == LearntMoves.MAX_MOVES)
            {
                var currentRandom = Random.Shared.NextDouble();
                if (currentRandom < chanceFactor)
                {
                    if (LearntMoves.Values.Count == LearntMoves.MAX_MOVES)
                    {
                        ForgetRandomMove();
                    }
                    LearntMoves.AddMove(moveByLevel.Move);
                    AddDomainEvent(new PokemonLearnedMovesDomainEvent(this));
                }
            }
            else
            {
                LearntMoves.AddMove(moveByLevel.Move);
                AddDomainEvent(new PokemonLearnedMovesDomainEvent(this));
            }
        }

        private void ForgetRandomMove()
        {
            var forgottenMoveIndex = Random.Shared.Next(0, LearntMoves.MAX_MOVES);
            var forgottenMove = LearntMoves.Values.ElementAt(forgottenMoveIndex);
            LearntMoves.ForgetMove(forgottenMove);
        }
    }
}
