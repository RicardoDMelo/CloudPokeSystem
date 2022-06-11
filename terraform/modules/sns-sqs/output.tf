# Output variable definitions
output "aws_arn_pokemon_created_topic" {
  description = "ARN of the Pokemon Created Topic"
  value       = aws_sns_topic.pokemon_created_topic.arn
}
output "aws_arn_pokemon_evolved_topic" {
  description = "ARN of the Pokemon Evolved Topic"
  value       = aws_sns_topic.pokemon_evolved_topic.arn
}
output "aws_arn_pokemon_level_raised_topic" {
  description = "ARN of the Pokemon Level Raised Topic"
  value       = aws_sns_topic.pokemon_level_raised_topic.arn
}