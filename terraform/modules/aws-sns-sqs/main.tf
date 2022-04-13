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
resource "aws_sqs_queue" "pokemon_created_queue" {
  name                        = "PokemonCreatedQueue.fifo"
  fifo_queue                  = true
  content_based_deduplication = true
}
resource "aws_sqs_queue" "pokemon_evolved_queue" {
  name                        = "PokemonEvolvedQueue.fifo"
  fifo_queue                  = true
  content_based_deduplication = true
}
resource "aws_sqs_queue" "pokemon_level_raised_queue" {
  name                        = "PokemonLevelRaisedQueue.fifo"
  fifo_queue                  = true
  content_based_deduplication = true
}
resource "aws_sns_topic_subscription" "pokemon_created_sqs_target" {
  topic_arn = aws_sns_topic.pokemon_created_topic.arn
  protocol  = "sqs"
  endpoint  = aws_sqs_queue.pokemon_created_queue.arn
}
resource "aws_sns_topic_subscription" "pokemon_evolved_sqs_target" {
  topic_arn = aws_sns_topic.pokemon_evolved_topic.arn
  protocol  = "sqs"
  endpoint  = aws_sqs_queue.pokemon_evolved_queue.arn
}
resource "aws_sns_topic_subscription" "pokemon_level_raised_sqs_target" {
  topic_arn = aws_sns_topic.pokemon_level_raised_topic.arn
  protocol  = "sqs"
  endpoint  = aws_sqs_queue.pokemon_level_raised_queue.arn
}