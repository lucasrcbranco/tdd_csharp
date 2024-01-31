using Domain._Utils.Validators;

namespace Domain.Common.Guards;

public class Guards
{
    public static void NotNullOrEmpty(string value, string errorMessage = "The value must not be null or empty")
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentException(errorMessage);
        }
    }

    public static void NotNull(object value, string errorMessage = "The value must not be null or empty")
    {
        if (value == null)
        {
            throw new ArgumentException(errorMessage);
        }
    }

    public static void GreaterThanZero(decimal value, string errorMessage = "The value must be greater than zero")
    {
        if (value <= 0)
        {
            throw new ArgumentException(errorMessage);
        }
    }

    public static void Assert(bool expression, string errorMessage = "The expression must be false")
    {
        if (expression is false)
        {
            throw new ArgumentException(errorMessage);
        }
    }

    public static void Ensures(bool expression, string errorMessage = "The expression must be true")
    {
        if (expression)
        {
            throw new ArgumentException(errorMessage);
        }
    }

    public static void IsValidDocument(string document, string errorMessage = "Document must have 11 digits")
    {
        if (!CpfValidator.IsValidCPF(document))
        {
            throw new ArgumentException(errorMessage);
        }
    }
}
