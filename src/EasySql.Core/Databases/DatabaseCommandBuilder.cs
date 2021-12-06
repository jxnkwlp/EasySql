using System.Collections.Generic;
using System.Text;

namespace EasySql.Databases
{
    public class DatabaseCommandBuilder : IDatabaseCommandBuilder
    {
        private int _indent = 0;
        private readonly int _indentSize = 4;
        private readonly StringBuilder _sb = new StringBuilder();
        private readonly List<IDatabaseCommandParameter> _parameters = new List<IDatabaseCommandParameter>();

        public IReadOnlyList<IDatabaseCommandParameter> Parameters => _parameters;

        public IDatabaseCommandBuilder AddParameter(IDatabaseCommandParameter parameter)
        {
            _parameters.Add(parameter);

            return this;
        }

        public IDatabaseCommandBuilder Append(string value)
        {
            if (value == null)
                throw new System.ArgumentNullException(nameof(value));

            _sb.Append(value);
            return this;
        }

        public IDatabaseCommandBuilder AppendLine(string value = null)
        {
            _sb.AppendLine(value);
            return this;
        }

        public IDatabaseCommand Build()
        {
            // TODO 
            return new DatabaseCommand(_sb.ToString(), Parameters);
        }

        public IDatabaseCommandBuilder DecrementIndent()
        {
            if (_indent <= 0)
                return this;

            // TODO 

            return this;
        }

        public IDatabaseCommandBuilder IncrementIndent()
        {
            _indent += _indentSize;

            // TODO 

            return this;
        }

    }
}
