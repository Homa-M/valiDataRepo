
using System.Data;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace ValiData.SQL_SERVER_DB
{
    public class ValidateDbRecords
    {
        private readonly IConfiguration _config;
        List<ValidationRule> validationRules = new List<ValidationRule>();
        public ValidateDbRecords(IConfiguration config)
        {
            _config = config;
            Dictionary<string, string> validationRules = _config
            .GetSection("ValidationRules")
            .Get<Dictionary<string, string>>();
        }
        
        public async Task validRecordsAsync()
        {
            using var conn = new SqlConnection("your_connection_string");
            await conn.OpenAsync();

            var cmd = new SqlCommand("SELECT * FROM MyTable", conn);
            var reader = await cmd.ExecuteReaderAsync();

            var table = new DataTable();
            table.Load(reader);

            foreach (DataRow row in table.Rows)
            {
                foreach (DataColumn col in table.Columns)
                {
                    var value = row[col];
                    ValidateField(col.ColumnName, value);
                }
            }
        }

        void ValidateField(string name, object value)
        {
            var rule = validationRules.FirstOrDefault(r => r.Field == name);
            if (rule == null) return;

            if (rule.Type == "int")
            {
                if (value is int intValue)
                {
                    if (intValue < rule.Min || intValue > rule.Max)
                        Log.Error(name, value, "Out of range");
                }
            }
            else if (rule.Type == "string" && !string.IsNullOrEmpty(rule.RegexPattern))
            {
                if (!Regex.IsMatch(value?.ToString() ?? "", rule.RegexPattern))
                    Log.Error(name, value, "Invalid format");
            }
        }

    }
}
