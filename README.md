# Cloud Pokemon System

This is a simple project used for testing cloud technologies, especifically AWS.

## Description

This projects aims to use AWS technologies, while trying to stay agnosthic and aparthed from the services that is being used. 

It's business rules are based on DDD, made from techniques like event storming and entity modeling. It's documentation is mainly on Notion.

## Getting Started

### Dependencies

* .NET (Net Core 3.1 and .NET 6)
* Terraform
* AWS Lambda
* AWS CodePipeline
* AWS CodeBuild
* AWS DynamoDB
* GitHub

### Running local

You need to run a DynamoDB locally. You can use the docker-compose.yml to achieve that.
```
cd dynamodb
docker-compose up
```
Run the PokedexInjector to inject all data necessary on DynamoDB.

Run each service as you like on Visual Studio.


### Installing on AWS

You need to terraform your AWS account using the following command:
```
cd terraform
terraform apply
```
This procedure can take some minutes to end.

## Help

You can check the domain documentation (in portuguese) in this link.
https://ricksmelo.notion.site/Pokemon-System-3fd4dc89168140e0812d53864ca66f5c

At last resort, contact me, @RicardoDMelo, for help. I can not guarantee any support.

## Authors

Ricardo Melo: @RicardoDMelo

## Version History

No version released yet.
See [commit change](https://github.com/RicardoDMelo/CloudPokeSystem/commits/main) for more information on development status.

## License

This project is licensed under the Apache License 2.0 - see the LICENSE file for details
