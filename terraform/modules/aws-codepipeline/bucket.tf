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

resource "aws_s3_bucket" "images_bucket" {
    bucket                      = "poke-images"
    force_destroy = true
}

resource "aws_s3_bucket_versioning" "images_bucket_versioning" {
  bucket = aws_s3_bucket.images_bucket.id
  versioning_configuration {
    status = "Disabled"
  }
}

resource "aws_s3_bucket_request_payment_configuration" "images_bucket_payment" {
  bucket = aws_s3_bucket.images_bucket.bucket
  payer  = "BucketOwner"
}

resource "aws_s3_object" "image_objects" {
    for_each = fileset("images/", "*")
    bucket = aws_s3_bucket.images_bucket.id
    key = each.value
    source = "images/${each.value}"
    etag = filemd5("images/${each.value}")
}