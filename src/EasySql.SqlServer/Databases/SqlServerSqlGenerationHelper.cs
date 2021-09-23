using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasySql.Databases;

namespace EasySql.SqlServer.Databases
{
    public class SqlServerSqlGenerationHelper : SqlGenerationHelper
    {
        public override string BatchTerminator => "GO" + Environment.NewLine + Environment.NewLine;

        public override string EscapeIdentifier(string identifier)
        {
            return identifier.Replace("]", "]]");
        }

        public override void EscapeIdentifier(StringBuilder builder, string identifier)
        {
            var initialLength = builder.Length;
            builder.Append(identifier);
            builder.Replace("]", "]]", initialLength, identifier.Length);
        }

        public override string DelimitIdentifier(string identifier)
        {
            return $"[{EscapeIdentifier(identifier)}]";
        }

        public override void DelimitIdentifier(StringBuilder builder, string identifier)
        {
            builder.Append('[');
            EscapeIdentifier(builder, identifier);
            builder.Append(']');
        }
    }
}
