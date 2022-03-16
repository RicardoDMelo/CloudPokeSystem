# Output variable definitions
output "aws_arn_codepipeline" {
  description = "ARN of the Code Pipeline"
  value       = aws_codepipeline.code_pipeline.arn
}

output "aws_arn_code_build" {
  description = "ARN of the Code Build"
  value       = aws_codebuild_project.code_build.arn
}

output "aws_arn_build_bucket" {
  description = "ARN of the CI Bucket"
  value       = aws_s3_bucket.build_bucket.arn
}
