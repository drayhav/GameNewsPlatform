namespace GameService.Domain.ValueObjects
{
    public sealed class Name : IEquatable<Name>
    {
        public string Value { get; }

        public Name(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Name cannot be null or empty.", nameof(value));

            Value = value;
        }

        public override bool Equals(object? obj)
        {
            return obj is Name name && Equals(name);
        }

        public bool Equals(Name? other)
        {
            return other is not null && Value == other.Value;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return Value;
        }

        public static implicit operator Name(string value)
        {
            return new Name(value);
        }

        public static explicit operator string(Name name)
        {
            if (name is null)
                throw new ArgumentNullException(nameof(name), "Name cannot be null.");

            return name.Value;
        }

    }
}
