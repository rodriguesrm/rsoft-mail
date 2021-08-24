namespace RSoft.Lib.Data.MongoDb.Extensions
{

    /// <summary>
    /// Provides helper extensions methods
    /// </summary>
    public static class HelperExtensions
    {

        /// <summary>
        /// Convert a string to camelCase
        /// </summary>
        /// <param name="expression">String expression to convert</param>
        public static string ToCamelCase(this string expression)
        {

            if (string.IsNullOrWhiteSpace(expression))
                return null;

            if (expression.Length == 1)
                return expression;

            return char.ToLowerInvariant(expression[0]) + expression[1..];
        }

    }
}
