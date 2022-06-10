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

module "dynamodb" {
  source = "./modules/dynamodb"
}

module "api_gateway" {
  source = "./modules/api-gateway"

  domain_name = var.domain_name
}

module "sns_sqs" {
  source = "./modules/sns-sqs"

  aws_account_id = var.aws_account_id
  aws_region = var.aws_region
}

module "front_app" {
  source = "./modules/front-app"
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
}