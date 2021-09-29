using System.Collections.Generic;
using System.Text;
using EasySql.Query;

namespace EasySql.Databases
{
    public class SqlCommandBuilder : ISqlCommandBuilder
    {
        private int _indent = 0;
        private readonly int _indentSize = 4;
        private readonly StringBuilder _sb = new StringBuilder();
        private readonly List<ISqlCommandParameter> _parameters = new List<ISqlCommandParameter>();

        public IReadOnlyList<ISqlCommandParameter> Parameters => _parameters;

        public QueryResultType QueryResultType { get; set; }

        public ISqlCommandBuilder AddParameter(ISqlCommandParameter parameter)
        {
            _parameters.Add(parameter);

            return this;
        }

        public ISqlCommandBuilder Append(string value)
        {
            if (value == null)
                throw new System.ArgumentNullException(nameof(value));

            _sb.Append(value);
            return this;
        }

        public ISqlCommandBuilder AppendLine(string value = null)
        {
            _sb.AppendLine(value);
            return this;
        }

        public IDatabaseCommand Build()
        {
            // TODO 
            return new DatabaseCommand(_sb.ToString(), Parameters, QueryResultType);
        }

        public ISqlCommandBuilder DecrementIndent()
        {
            if (_indent <= 0)
                return this;

            // TODO 

            return this;
        }

        public ISqlCommandBuilder IncrementIndent()
        {
            _indent += _indentSize;

            // TODO 

            return this;
        }

    }
}
