

resource "aws_s3_bucket" "images_bucket" {
    bucket        = "poke-images"
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

resource "aws_s3_bucket_policy" "image_bucket_policy" {
    bucket = aws_s3_bucket.images_bucket.id
    policy = jsonencode(
        {
            Id        = "Policy1654727343240"
            Statement = [
                {
                    Action    = "s3:GetObject"
                    Effect    = "Allow"
                    Principal = "*"
                    Resource  = "arn:aws:s3:::poke-images/*"
                    Sid       = "Allo Public Read"
                },
            ]
            Version   = "2012-10-17"
        }
    )
}

resource "aws_s3_bucket" "front_app_bucket" {
    bucket        = "poke-front-app"
    force_destroy = true
}

resource "aws_s3_bucket_website_configuration" "front_app_bucket_website" {
  bucket = aws_s3_bucket.front_app_bucket.bucket
  
  index_document {
    suffix = "index.html"
  }
}

resource "aws_s3_bucket_versioning" "front_app_bucket_versioning" {
  bucket = aws_s3_bucket.front_app_bucket.id
  versioning_configuration {
    status = "Disabled"
  }
}

resource "aws_s3_bucket_request_payment_configuration" "front_app_bucket_payment" {
  bucket = aws_s3_bucket.front_app_bucket.bucket
  payer  = "BucketOwner"
}

resource "aws_s3_bucket_acl" "front_app_bucket_acl" {
  bucket = aws_s3_bucket.front_app_bucket.id
  acl    = "private"
}