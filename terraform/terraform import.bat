terraform import -var-file="custom.tfvars" aws_s3_bucket_policy.image_bucket_policy poke-images
terraform state show aws_s3_bucket_policy.image_bucket_policy