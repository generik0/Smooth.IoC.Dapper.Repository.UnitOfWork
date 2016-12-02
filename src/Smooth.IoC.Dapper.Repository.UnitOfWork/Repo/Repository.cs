using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Dapper.FastCrud;
using Dapper.FastCrud.Mappings;
using Smooth.IoC.Dapper.Repository.UnitOfWork.Data;
using Smooth.IoC.Dapper.Repository.UnitOfWork.Entities;
using System.Reflection;

namespace Smooth.IoC.Dapper.Repository.UnitOfWork.Repo
{
    public abstract partial class Repository<TSession, TEntity, TPk> : RepositoryBase, IRepository<TEntity, TPk>
        where TEntity : class
        where TSession : class, ISession
    {
        protected readonly IDbFactory Factory;
        private static readonly ConcurrentDictionary<TEntity, PropertyMapping[]> _keys = new ConcurrentDictionary<TEntity, PropertyMapping[]>();
        private static readonly ConcurrentDictionary<TEntity, IEnumerable<PropertyInfo>> _properties = new ConcurrentDictionary<TEntity, IEnumerable<PropertyInfo>>();

        protected Repository(IDbFactory factory)
        {
            Factory = factory;
        }

        protected bool TryAllKeysDefault(TEntity entity)
        {
            var keys = _keys.GetOrAdd(entity, GetKeyPropertyMembers());
            var properies = _properties.GetOrAdd(entity, GetKeyPropertyInfo(entity, keys));
            if (keys == null || properies == null)
            {
                throw new NoPkException("There is no keys for this entity, please create your logic or add a key attribute to the entity");
            }
            return properies.Select(property => property.GetValue(entity)).All(value => value == null || value.Equals(0));
        }

        protected TPk GetPrimaryKeyValue(TEntity entity)
        {
            var keys = _keys.GetOrAdd(entity, GetKeyPropertyMembers());
            var primarKeyName = keys.FirstOrDefault(key => key.IsPrimaryKey)?.PropertyName;
            var properies = _properties.GetOrAdd(entity, GetKeyPropertyInfo(entity, keys));
            if (keys == null || primarKeyName ==null || properies == null)
            {
                throw new NoPkException("There is no primary ket for this entity, please create your logic or add a key attribute to the entity");
            }
            var primarKeyValue=  entity.GetType().GetProperties().FirstOrDefault(property => property.Name.Equals(primarKeyName, StringComparison.Ordinal));
            return (TPk) primarKeyValue.GetValue(entity);
        }
        protected void SetPrimaryKeyValue(TEntity entity, TPk value)
        {
            var keys = _keys.GetOrAdd(entity, GetKeyPropertyMembers());
            var primarKeyName = keys.FirstOrDefault(key => key.IsPrimaryKey)?.PropertyName;
            var properies = _properties.GetOrAdd(entity, GetKeyPropertyInfo(entity, keys));
            if (keys == null || primarKeyName == null || properies == null)
            {
                throw new NoPkException("There is no primary ket for this entity, please create your logic or add a key attribute to the entity");
            }
            var primarKeyValue = entity.GetType().GetProperties().FirstOrDefault(property => property.Name.Equals(primarKeyName, StringComparison.Ordinal));
            primarKeyValue.SetValue(entity, value);
        }

        private static IEnumerable<PropertyInfo> GetKeyPropertyInfo(TEntity entity, PropertyMapping[] keys)
        {
            return entity.GetType().GetProperties().Where(property => keys.Any(key => property.Name.Equals(key.PropertyName, StringComparison.Ordinal)));
        }

        private static PropertyMapping[] GetKeyPropertyMembers()
        {
            return OrmConfiguration.GetDefaultEntityMapping<TEntity>().GetProperties(PropertyMappingOptions.KeyProperty);
        }
    }
}
