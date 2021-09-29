using System;
using System.Collections.Generic;
using EasySql.Infrastructure;

namespace EasySql
{
    public class EntityConfiguration : IEntityConfiguration
    {
        private static readonly Dictionary<Type, EntityDefintion> _entities = new Dictionary<Type, EntityDefintion>();

        private readonly IEntityConfigurationLoader _loader;

        public EntityConfiguration(IEntityConfigurationLoader loader)
        {
            _loader = loader;
        }

        public EntityDefintion Find(Type entityType)
        {
            if (_entities.ContainsKey(entityType))
                return _entities[entityType];

            return null;
        }

        public EntityDefintion Register(Type entityType)
        {
            if (!_entities.ContainsKey(entityType))
                _entities[entityType] = _loader.Load(entityType);

            return _entities[entityType];
        }
    }
}
