package money

import (
	"errors"
	"math/big"
	"strings"
)

const MaxScale = 8

var ErrInvalidDecimal = errors.New("invalid decimal value")

func parse(value string) (*big.Rat, error) {
	value = strings.TrimSpace(value)
	if value == "" {
		return nil, ErrInvalidDecimal
	}
	rat := new(big.Rat)
	if _, ok := rat.SetString(value); !ok {
		return nil, ErrInvalidDecimal
	}
	return rat, nil
}

func Quantize(value string, scale int) (string, error) {
	if scale < 0 || scale > MaxScale {
		return "", ErrInvalidDecimal
	}
	rat, err := parse(value)
	if err != nil {
		return "", err
	}
	factor := new(big.Int).Exp(big.NewInt(10), big.NewInt(int64(scale)), nil)
	scaled := new(big.Rat).Mul(rat, new(big.Rat).SetInt(factor))
	numerator := new(big.Int).Set(scaled.Num())
	denominator := new(big.Int).Set(scaled.Denom())
	negative := numerator.Sign() < 0
	absNumerator := new(big.Int).Abs(numerator)
	quotient, remainder := new(big.Int), new(big.Int)
	quotient.QuoRem(absNumerator, denominator, remainder)
	if new(big.Int).Lsh(remainder, 1).Cmp(denominator) >= 0 {
		quotient.Add(quotient, big.NewInt(1))
	}
	if negative {
		quotient.Neg(quotient)
	}
	return formatScaled(quotient, scale), nil
}

func Multiply(left, right string, scale int) (string, error) {
	l, err := parse(left)
	if err != nil {
		return "", err
	}
	r, err := parse(right)
	if err != nil {
		return "", err
	}
	return Quantize(new(big.Rat).Mul(l, r).RatString(), scale)
}

func Sum(values []string, scale int) (string, error) {
	total := new(big.Rat)
	for _, value := range values {
		item, err := parse(value)
		if err != nil {
			return "", err
		}
		total.Add(total, item)
	}
	return Quantize(total.RatString(), scale)
}

func formatScaled(value *big.Int, scale int) string {
	negative := value.Sign() < 0
	absolute := new(big.Int).Abs(value).String()
	if scale == 0 {
		if negative && absolute != "0" {
			return "-" + absolute
		}
		return absolute
	}
	for len(absolute) <= scale {
		absolute = "0" + absolute
	}
	whole := absolute[:len(absolute)-scale]
	fraction := absolute[len(absolute)-scale:]
	result := whole + "." + fraction
	if negative && result != "0."+strings.Repeat("0", scale) {
		result = "-" + result
	}
	return result
}
