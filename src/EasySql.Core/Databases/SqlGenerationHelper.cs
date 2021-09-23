using System;
using System.IO;
using System.Text;

namespace EasySql.Databases
{
    public class SqlGenerationHelper : ISqlGenerationHelper
    {
        public virtual string StatementTerminator => ";";

        public virtual string BatchTerminator => Environment.NewLine;

        public virtual string StartTransactionStatement => "START TRANSACTION" + StatementTerminator;

        public virtual string CommitTransactionStatement => "COMMIT" + StatementTerminator;

        public virtual string SingleLineCommentToken => "--";

        public virtual string DelimitIdentifier(string identifier)
        {
            return $"\"{EscapeIdentifier(identifier)}\""; // Interpolation okay; strings
        }

        public virtual void DelimitIdentifier(StringBuilder builder, string identifier)
        {
            builder.Append('"');
            EscapeIdentifier(builder, identifier);
            builder.Append('"');
        }

        public virtual string DelimitIdentifier(string name, string schema)
        {
            return (!string.IsNullOrEmpty(schema)
                     ? DelimitIdentifier(schema) + "."
                     : string.Empty)
                 + DelimitIdentifier(name);
        }

        public virtual void DelimitIdentifier(StringBuilder builder, string name, string schema)
        {
            if (!string.IsNullOrEmpty(schema))
            {
                DelimitIdentifier(builder, schema);
                builder.Append('.');
            }

            DelimitIdentifier(builder, name);
        }

        public virtual string GenerateComment(string text)
        {
            var builder = new StringBuilder();
            using (var reader = new StringReader(text))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    builder.Append(SingleLineCommentToken).Append(' ').AppendLine(line);
                }
            }

            return builder.ToString();
        }

        public virtual string GenerateCreateSavepointStatement(string name)
        {
            return "SAVEPOINT " + DelimitIdentifier(name) + StatementTerminator;
        }

        public virtual string GenerateParameterName(string name)
        {
            return name.StartsWith("@", StringComparison.Ordinal)
                ? name
                : "@" + name;
        }

        public virtual void GenerateParameterName(StringBuilder builder, string name)
        {
            builder.Append('@').Append(name);
        }

        public virtual string GenerateParameterNamePlaceholder(string name)
        {
            return GenerateParameterName(name);
        }

        public virtual void GenerateParameterNamePlaceholder(StringBuilder builder, string name)
        {
            GenerateParameterName(builder, name);
        }

        public virtual string GenerateReleaseSavepointStatement(string name)
        {
            return "RELEASE SAVEPOINT " + DelimitIdentifier(name) + StatementTerminator;
        }

        public virtual string GenerateRollbackToSavepointStatement(string name)
        {
            return "ROLLBACK TO " + DelimitIdentifier(name) + StatementTerminator;
        }

        public virtual string EscapeIdentifier(string identifier)
        {
            return identifier.Replace("\"", "\"\"");
        }

        public virtual void EscapeIdentifier(StringBuilder builder, string identifier)
        {
            var initialLength = builder.Length;
            builder.Append(identifier);
            builder.Replace("\"", "\"\"", initialLength, identifier.Length);
        }
    }
}
