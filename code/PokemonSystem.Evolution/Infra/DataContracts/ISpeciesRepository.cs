using PokemonSystem.Evolution.Infra.DatabaseDtos;

namespace PokemonSystem.Evolution.Infra.DataContracts
{
    public interface ISpeciesRepository
    {
        Task<SpeciesDynamoDb> GetSpeciesAsync(uint id);
    }
}
