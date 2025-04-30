namespace SharedKernel;

public sealed class EnumerationInvalidException : Exception
{
    public EnumerationInvalidException(Type type)
        : base($"The type {type.Name} is not a valid enumeration type.")
    {
    }
}
