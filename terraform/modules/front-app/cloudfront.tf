
locals {
  s3_origin_id = "front_app_bucket_origin"
}

resource "aws_cloudfront_distribution" "s3_distribution" {
  origin {
    domain_name = aws_s3_bucket.front_app_bucket.bucket_regional_domain_name
    origin_id   = local.s3_origin_id

    s3_origin_config {
      origin_access_identity = ""
    }
  }

  enabled             = true
  default_root_object = "index.html"

  default_cache_behavior {
    min_ttl                = 0
    max_ttl                = 86400
    forwarded_values {
      query_string = true
    }
    target_origin_id = local.s3_origin_id
    viewer_protocol_policy = "redirect-to-https"
  }
}