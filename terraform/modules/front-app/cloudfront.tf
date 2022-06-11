
locals {
  s3_origin_id = "front_app_bucket_origin"
}

resource "aws_acm_certificate" "certificate" {
  provider = aws.us
  domain_name       = "pokemon-app.${var.domain_name}"
  validation_method = "DNS"

  lifecycle {
    create_before_destroy = true
  }
}

resource "aws_cloudfront_distribution" "s3_distribution" {
  aliases                        = [
    "pokemon-app.ricardomelo.dev",
  ]
  origin {
    connection_attempts = 3
    connection_timeout  = 10
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

      cookies {
        forward = "none"
      }
    }

    target_origin_id = local.s3_origin_id
    viewer_protocol_policy = "redirect-to-https"
    allowed_methods  = ["GET", "HEAD"]    
    cached_methods   = ["GET", "HEAD"]
  }
  
  restrictions {
    geo_restriction {
      restriction_type = "none"
    }
  }
  
  custom_error_response {
    error_caching_min_ttl = 0
    error_code            = 403
    response_code         = 200
    response_page_path    = "/index.html"
  }

  custom_error_response {
    error_caching_min_ttl = 0
    error_code            = 404
    response_code         = 200
    response_page_path    = "/index.html"
  }

  viewer_certificate {
    acm_certificate_arn            = aws_acm_certificate.certificate.arn
    cloudfront_default_certificate = false
    minimum_protocol_version       = "TLSv1.2_2021"
    ssl_support_method             = "sni-only"
  }
}