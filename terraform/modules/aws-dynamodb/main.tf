
resource "aws_dynamodb_table" "pokedex-table" {
    name           = "PokemonSpecies"
    billing_mode   = "PROVISIONED"
    read_capacity  = 5
    write_capacity = 10
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