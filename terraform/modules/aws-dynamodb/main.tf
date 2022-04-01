
resource "aws_dynamodb_table" "pokedex-table" {
    name           = "PokemonSpecies"
    billing_mode   = "PAY_PER_REQUEST"
    hash_key       = "Id"
    stream_enabled = false

    attribute {
        name = "Id"
        type = "N"
    }

    point_in_time_recovery {
        enabled = false
    }

    provisioner "local-exec" {
      command = "dotnet run --project ..\\code\\PokemonSystem.PokedexInjector\\PokemonSystem.PokedexInjector.csproj remote"
    }
}