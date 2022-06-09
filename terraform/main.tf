terraform {
  required_providers {
    aws = {
      source  = "hashicorp/aws"
      version = "~> 4.17.1"
    }
  }
}

provider "aws" {
  profile = "default"
  region  = "sa-east-1"
}

provider "github" {
  token = var.github_token
}

module "aws_dynamodb" {
  source = "./modules/aws-dynamodb"
}

module "aws_api_gateway" {
  source = "./modules/aws-api-gateway"

  domain_name = var.domain_name
}

module "aws_sns_sqs" {
  source = "./modules/aws-sns-sqs"

  aws_account_id = var.aws_account_id
  aws_region = var.aws_region
}

module "aws_codepipeline" {
  source = "./modules/aws-codepipeline"

  aws_account_id = var.aws_account_id
  aws_access_key_id = var.aws_access_key_id
  aws_secret_access_key = var.aws_secret_access_key
  aws_region = var.aws_region
  webhook_secret = var.webhook_secret
  git_repository_id = var.git_repository_id
  git_repository_name = var.git_repository_name
  git_repository_branch = var.git_repository_branch
  domain_name = var.domain_name
  certificate_id = module.aws_api_gateway.certificate_id
}