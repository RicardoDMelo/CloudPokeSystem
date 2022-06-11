# Output variable definitions
output "aws_front_bucket_name" {
  description = "Front Bucket Name"
  value       = aws_s3_bucket.front_app_bucket.bucket
}

output "aws_cloudfront_id" {
  description = "Cloudfront Distribution Id"
  value       = aws_cloudfront_distribution.s3_distribution.id
}
