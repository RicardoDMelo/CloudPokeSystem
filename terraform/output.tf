# Output variable definitions
output "certificate_id" {
  description = "Id of the Certificate"
  value       = module.aws_api_gateway.certificate_id
}