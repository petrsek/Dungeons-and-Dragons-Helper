using System;
using System.Collections.Generic;
using System.Text;

namespace DnDH.Repo
{
    /// <summary>
    /// Provides methods for validating data before it is saved to the database.
    /// </summary>
    public static class DataValidation
    {
        /// <summary>
        /// String extension that validates a string is not null, empty, or whitespace, and trims it. 
        /// Otherwise, throws an ArgumentException.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static string Validated(this string? s)
        {
            if (string.IsNullOrWhiteSpace(s))
                throw new ArgumentException("String cannot be null, empty or only whitespace.", nameof(s));

            return s.Trim();
        }
    }
}
