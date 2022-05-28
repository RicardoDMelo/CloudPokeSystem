# Output variable definitions
output "aws_arn_cert" {
  description = "ARN of the CERT"
  value       = aws_acm_certificate.cert.arn
}