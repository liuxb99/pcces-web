package money

// CalculateBudgetLeaf returns quantity multiplied by unit price using HALF_UP.
func CalculateBudgetLeaf(quantity, unitPrice string, amountScale int) (string, error) {
	return Multiply(quantity, unitPrice, amountScale)
}

// CalculateBudgetRollup returns the signed sum of child amounts.
func CalculateBudgetRollup(children []string, amountScale int) (string, error) {
	return Sum(children, amountScale)
}
