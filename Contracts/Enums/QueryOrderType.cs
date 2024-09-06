using System.Text.Json.Serialization;

namespace Auth.Contracts.Enums
{
    /// <summary>
    /// Specifies the type of order operation for queries.
    /// </summary>
    public enum QueryOrderType : byte
    {
        /// <summary>
        /// order query accenting.
        /// </summary>
        Ascending = 1,

        /// <summary>
        /// order query descending
        /// </summary>
        Descending = 2,
    }
}