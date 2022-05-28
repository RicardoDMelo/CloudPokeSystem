variable "github_token" {
  description = "GitHub Token"
  sensitive = true
}

# Module AWS
variable "aws_account_id" {
  description = "AWS Account ID"
  sensitive = true
}

variable "aws_access_key_id" {
  description = "AWS Access Key Id"
  sensitive = true
}

variable "aws_secret_access_key" {
  description = "AWS Secret Access Key"
  sensitive = true
}

variable "aws_region" {
  description = "The region to use"
  default     = "sa-east-1"
}

variable "webhook_secret" {
  description = "Webhook secret between AWS and Github"
  sensitive = true
}

variable "git_repository_id" {
  description = "Repository Id"
  default     = "RicardoDMelo/CloudPokeSystem"
}

variable "git_repository_name" {
  description = "Repository Name"
  default     = "CloudPokeSystem"
}

variable "git_repository_branch" {
  description = "Build branch aka Master"
  default     = "dev"
}

variable "domain_name" {
  description = "Domain Name"
  sensitive = true
}

