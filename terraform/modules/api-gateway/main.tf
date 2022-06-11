terraform {
    required_providers {
        aws = {
          source  = "hashicorp/aws"
          version = "~> 4.17.1"
        }
    }
}
resource "aws_acm_certificate" "certificate" {
  domain_name       = "pokemon-api.${var.domain_name}"
  validation_method = "DNS"

  lifecycle {
    create_before_destroy = true
  }
}