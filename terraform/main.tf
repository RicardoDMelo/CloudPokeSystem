terraform {
  required_providers {
    aws = {
      source  = "hashicorp/aws"
      version = "~> 4.17.1"
    }
  }

  cloud {
    organization = "ricksmelo"

    workspaces {
      name = "pokemon-app"
    }
  }
}

provider "aws" {
  alias  = "sa"
  region  = "sa-east-1"
  access_key = var.aws_access_key_id
  secret_key = var.aws_secret_access_key
}

provider "aws" {
  alias   = "us"
  region  = "us-east-1"
  access_key = var.aws_access_key_id
  secret_key = var.aws_secret_access_key
}

provider "github" {
  token = var.github_token
}

module "dynamodb" {
  source = "./modules/dynamodb"

  providers = {
    aws = aws.sa
  }
}

module "api_gateway" {
  source = "./modules/api-gateway"

  domain_name = var.domain_name

  providers = {
    aws = aws.sa
  }
}

module "sns_sqs" {
  source = "./modules/sns-sqs"

  aws_account_id = var.aws_account_id
  aws_region = var.aws_region

  providers = {
    aws = aws.sa
  }
}

module "front_app" {
  source = "./modules/front-app"

  domain_name = var.domain_name

  providers = {
    aws = aws.sa
    aws.us = aws.us
  }
}

module "build_pipeline" {
  source = "./modules/build-pipeline"

  aws_account_id = var.aws_account_id
  aws_access_key_id = var.aws_access_key_id
  aws_secret_access_key = var.aws_secret_access_key
  aws_region = var.aws_region
  webhook_secret = var.webhook_secret
  git_repository_id = var.git_repository_id
  git_repository_name = var.git_repository_name
  git_repository_branch = var.git_repository_branch
  domain_name = var.domain_name
  certificate_id = module.api_gateway.certificate_id
  front_bucket_name = module.front_app.aws_front_bucket_name
  cloudfront_distribution_id = module.front_app.aws_cloudfront_id

  providers = {
    aws = aws.sa
  }
}
