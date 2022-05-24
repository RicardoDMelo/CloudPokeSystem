
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

resource "aws_dynamodb_table" "learning-pokemon-table" {
    name           = "Learning-Pokemon"
    billing_mode   = "PAY_PER_REQUEST"
    hash_key       = "Id"
    stream_enabled = false

    attribute {
        name = "Id"
        type = "S"
    }

    point_in_time_recovery {
        enabled = false
    }
}

resource "aws_dynamodb_table" "bills-pc-pokemon-events" {
    name           = "BillsPC-PokemonEvents"
    billing_mode   = "PAY_PER_REQUEST"
    hash_key       = "StreamId"
    range_key      = "StreamPosition"
    stream_enabled = false

    attribute {
        name = "StreamId"
        type = "S"
    }

    attribute {
        name = "StreamPosition"
        type = "N"
    }

    point_in_time_recovery {
        enabled = false
    }
}