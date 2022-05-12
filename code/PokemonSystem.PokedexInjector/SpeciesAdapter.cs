using AutoMapper;
using PokemonSystem.Common.Enums;
using PokemonSystem.Common.ValueObjects;
using PokemonSystem.Incubator.Domain.SpeciesAggregate;
using PokemonSystem.Incubator.Infra.DatabaseDtos;
using PokemonSystem.PokedexInjector.Dtos;

namespace PokemonSystem.PokedexInjector
{
    internal class SpeciesAdapter
    {
        private readonly IMapper _mapper;

        public SpeciesAdapter()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Species, SpeciesDynamoDb>();
                cfg.CreateMap<Typing, TypingDynamoDb>();
                cfg.CreateMap<Stats, StatsDynamoDb>();
                cfg.CreateMap<Move, MoveDynamoDb>();
                cfg.CreateMap<MoveByLevel, MoveByLevelDynamoDb>();
                cfg.CreateMap<EvolutionCriteria, EvolutionCriteriaDynamoDb>();
                cfg.CreateMap<Guid, string>().ConvertUsing(f => f.ToString());
                cfg.CreateMap<Level, uint>().ConvertUsing(f => f.Value);
                cfg.CreateMap<Level?, uint?>().ConvertUsing((x, y) =>
                {
                    if (x is null)
                    {
                        y = null;
                    }
                    else
                    {
                        y = x.Value;
                    }
                    return y;
                });
            });

            config.AssertConfigurationIsValid();
            _mapper = config.CreateMapper();
        }

        public IEnumerable<SpeciesDynamoDb> ConvertToDto(IEnumerable<Species> speciesList)
        {
            foreach (var species in speciesList)
            {
                yield return _mapper.Map<SpeciesDynamoDb>(species);
            }
        }

        public IEnumerable<Species> ConvertToDomain(ImportDto importDto)
        {
            foreach (var spDto in importDto.Species)
            {
                yield return ConvertToDomain(spDto, importDto);
            }
        }

        private Species ConvertToDomain(SpeciesImportDto speciesDto, ImportDto importDto)
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

        private IEnumerable<MoveByLevel> ConvertToDomain(MoveSetImportDto moveSetDto, IEnumerable<MoveImportDto> allMoves)
        {
            foreach (var moveWithLevel in moveSetDto.MovesWithLevel)
            {
                string[] splitted = moveWithLevel.Split(" - ");
                uint? level = null;
                if (splitted[0].Contains("L"))
                {
                    level = Convert.ToUInt32(splitted[0].Replace("L", string.Empty));
                }
                else if (splitted[0].Contains("Start"))
                {
                    level = 1;
                }

                string moveName = splitted[1];
                var moveDto = allMoves.FirstOrDefault(x => x.Name == moveName);
                if (moveDto != null)
                {
                    yield return new MoveByLevel(level != null ? new Level(level.Value) : null, ConvertToDomain(moveDto));
                }
            }
        }

        private Move ConvertToDomain(MoveImportDto moveDto)
        {
            return new Move(moveDto.Name, moveDto.Type, moveDto.Category, moveDto.Power, moveDto.Accuracy, moveDto.PP);
        }

        private EvolutionCriteria ConvertToDomain(EvolutionImportDto evolutionDto, Species species)
        {
            switch (evolutionDto.Type)
            {
                case EvolutionType.Level:
                    return EvolutionCriteria.CreateLevelEvolution(evolutionDto.Level!, species);
                case EvolutionType.Item:
                    return EvolutionCriteria.CreateItemEvolution(evolutionDto.Item!, species);
                case EvolutionType.Trading:
                    return EvolutionCriteria.CreateTradingEvolution(species);
            }
            throw new Exception("No evolution type found");
        }
    }
}
