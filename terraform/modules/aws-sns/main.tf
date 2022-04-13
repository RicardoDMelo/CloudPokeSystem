resource "aws_sns_topic" "pokemon_created_topic" {
    application_success_feedback_sample_rate = 0
    content_based_deduplication              = true
    fifo_topic                               = true
    firehose_success_feedback_sample_rate    = 0
    http_success_feedback_sample_rate        = 0
    lambda_success_feedback_sample_rate      = 0
    name                                     = "PokemonCreatedTopic.fifo"
    sqs_failure_feedback_role_arn            = "arn:aws:iam::${var.aws_account_id}:role/SNSRole"
    sqs_success_feedback_role_arn            = "arn:aws:iam::${var.aws_account_id}:role/SNSRole"
    sqs_success_feedback_sample_rate         = 100
}
resource "aws_sns_topic" "pokemon_evolved_topic" {
    application_success_feedback_sample_rate = 0
    content_based_deduplication              = true
    fifo_topic                               = true
    firehose_success_feedback_sample_rate    = 0
    http_success_feedback_sample_rate        = 0
    lambda_success_feedback_sample_rate      = 0
    name                                     = "PokemonEvolvedTopic.fifo"
    sqs_failure_feedback_role_arn            = "arn:aws:iam::${var.aws_account_id}:role/SNSRole"
    sqs_success_feedback_role_arn            = "arn:aws:iam::${var.aws_account_id}:role/SNSRole"
    sqs_success_feedback_sample_rate         = 100
}
resource "aws_sns_topic" "pokemon_level_raised_topic" {
    application_success_feedback_sample_rate = 0
    content_based_deduplication              = true
    fifo_topic                               = true
    firehose_success_feedback_sample_rate    = 0
    http_success_feedback_sample_rate        = 0
    lambda_success_feedback_sample_rate      = 0
    name                                     = "PokemonLevelRaisedTopic.fifo"
    sqs_failure_feedback_role_arn            = "arn:aws:iam::${var.aws_account_id}:role/SNSRole"
    sqs_success_feedback_role_arn            = "arn:aws:iam::${var.aws_account_id}:role/SNSRole"
    sqs_success_feedback_sample_rate         = 100
}