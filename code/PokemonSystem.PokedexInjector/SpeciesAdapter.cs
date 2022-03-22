using PokemonSystem.Common.Enums;
using PokemonSystem.Common.ValueObjects;
using PokemonSystem.Incubator.Domain.SpeciesAggregate;
using PokemonSystem.PokedexInjector.Dtos;

namespace PokemonSystem.PokedexInjector
{
    internal class SpeciesAdapter
    {
        public static IEnumerable<Species> ConvertToDomain(ImportDto importDto)
        {
            foreach (var spDto in importDto.Species)
            {
                yield return ConvertToDomain(spDto, importDto);
            }
        }

        private static Species ConvertToDomain(SpeciesImportDto speciesDto, ImportDto importDto)
        {
            Typing typing = new(speciesDto.Type1, speciesDto.Type2);
            Stats stats = new(speciesDto.HP, speciesDto.Attack, speciesDto.Defense, speciesDto.SpecialAttack, speciesDto.SpecialDefense, speciesDto.Speed);
            var moveSet = ConvertToDomain(importDto.MoveSets.First(x => x.Id == speciesDto.Id), importDto.Moves);

            List<EvolutionCriteria> evolutionCriterias = new List<EvolutionCriteria>();
            var evolutionSpeciesDtoList = importDto.Species.Where(x => x.PreEvolution == speciesDto.Name);
            if (evolutionSpeciesDtoList != null && evolutionSpeciesDtoList.Any())
            {
                foreach (var evolutionSpeciesDto in evolutionSpeciesDtoList)
                {
                    var evolvedSpecies = ConvertToDomain(evolutionSpeciesDto, importDto);
                    var evolutionDtos = importDto.Evolutions.Where(x => x.From == speciesDto.Name && x.To == evolvedSpecies.Name);
                    if (evolutionDtos != null && evolutionDtos.Any())
                    {
                        foreach (var evolutionDto in evolutionDtos)
                        {
                            evolutionCriterias.Add(ConvertToDomain(evolutionDto, evolvedSpecies));
                        }
                    }
                }
            }

            return new(speciesDto.Id, speciesDto.Name, typing, stats, speciesDto.MaleFactor, evolutionCriterias, moveSet.ToList());
        }

        public static IEnumerable<MoveByLevel> ConvertToDomain(MoveSetImportDto moveSetDto, IEnumerable<MoveImportDto> allMoves)
        {
            foreach (var moveWithLevel in moveSetDto.MovesWithLevel)
            {
                string[] splitted = moveWithLevel.Split(" - ");
                uint level = 1;
                if (splitted[0].Contains("L"))
                {
                    level = Convert.ToUInt32(splitted[0].Replace("L", string.Empty));
                }

                string moveName = splitted[1];
                var moveDto = allMoves.FirstOrDefault(x => x.Name == moveName);
                if (moveDto != null)
                {
                    yield return new MoveByLevel(new Level(level), ConvertToDomain(moveDto));
                }
            }
        }

        private static Move ConvertToDomain(MoveImportDto moveDto)
        {
            return new Move(moveDto.Name, moveDto.Type, moveDto.Category, moveDto.Power, moveDto.Accuracy, moveDto.PP);
        }

        private static EvolutionCriteria ConvertToDomain(EvolutionImportDto evolutionDto, Species species)
        {
            switch (evolutionDto.Type)
            {
                case EvolutionType.Level:
                    return EvolutionCriteria.CreateLevelEvolution(evolutionDto.Level, species);
                case EvolutionType.Item:
                    return EvolutionCriteria.CreateItemEvolution(evolutionDto.Item, species);
                case EvolutionType.Trading:
                    return EvolutionCriteria.CreateTradingEvolution(species);
            }
            throw new Exception("No evolution type found");
        }
    }
}
