terraform {
    required_providers {
        aws = {
          source  = "hashicorp/aws"
          version = "~> 4.17.1"
        }
    }
}

resource "aws_codebuild_project" "code_test" {
    badge_enabled          = false
    build_timeout          = 20
    concurrent_build_limit = 1
    encryption_key         = "arn:aws:kms:${var.aws_region}:${var.aws_account_id}:alias/aws/s3"
    name                   = "PokeCodeTest"
    project_visibility     = "PRIVATE"
    queued_timeout         = 480
    service_role           = aws_iam_role.code_build_role.arn

    artifacts {
        encryption_disabled    = false
        name                   = "PokeCodeTest"
        override_artifact_name = false
        packaging              = "NONE"
        type                   = "CODEPIPELINE"
    }

    cache {
        modes = ["LOCAL_CUSTOM_CACHE"]
        type  = "LOCAL"
    }

    environment {
        compute_type                = "BUILD_GENERAL1_SMALL"
        image                       = "aws/codebuild/amazonlinux2-x86_64-standard:3.0"
        image_pull_credentials_type = "CODEBUILD"
        privileged_mode             = true
        type                        = "LINUX_CONTAINER"
    }

    logs_config {
        cloudwatch_logs {
            status = "ENABLED"
        }

        s3_logs {
            encryption_disabled = false
            status              = "DISABLED"
        }
    }

    source {
        git_clone_depth     = 0
        insecure_ssl        = false
        report_build_status = false
        type                = "CODEPIPELINE"
        buildspec           = "testspec.yml"
    }
}

resource "aws_codebuild_project" "dotnet_code_build" {
    badge_enabled          = false
    build_timeout          = 20
    concurrent_build_limit = 1
    encryption_key         = "arn:aws:kms:${var.aws_region}:${var.aws_account_id}:alias/aws/s3"
    name                   = "PokeDotNetCodeBuild"
    project_visibility     = "PRIVATE"
    queued_timeout         = 480
    service_role           = aws_iam_role.code_build_role.arn

    artifacts {
        encryption_disabled    = false
        name                   = "PokeDotNetCodeBuild"
        override_artifact_name = false
        packaging              = "NONE"
        type                   = "CODEPIPELINE"
    }

    cache {
        modes = ["LOCAL_CUSTOM_CACHE"]
        type  = "LOCAL"
    }

    environment {
        compute_type                = "BUILD_GENERAL1_SMALL"
        image                       = "aws/codebuild/amazonlinux2-x86_64-standard:3.0"
        image_pull_credentials_type = "CODEBUILD"
        privileged_mode             = true
        type                        = "LINUX_CONTAINER"

        environment_variable {
            name  = "AWS_ACCOUNT_ID"
            type  = "PLAINTEXT"
            value = var.aws_account_id
        }
        environment_variable {
            name  = "AWS_ACCESS_KEY_ID"
            type  = "PLAINTEXT"
            value = var.aws_access_key_id
        }
        environment_variable {
            name  = "AWS_SECRET_ACCESS_KEY"
            type  = "PLAINTEXT"
            value = var.aws_secret_access_key
        }
        environment_variable {
            name  = "DOMAIN_NAME"
            type  = "PLAINTEXT"
            value = var.domain_name
        }
        environment_variable {
            name  = "CERTIFICATE_ID"
            type  = "PLAINTEXT"
            value = var.certificate_id
        }
    }

    logs_config {
        cloudwatch_logs {
            status = "ENABLED"
        }

        s3_logs {
            encryption_disabled = false
            status              = "DISABLED"
        }
    }

    source {
        git_clone_depth     = 0
        insecure_ssl        = false
        report_build_status = false
        type                = "CODEPIPELINE"
        buildspec           = "dotnet-buildspec.yml"
    }
}

resource "aws_codebuild_project" "react_code_build" {
    badge_enabled          = false
    build_timeout          = 20
    concurrent_build_limit = 1
    encryption_key         = "arn:aws:kms:${var.aws_region}:${var.aws_account_id}:alias/aws/s3"
    name                   = "PokeReactCodeBuild"
    project_visibility     = "PRIVATE"
    queued_timeout         = 480
    service_role           = aws_iam_role.code_build_role.arn

    artifacts {
        encryption_disabled    = false
        name                   = "PokeReactCodeBuild"
        override_artifact_name = false
        packaging              = "NONE"
        type                   = "CODEPIPELINE"
    }

    cache {
        modes = ["LOCAL_CUSTOM_CACHE"]
        type  = "LOCAL"
    }

    environment {
        compute_type                = "BUILD_GENERAL1_SMALL"
        image                       = "aws/codebuild/amazonlinux2-x86_64-standard:3.0"
        image_pull_credentials_type = "CODEBUILD"
        privileged_mode             = true
        type                        = "LINUX_CONTAINER"

        environment_variable {
            name  = "DEPLOY_BUCKET"
            type  = "PLAINTEXT"
            value = var.front_bucket_name
        }
        environment_variable {
            name  = "DISTRIBUTION"
            type  = "PLAINTEXT"
            value = var.cloudfront_distribution_id
        }
    }

    logs_config {
        cloudwatch_logs {
            status = "ENABLED"
        }

        s3_logs {
            encryption_disabled = false
            status              = "DISABLED"
        }
    }

    source {
        git_clone_depth     = 0
        insecure_ssl        = false
        report_build_status = false
        type                = "CODEPIPELINE"
        buildspec           = "react-buildspec.yml"
    }
}

resource "aws_codestarconnections_connection" "github_connection" {
    name              = "GitHub Connection"
    provider_type     = "GitHub"
}

resource "aws_codepipeline" "code_pipeline" {
    name          = "PokePipeline"
    role_arn      = aws_iam_role.code_pipeline_role.arn

    artifact_store {
        location = "poke-build-bucket"
        type     = "S3"
    }

    stage {
        name = "Source"

        action {
            category         = "Source"
            configuration    = {
                "BranchName"           = var.git_repository_branch
                "ConnectionArn"        = aws_codestarconnections_connection.github_connection.arn
                "FullRepositoryId"     = var.git_repository_id
                "OutputArtifactFormat" = "CODE_ZIP"
            }
            name             = "Source"
            namespace        = "SourceVariables"
            output_artifacts = ["SourceArtifact"]
            region           = var.aws_region
            owner            = "AWS"
            provider         = "CodeStarSourceConnection"
            run_order        = 1
            version          = "1"
        }
    }

    stage {
        name = "Test"

        action {
            category         = "Build"
            configuration    = {
                "ProjectName" = "PokeCodeTest"
            }
            input_artifacts  = ["SourceArtifact"]
            name             = "Test"
            namespace        = "TestNamespace"
            region           = var.aws_region
            owner            = "AWS"
            provider         = "CodeBuild"
            run_order        = 1
            version          = "1"
        }
    }

    stage {
        name = "Build"

        action {
            category         = "Build"
            configuration    = {
                "ProjectName" = "PokeDotNetCodeBuild"
            }
            input_artifacts  = ["SourceArtifact"]
            name             = "DotnetBuild"
            namespace        = "DotnetBuildNamespace"
            region           = var.aws_region
            owner            = "AWS"
            provider         = "CodeBuild"
            run_order        = 1
            version          = "1"
        }
        
        action {
            category         = "Build"
            configuration    = {
                "ProjectName" = "PokeReactCodeBuild"
            }
            input_artifacts  = ["SourceArtifact"]
            name             = "FrontBuild"
            namespace        = "FrontBuildNamespace"
            region           = var.aws_region
            owner            = "AWS"
            provider         = "CodeBuild"
            run_order        = 1
            version          = "1"
        }
    }
}

locals {
  webhook_secret = "super-secret"
}

resource "aws_codepipeline_webhook" "codepipeline_webhook" {
  name            = "GithubRepositoryWebhook"
  authentication  = "GITHUB_HMAC"
  target_action   = "Source"
  target_pipeline = aws_codepipeline.code_pipeline.name

  authentication_configuration {
    secret_token = var.webhook_secret
  }

  filter {
    json_path    = "$.ref"
    match_equals = "refs/heads/{Branch}"
  }
}

resource "github_repository_webhook" "repository_webhook" {
  repository = var.git_repository_name
  
  configuration {
    url          = aws_codepipeline_webhook.codepipeline_webhook.url
    content_type = "json"
    insecure_ssl = true
    secret       = var.webhook_secret
  }

  events = ["push"]
}