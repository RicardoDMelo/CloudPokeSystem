# Output variable definitions
output "certificate_id" {
  description = "Id of the Certificate"
  value       = aws_acm_certificate.cert.id
}