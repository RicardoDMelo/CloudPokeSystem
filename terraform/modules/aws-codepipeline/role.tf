resource "aws_iam_role" "code_build_role" {
    name                  = "CodeBuildRole"
    assume_role_policy    = jsonencode(
        {
            Statement = [
                {
                    Action    = "sts:AssumeRole"
                    Effect    = "Allow"
                    Principal = {
                        Service = "codebuild.amazonaws.com"
                    }
                },
            ]
            Version   = "2012-10-17"
        }
    )
    managed_policy_arns   = [
        "arn:aws:iam::aws:policy/AWSCodeBuildAdminAccess",
        "arn:aws:iam::aws:policy/AWSCodeBuildDeveloperAccess",
        "arn:aws:iam::aws:policy/AmazonS3ReadOnlyAccess",
        "arn:aws:iam::aws:policy/CloudWatchLogsFullAccess",
    ]
    max_session_duration  = 3600
}

resource "aws_iam_role" "code_pipeline_role" {
    name                  = "CodePipelineRole"
    assume_role_policy    = jsonencode(
        {
            Statement = [
                {
                    Action    = "sts:AssumeRole"
                    Effect    = "Allow"
                    Principal = {
                        Service = "codepipeline.amazonaws.com"
                    }
                },
            ]
            Version   = "2012-10-17"
        }
    )
    max_session_duration  = 3600

    inline_policy {
        name   = "CodePipelinePolicy"
        policy = jsonencode(
            {
                Statement = [
                    {
                        Action    = [
                            "iam:PassRole",
                        ]
                        Condition = {
                            StringEqualsIfExists = {
                                "iam:PassedToService" = [
                                    "cloudformation.amazonaws.com",
                                    "elasticbeanstalk.amazonaws.com",
                                    "ec2.amazonaws.com",
                                    "ecs-tasks.amazonaws.com",
                                ]
                            }
                        }
                        Effect    = "Allow"
                        Resource  = "*"
                    },
                    {
                        Action   = [
                            "codecommit:CancelUploadArchive",
                            "codecommit:GetBranch",
                            "codecommit:GetCommit",
                            "codecommit:GetRepository",
                            "codecommit:GetUploadArchiveStatus",
                            "codecommit:UploadArchive",
                        ]
                        Effect   = "Allow"
                        Resource = "*"
                    },
                    {
                        Action   = [
                            "codedeploy:CreateDeployment",
                            "codedeploy:GetApplication",
                            "codedeploy:GetApplicationRevision",
                            "codedeploy:GetDeployment",
                            "codedeploy:GetDeploymentConfig",
                            "codedeploy:RegisterApplicationRevision",
                        ]
                        Effect   = "Allow"
                        Resource = "*"
                    },
                    {
                        Action   = [
                            "codestar-connections:UseConnection",
                        ]
                        Effect   = "Allow"
                        Resource = "*"
                    },
                    {
                        Action   = [
                            "elasticbeanstalk:*",
                            "ec2:*",
                            "elasticloadbalancing:*",
                            "autoscaling:*",
                            "cloudwatch:*",
                            "s3:*",
                            "sns:*",
                            "cloudformation:*",
                            "rds:*",
                            "sqs:*",
                            "ecs:*",
                        ]
                        Effect   = "Allow"
                        Resource = "*"
                    },
                    {
                        Action   = [
                            "lambda:InvokeFunction",
                            "lambda:ListFunctions",
                        ]
                        Effect   = "Allow"
                        Resource = "*"
                    },
                    {
                        Action   = [
                            "opsworks:CreateDeployment",
                            "opsworks:DescribeApps",
                            "opsworks:DescribeCommands",
                            "opsworks:DescribeDeployments",
                            "opsworks:DescribeInstances",
                            "opsworks:DescribeStacks",
                            "opsworks:UpdateApp",
                            "opsworks:UpdateStack",
                        ]
                        Effect   = "Allow"
                        Resource = "*"
                    },
                    {
                        Action   = [
                            "cloudformation:CreateStack",
                            "cloudformation:DeleteStack",
                            "cloudformation:DescribeStacks",
                            "cloudformation:UpdateStack",
                            "cloudformation:CreateChangeSet",
                            "cloudformation:DeleteChangeSet",
                            "cloudformation:DescribeChangeSet",
                            "cloudformation:ExecuteChangeSet",
                            "cloudformation:SetStackPolicy",
                            "cloudformation:ValidateTemplate",
                        ]
                        Effect   = "Allow"
                        Resource = "*"
                    },
                    {
                        Action   = [
                            "codebuild:BatchGetBuilds",
                            "codebuild:StartBuild",
                            "codebuild:BatchGetBuildBatches",
                            "codebuild:StartBuildBatch",
                        ]
                        Effect   = "Allow"
                        Resource = "*"
                    },
                    {
                        Action   = [
                            "devicefarm:ListProjects",
                            "devicefarm:ListDevicePools",
                            "devicefarm:GetRun",
                            "devicefarm:GetUpload",
                            "devicefarm:CreateUpload",
                            "devicefarm:ScheduleRun",
                        ]
                        Effect   = "Allow"
                        Resource = "*"
                    },
                    {
                        Action   = [
                            "servicecatalog:ListProvisioningArtifacts",
                            "servicecatalog:CreateProvisioningArtifact",
                            "servicecatalog:DescribeProvisioningArtifact",
                            "servicecatalog:DeleteProvisioningArtifact",
                            "servicecatalog:UpdateProduct",
                        ]
                        Effect   = "Allow"
                        Resource = "*"
                    },
                    {
                        Action   = [
                            "cloudformation:ValidateTemplate",
                        ]
                        Effect   = "Allow"
                        Resource = "*"
                    },
                    {
                        Action   = [
                            "ecr:DescribeImages",
                        ]
                        Effect   = "Allow"
                        Resource = "*"
                    },
                    {
                        Action   = [
                            "states:DescribeExecution",
                            "states:DescribeStateMachine",
                            "states:StartExecution",
                        ]
                        Effect   = "Allow"
                        Resource = "*"
                    },
                    {
                        Action   = [
                            "appconfig:StartDeployment",
                            "appconfig:StopDeployment",
                            "appconfig:GetDeployment",
                        ]
                        Effect   = "Allow"
                        Resource = "*"
                    },
                ]
                Version   = "2012-10-17"
            }
        )
    }
}

resource "aws_iam_role" "lambda_execution_role" {
  name = "LambdaExecutionRole"

  assume_role_policy = jsonencode({
    "Version" : "2012-10-17",
    "Statement" : [
      {
        "Effect" : "Allow",
        "Principal" : {
          "Service" : "lambda.amazonaws.com"
        },
        "Action" : "sts:AssumeRole"
      }
    ]
  })
}
