# Output variable definitions
output "aws_arn_front_bucket" {
  description = "Front Bucket Name"
  value       = aws_s3_bucket.front_app_bucket.bucket
}

output "aws_arn_cloudfront" {
  description = "Cloudfront Distribution Id"
  value       = aws_cloudfront_distribution.s3_distribution.id
}
