# Output variable definitions
output "aws_arn_pokedex_table" {
  description = "ARN of the Pokedex Table"
  value       = aws_dynamodb_table.pokedex-table.arn
}