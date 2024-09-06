using System.Text.Json.Serialization;

namespace Auth.Contracts.Enums
{
    /// <summary>
    /// Specifies the type of comparison operation for queries.
    /// </summary>
    public enum QueryComparisonType : byte
    {
        /// <summary>
        /// Checks if the values are equal.
        /// </summary>
        EqualTo = 1,

        /// <summary>
        /// Checks if the values are not equal.
        /// </summary>
        NotEqualTo = 2,

        /// <summary>
        /// Checks if the value contains a specific substring.
        /// </summary>
        Contains = 3,

        /// <summary>
        /// Checks if the value are not contains a specific substring.
        /// </summary>
        NotContains = 4,

        /// <summary>
        /// Checks if the value is greater than the comparison value.
        /// </summary>
        GreaterThan = 5,

        /// <summary>
        /// Checks if the value is greater than or equal to the comparison value.
        /// </summary>
        GreaterThanOrEqualTo = 6,

        /// <summary>
        /// Checks if the value is less than the comparison value.
        /// </summary>
        LessThan = 7,

        /// <summary>
        /// Checks if the value is less than or equal to the comparison value.
        /// </summary>
        LessThanOrEqualTo = 8,
    }
}