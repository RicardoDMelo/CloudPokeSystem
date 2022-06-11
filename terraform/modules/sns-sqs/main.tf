terraform {
    required_providers {
        aws = {
          source  = "hashicorp/aws"
          version = "~> 4.17.1"
        }
    }
}

resource "aws_sns_topic" "pokemon_created_topic" {
    content_based_deduplication              = true
    fifo_topic                               = true
    name                                     = "PokemonCreatedTopic.fifo"
}

resource "aws_sns_topic" "pokemon_evolved_topic" {
    content_based_deduplication              = true
    fifo_topic                               = true
    name                                     = "PokemonEvolvedTopic.fifo"
}

resource "aws_sns_topic" "pokemon_level_raised_topic" {
    content_based_deduplication              = true
    fifo_topic                               = true
    name                                     = "PokemonLevelRaisedTopic.fifo"
}

resource "aws_sns_topic" "pokemon_learned_move_topic" {
    content_based_deduplication              = true
    fifo_topic                               = true
    name                                     = "PokemonLearnedMoveTopic.fifo"
}

data "aws_iam_policy_document" "evolution_pokemon_created_queue_policy" {
  statement {
    actions   = [ "sqs:SendMessage"]
    resources = ["arn:aws:sqs:${var.aws_region}:${var.aws_account_id}:EvolutionPokemonCreatedQueue.fifo"]
    effect = "Allow"
    principals {
      type        = "Service"
      identifiers = ["sns.amazonaws.com"]
    }
    condition {
      test     = "ArnEquals"
      variable = "aws:SourceArn"
      values = ["arn:aws:sns:${var.aws_region}:${var.aws_account_id}:PokemonCreatedTopic.fifo"]
    }
  }
}

data "aws_iam_policy_document" "learning_pokemon_evolved_queue_policy" {
  statement {
    actions   = [ "sqs:SendMessage"]
    resources = ["arn:aws:sqs:${var.aws_region}:${var.aws_account_id}:LearningPokemonEvolvedQueue.fifo"]
    effect = "Allow"
    principals {
      type        = "Service"
      identifiers = ["sns.amazonaws.com"]
    }
    condition {
      test     = "ArnEquals"
      variable = "aws:SourceArn"
      values = ["arn:aws:sns:${var.aws_region}:${var.aws_account_id}:PokemonEvolvedTopic.fifo"]
    }
  }
}

data "aws_iam_policy_document" "learning_pokemon_level_raised_queue_policy" {
  statement {
    actions   = [ "sqs:SendMessage"]
    resources = ["arn:aws:sqs:${var.aws_region}:${var.aws_account_id}:LearningPokemonLevelRaisedQueue.fifo"]
    effect = "Allow"
    principals {
      type        = "Service"
      identifiers = ["sns.amazonaws.com"]
    }
    condition {
      test     = "ArnEquals"
      variable = "aws:SourceArn"
      values = ["arn:aws:sns:${var.aws_region}:${var.aws_account_id}:PokemonLevelRaisedTopic.fifo"]
    }
  }
}

data "aws_iam_policy_document" "billspc_pokemon_created_queue_policy" {
  statement {
    actions   = [ "sqs:SendMessage"]
    resources = ["arn:aws:sqs:${var.aws_region}:${var.aws_account_id}:BillsPCPokemonCreatedQueue.fifo"]
    effect = "Allow"
    principals {
      type        = "Service"
      identifiers = ["sns.amazonaws.com"]
    }
    condition {
      test     = "ArnEquals"
      variable = "aws:SourceArn"
      values = ["arn:aws:sns:${var.aws_region}:${var.aws_account_id}:PokemonCreatedTopic.fifo"]
    }
  }
}

data "aws_iam_policy_document" "billspc_pokemon_evolved_queue_policy" {
  statement {
    actions   = [ "sqs:SendMessage"]
    resources = ["arn:aws:sqs:${var.aws_region}:${var.aws_account_id}:BillsPCPokemonEvolvedQueue.fifo"]
    effect = "Allow"
    principals {
      type        = "Service"
      identifiers = ["sns.amazonaws.com"]
    }
    condition {
      test     = "ArnEquals"
      variable = "aws:SourceArn"
      values = ["arn:aws:sns:${var.aws_region}:${var.aws_account_id}:PokemonEvolvedTopic.fifo"]
    }
  }
}

data "aws_iam_policy_document" "billspc_pokemon_level_raised_queue_policy" {
  statement {
    actions   = [ "sqs:SendMessage"]
    resources = ["arn:aws:sqs:${var.aws_region}:${var.aws_account_id}:BillsPCPokemonLevelRaisedQueue.fifo"]
    effect = "Allow"
    principals {
      type        = "Service"
      identifiers = ["sns.amazonaws.com"]
    }
    condition {
      test     = "ArnEquals"
      variable = "aws:SourceArn"
      values = ["arn:aws:sns:${var.aws_region}:${var.aws_account_id}:PokemonLevelRaisedTopic.fifo"]
    }
  }
}

data "aws_iam_policy_document" "billspc_pokemon_learned_move_queue_policy" {
  statement {
    actions   = [ "sqs:SendMessage"]
    resources = ["arn:aws:sqs:${var.aws_region}:${var.aws_account_id}:BillsPCPokemonLearnedMoveQueue.fifo"]
    effect = "Allow"
    principals {
      type        = "Service"
      identifiers = ["sns.amazonaws.com"]
    }
    condition {
      test     = "ArnEquals"
      variable = "aws:SourceArn"
      values = ["arn:aws:sns:${var.aws_region}:${var.aws_account_id}:PokemonLearnedMoveTopic.fifo"]
    }
  }
}

resource "aws_sqs_queue" "evolution_pokemon_created_queue" {
    content_based_deduplication       = true
    deduplication_scope               = "queue"
    delay_seconds                     = 0
    fifo_queue                        = true
    fifo_throughput_limit             = "perQueue"
    name                              = "EvolutionPokemonCreatedQueue.fifo"
    policy                            = data.aws_iam_policy_document.evolution_pokemon_created_queue_policy.json
}

resource "aws_sqs_queue" "learning_pokemon_evolved_queue" {
    content_based_deduplication       = true
    deduplication_scope               = "queue"
    delay_seconds                     = 0
    fifo_queue                        = true
    fifo_throughput_limit             = "perQueue"
    name                              = "LearningPokemonEvolvedQueue.fifo"
    policy                            = data.aws_iam_policy_document.learning_pokemon_evolved_queue_policy.json
}

resource "aws_sqs_queue" "learning_pokemon_level_raised_queue" {
    content_based_deduplication       = true
    deduplication_scope               = "queue"
    delay_seconds                     = 0
    fifo_queue                        = true
    fifo_throughput_limit             = "perQueue"
    name                              = "LearningPokemonLevelRaisedQueue.fifo"
    policy                            = data.aws_iam_policy_document.learning_pokemon_level_raised_queue_policy.json
}

resource "aws_sqs_queue" "billspc_pokemon_created_queue" {
    content_based_deduplication       = true
    deduplication_scope               = "queue"
    delay_seconds                     = 0
    fifo_queue                        = true
    fifo_throughput_limit             = "perQueue"
    name                              = "BillsPCPokemonCreatedQueue.fifo"
    policy                            = data.aws_iam_policy_document.billspc_pokemon_created_queue_policy.json
}

resource "aws_sqs_queue" "billspc_pokemon_evolved_queue" {
    content_based_deduplication       = true
    deduplication_scope               = "queue"
    delay_seconds                     = 0
    fifo_queue                        = true
    fifo_throughput_limit             = "perQueue"
    name                              = "BillsPCPokemonEvolvedQueue.fifo"
    policy                            = data.aws_iam_policy_document.billspc_pokemon_evolved_queue_policy.json
}

resource "aws_sqs_queue" "billspc_pokemon_level_raised_queue" {
    content_based_deduplication       = true
    deduplication_scope               = "queue"
    delay_seconds                     = 0
    fifo_queue                        = true
    fifo_throughput_limit             = "perQueue"
    name                              = "BillsPCPokemonLevelRaisedQueue.fifo"
    policy                            = data.aws_iam_policy_document.billspc_pokemon_level_raised_queue_policy.json
}

resource "aws_sqs_queue" "billspc_pokemon_learned_move_queue" {
    content_based_deduplication       = true
    deduplication_scope               = "queue"
    delay_seconds                     = 0
    fifo_queue                        = true
    fifo_throughput_limit             = "perQueue"
    name                              = "BillsPCPokemonLearnedMoveQueue.fifo"
    policy                            = data.aws_iam_policy_document.billspc_pokemon_learned_move_queue_policy.json
}

resource "aws_sns_topic_subscription" "evolution_pokemon_created_sqs_target" {
    topic_arn = aws_sns_topic.pokemon_created_topic.arn
    protocol  = "sqs"
    endpoint  = aws_sqs_queue.evolution_pokemon_created_queue.arn
    raw_message_delivery           = false
}

resource "aws_sns_topic_subscription" "learning_pokemon_evolved_sqs_target" {
    topic_arn = aws_sns_topic.pokemon_evolved_topic.arn
    protocol  = "sqs"
    endpoint  = aws_sqs_queue.learning_pokemon_evolved_queue.arn
    raw_message_delivery           = false
}

resource "aws_sns_topic_subscription" "learning_pokemon_level_raised_sqs_target" {
    topic_arn = aws_sns_topic.pokemon_level_raised_topic.arn
    protocol  = "sqs"
    endpoint  = aws_sqs_queue.learning_pokemon_level_raised_queue.arn
    raw_message_delivery           = false
}

resource "aws_sns_topic_subscription" "billspc_created_sqs_target" {
    topic_arn = aws_sns_topic.pokemon_created_topic.arn
    protocol  = "sqs"
    endpoint  = aws_sqs_queue.billspc_pokemon_created_queue.arn
    raw_message_delivery           = false
}

resource "aws_sns_topic_subscription" "billspc_evolved_sqs_target" {
    topic_arn = aws_sns_topic.pokemon_evolved_topic.arn
    protocol  = "sqs"
    endpoint  = aws_sqs_queue.billspc_pokemon_evolved_queue.arn
    raw_message_delivery           = false
}

resource "aws_sns_topic_subscription" "billspc_level_raised_sqs_target" {
    topic_arn = aws_sns_topic.pokemon_level_raised_topic.arn
    protocol  = "sqs"
    endpoint  = aws_sqs_queue.billspc_pokemon_level_raised_queue.arn
    raw_message_delivery           = false
}

resource "aws_sns_topic_subscription" "billspc_learned_move_sqs_target" {
    topic_arn = aws_sns_topic.pokemon_learned_move_topic.arn
    protocol  = "sqs"
    endpoint  = aws_sqs_queue.billspc_pokemon_learned_move_queue.arn
    raw_message_delivery           = false
}