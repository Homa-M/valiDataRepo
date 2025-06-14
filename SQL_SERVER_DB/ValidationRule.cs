using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ValiData.SQL_SERVER_DB
{
    public class ValidationRule
    {
        public string Field { get; set; }           // Field name to validate
        public string Type { get; set; }
        public int? Min { get; set; }                // Optional minimum value (e.g., length or number)
        public int? Max { get; set; }                // Optional maximum value
        public string RegexPattern { get; set; }    // Optional regex pattern to validate format

        // Optional method to validate a value against the rule
        public bool Validate(object value)
        {
            if (value == null) return false;

            string strValue = value.ToString();

            // Check Min
            if (Min.HasValue && strValue.Length < Min.Value)
                return false;

            // Check Max
            if (Max.HasValue && strValue.Length > Max.Value)
                return false;

            // Check Regex
            if (!string.IsNullOrEmpty(RegexPattern))
            {
                var regex = new Regex(RegexPattern);
                if (!regex.IsMatch(strValue))
                    return false;
            }

            return true; // Passed all checks
        }
    }
}
