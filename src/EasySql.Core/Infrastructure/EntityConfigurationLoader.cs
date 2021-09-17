using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using EasySql.Databases.TypeMappings;

namespace EasySql.Infrastructure
{
    public class EntityConfigurationLoader : IEntityConfigurationLoader
    {
        private readonly ITypeMappingConfiguration _typeMappingConfiguration;

        public EntityConfigurationLoader(ITypeMappingConfiguration typeMappingConfiguration)
        {
            _typeMappingConfiguration = typeMappingConfiguration;
        }

        public EntityDefintion Load(Type type)
        {
            string name = type.Name;
            string schema = null;

            var tableAttribute = type.GetCustomAttribute<TableAttribute>();
            if (tableAttribute != null)
            {
                name = tableAttribute.Name;
                schema = tableAttribute.Schema;
            }

            var columns = type.GetProperties().Where(x => x.CanWrite && x.CanWrite).Select(x => GetColunmnDefintion(name, x)).ToList();

            var entityDefinition = new EntityDefintion(name, schema, type, columns);
            return entityDefinition;
        }

        private ColunmnDefintion GetColunmnDefintion(string tableName, PropertyInfo propertyInfo)
        {
            // TODO  Need to optimize

            var name = propertyInfo.Name;
            bool isKey = false;
            bool isNullable = false;
            int? maxLength = null;

            var columnAttribute = propertyInfo.GetCustomAttribute<ColumnAttribute>();
            if (columnAttribute != null)
                name = columnAttribute.Name;

            var keyAttribute = propertyInfo.GetCustomAttribute<KeyAttribute>();
            if (columnAttribute != null)
                isKey = true;
            else
            {
                var keyNames = new[] { "Id", $"{tableName}Id", $"{tableName}_Id" };
                if (keyNames.Any(x => x.Equals(propertyInfo.Name, StringComparison.InvariantCultureIgnoreCase)))
                {
                    isKey = true;
                }
            }

            if (Nullable.GetUnderlyingType(propertyInfo.PropertyType) != null)
            {
                isNullable = true;
            }

            if (propertyInfo.GetCustomAttribute<RequiredAttribute>() != null)
            {
                isNullable = false;
            }

            var maxLengthAttribute = propertyInfo.GetCustomAttribute<MaxLengthAttribute>();

            if (maxLengthAttribute != null)
            {
                maxLength = maxLengthAttribute.Length;
            }

            var mapping = _typeMappingConfiguration.FindMapping(propertyInfo.PropertyType.GetUnderlyingType());

            if (mapping == null)
            {
                throw new Exception($"The type '{propertyInfo.Name}' mapping not found.");
            }

            return new ColunmnDefintion(propertyInfo.PropertyType, name, isKey, isNullable, maxLength, mapping);
        }
    }
}
