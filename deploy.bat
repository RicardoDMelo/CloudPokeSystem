cd code/PokemonSystem.Incubator/
dotnet lambda deploy-serverless --disable-interactive true --stack-name pokemon-incubator --s3-bucket poke-build-bucket --template serverless.template

cd ../PokemonSystem.Evolution/
dotnet lambda deploy-serverless --disable-interactive true --stack-name pokemon-evolution --s3-bucket poke-build-bucket --template serverless.template

cd ../PokemonSystem.Learning/
dotnet lambda deploy-serverless --disable-interactive true --stack-name pokemon-learning --s3-bucket poke-build-bucket --template serverless.template

