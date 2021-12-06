using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using EasySql.Infrastructure;

namespace EasySql.Databases
{
    public class DatabaseDataReader : IDatabaseDataReader
    {
        private readonly DbDataReader _reader;
        private readonly DatabaseCommandContext _context;

        public DatabaseDataReader(DbDataReader reader, DatabaseCommandContext context)
        {
            _reader = reader;
            _context = context;
        }

        public void Dispose()
        {
            if (_reader?.IsClosed != true)
                _reader?.Close();
        }

        public IEnumerable<T> GetResult<T>()
        {
            var effectiveType = typeof(T);

            // var buffer = new List<T>();

            var entityDefintion = _context.EntityConfiguration.Find(effectiveType);

            while (_reader.Read())
            {
                var values = new Dictionary<ColunmnDefintion, object>();
                foreach (var item in entityDefintion.Colunmns)
                {
                    values[item] = _reader[item.Name];
                }

                yield return CreateInstance<T>(effectiveType, entityDefintion, values);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static object GetValue(Type effectiveType, object val)
        {
            if (val.GetType() == effectiveType)
            {
                return val;
            }
            else if (val == null && (!effectiveType.IsValueType || Nullable.GetUnderlyingType(effectiveType) != null))
            {
                return default;
            }
            else if (val is Array array && effectiveType.IsArray)
            {
                var elementType = effectiveType.GetElementType();
                var result = Array.CreateInstance(elementType, array.Length);
                for (int i = 0; i < array.Length; i++)
                    result.SetValue(Convert.ChangeType(array.GetValue(i), elementType, CultureInfo.InvariantCulture), i);
                return result;
            }
            else
            {
                try
                {
                    var convertToType = Nullable.GetUnderlyingType(effectiveType) ?? effectiveType;
                    return Convert.ChangeType(val, convertToType, CultureInfo.InvariantCulture);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }


        private static T CreateInstance<T>(Type type, EntityDefintion entity, Dictionary<ColunmnDefintion, object> values)
        {
            //DynamicMethod dm = new DynamicMethod("MapDR", type);

            //var il = dm.GetILGenerator();

            //il.DeclareLocal(type);

            //il.Emit(OpCodes.Newobj, typeof(T).GetConstructor(Type.EmptyTypes));
            //il.Emit(OpCodes.Stloc_0);

            //foreach (var item in values)
            //{
            //    il.Emit(OpCodes.Callvirt, type.GetMethod($"set_{item.Key.Name}", new[] { item.Key.ClrType }));
            //}

            //il.Emit(OpCodes.Ldloc_0);

            var instance = Activator.CreateInstance(type);

            foreach (var item in type.GetProperties().Where(x => x.CanWrite))
            {
                var column = entity.FindColumn(item);

                var columnValue = values[column];

                var value = GetValue(item.PropertyType, columnValue);

                item.SetValue(instance, value);
            }

            return (T)instance;
        }

    }
}
