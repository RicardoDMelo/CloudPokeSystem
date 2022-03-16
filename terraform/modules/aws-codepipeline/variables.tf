variable "git_repository_id" {
  description = "Repository Id"
}

variable "git_repository_name" {
  description = "Repository Name"
}

variable "git_repository_branch" {
  description = "Build branch aka Master"
}

variable "aws_region" {
  description = "The region to use"
}

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

variable "webhook_secret" {
  description = "Webhook secret between AWS and Github"
  sensitive = true
}
