resource "aws_s3_bucket" "build_bucket" {
    bucket        = "poke-build-bucket"
    force_destroy = true
}

resource "aws_s3_bucket_versioning" "build_bucket_versioning" {
  bucket = aws_s3_bucket.build_bucket.id
  versioning_configuration {
    status = "Disabled"
  }
}

resource "aws_s3_bucket_request_payment_configuration" "build_bucket_payment" {
  bucket = aws_s3_bucket.build_bucket.bucket
  payer  = "BucketOwner"
}