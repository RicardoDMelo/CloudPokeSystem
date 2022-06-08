using AutoMapper;
using PokemonSystem.Common.ValueObjects;
using PokemonSystem.Incubator.Application.ViewModel;
using PokemonSystem.Incubator.Domain.PokemonAggregate;
using PokemonSystem.Incubator.Domain.SpeciesAggregate;

namespace PokemonSystem.Tests.Incubator.Builders
{
    public class PokemonBuilder
    { 
        private string _nickname = "Tito";
        private Level _level = new Level(1);
        private Species _species = new SpeciesBuilder().Build();

        public PokemonBuilder()
        {
            Reset();
        }

        public void Reset()
        {
            _nickname = "Tauros";
            _level = new Level(1);
            _species = new SpeciesBuilder().Build();
        }

        public PokemonBuilder WithNickname(string nickname)
        {
            _nickname = nickname;
            return this;
        }

        public PokemonBuilder WithSpecies(Species species)
        {
            _species = species;
            return this;
        }

        public PokemonBuilder WithLevel(Level level)
        {
            _level = level;
            return this;
        }

        public Pokemon Build()
        {
            var pokemon = new Pokemon(_nickname, _species, _level);
            Reset();
            return pokemon;
        }

        public PokemonLookup ConvertToViewModel(Pokemon pokemon)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Pokemon, PokemonLookup>()
                    .ForMember(dest => dest.Level, opt => opt.MapFrom(src => src.LevelToGrow!.Value))
                    .ForMember(dest => dest.Name, opt => opt.MapFrom((src, dest) =>
                    {
                        if (string.IsNullOrEmpty(src.Nickname))
                        {
                            return src.PokemonSpecies.Name;
                        }
                        else
                        {
                            return $"{src.Nickname} - {src.PokemonSpecies.Name}";
                        }
                    }));
            });

            config.AssertConfigurationIsValid();
            var mapper = config.CreateMapper();

            return mapper.Map<PokemonLookup>(pokemon);
        }
    }
}