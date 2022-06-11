using PokemonSystem.BillsPC.Domain.SpeciesAggregate;
using PokemonSystem.BillsPC.Infra.DatabaseDtos;

namespace PokemonSystem.BillsPC.Infra.Adapters
{
    public interface IDbSpeciesAdapter
    {
        Species ConvertToModel(SpeciesDynamoDb speciesDynamoDb);
    }
}
