using PokemonSystem.BillsPC.Domain.SpeciesAggregate;
using PokemonSystem.BillsPC.Infra.DatabaseDtos;

namespace PokemonSystem.BillsPC.Infra.Adapters
{
    public interface ISpeciesAdapter
    {
        Species ConvertToModel(SpeciesDynamoDb speciesDynamoDb);
        SpeciesDynamoDb ConvertToDto(Species species);
    }
}
