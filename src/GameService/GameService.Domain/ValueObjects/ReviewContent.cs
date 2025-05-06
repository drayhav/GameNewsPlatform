namespace GameService.Domain.ValueObjects
{
    public sealed class ReviewContent : IEquatable<ReviewContent>
    {
        private const int MaxLength = 1000;

        public string Value { get; }

        private ReviewContent(string value)
        {
            Value = value;
        }

        public static ReviewContent Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Review content cannot be empty or whitespace.", nameof(value));

            if (value.Length > MaxLength)
                throw new ArgumentException($"Review content cannot exceed {MaxLength} characters.", nameof(value));

            return new ReviewContent(value);
        }

        public override bool Equals(object? obj)
        {
            return obj is ReviewContent other && Equals(other);
        }

        public bool Equals(ReviewContent? other)
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

        public static implicit operator string(ReviewContent reviewContent)
        {
            return reviewContent.Value;
        }

        public static implicit operator ReviewContent(string value)
        {
            return Create(value);
        }
    }
}
