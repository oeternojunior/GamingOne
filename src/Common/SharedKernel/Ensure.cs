using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace SharedKernel;

public static class Ensure
{
    public static void NotNullOrEmpty(
        [NotNull] string? value,
        [CallerArgumentExpression("value")] string? paramName = default)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentNullException(paramName, $"{paramName} cannot be null or empty.");
        }
    }

    public static void NotNull(
        [NotNull] object? value,
        [CallerArgumentExpression("value")] string? paramName = default)
    {
        if (value is null)
        {
            throw new ArgumentNullException(paramName, $"{paramName} cannot be null.");
        }
    }

    public static void GreaterThanZero(
        decimal value,
        [CallerArgumentExpression("value")] string? paramName = default)
    {
        if (value <= 0)
        {
            throw new ArgumentOutOfRangeException(paramName, $"{paramName} must be greater than zero.");
        }
    }

    public static void StartDatePrecedesEndDate(
        DateTime start,
        DateTime end,
        [CallerArgumentExpression("end")] string? paramName = default)
    {
        if (start >= end)
        {
            throw new ArgumentOutOfRangeException(paramName, "Start date must be earlier than end date.");
        }
    }

    public static void NotWhitespace(
        string? value,
        [CallerArgumentExpression("value")] string? paramName = default)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException($"{paramName} cannot be empty or whitespace.", paramName);
        }
    }

    public static void NotNegative(
        int value,
        [CallerArgumentExpression("value")] string? paramName = default)
    {
        if (value < 0)
        {
            throw new ArgumentException($"{paramName} cannot be negative.", paramName);
        }
    }

    public static void NotNegativeOrZero(
        int value,
        [CallerArgumentExpression("value")] string? paramName = default)
    {
        if (value <= 0)
        {
            throw new ArgumentException($"{paramName} must be greater than zero.", paramName);
        }
    }

    public static void InRange<T>(
        T value, T min, T max,
        [CallerArgumentExpression("value")] string? paramName = default) where T : IComparable<T>
    {
        if (value.CompareTo(min) < 0 || value.CompareTo(max) > 0)
        {
            throw new ArgumentOutOfRangeException(paramName, $"{paramName} must be between {min} and {max}.");
        }
    }

    public static void ValidEnum<TEnum>(
        TEnum value,
        [CallerArgumentExpression("value")] string? paramName = default) where TEnum : struct, Enum
    {
        if (!Enum.IsDefined(typeof(TEnum), value))
        {
            throw new ArgumentException($"{paramName} has an invalid value.", paramName);
        }
    }

    public static void NotEmptyCollection<T>(
        IEnumerable<T> collection,
        [CallerArgumentExpression("collection")] string? paramName = default)
    {
        if (collection == null || !collection.Any())
        {
            throw new ArgumentException($"{paramName} cannot be empty.", paramName);
        }
    }

    public static void NoDuplicates<T>(
        IEnumerable<T> collection,
        Func<T, bool> predicate,
        string message,
        [CallerArgumentExpression("collection")] string? paramName = default)
    {
        if (collection.Any(predicate))
        {
            throw new ArgumentException(message, paramName);
        }
    }

    public static void Contains<T>(
        IEnumerable<T> collection,
        T value,
        [CallerArgumentExpression("collection")] string? paramName = default)
    {
        if (!collection.Contains(value))
        {
            throw new ArgumentException($"{paramName} does not contain the specified value.", paramName);
        }
    }

    public static void ValidEmail(
        string email,
        [CallerArgumentExpression("email")] string? paramName = default)
    {
        if (string.IsNullOrWhiteSpace(email) || !new EmailAddressAttribute().IsValid(email))
        {
            throw new ArgumentException($"{paramName} is not a valid email address.", paramName);
        }
    }

    public static void MinLength(
        string value,
        int minLength,
        [CallerArgumentExpression("value")] string? paramName = default)
    {
        if (value.Length < minLength)
        {
            throw new ArgumentException($"{paramName} must be at least {minLength} characters long.", paramName);
        }
    }
}