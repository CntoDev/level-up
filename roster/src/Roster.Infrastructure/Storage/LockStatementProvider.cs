using System;
using System.Collections.Concurrent;
using System.Linq;
using MassTransit;
using MassTransit.EntityFrameworkCoreIntegration;
using MassTransit.Metadata;
using MassTransit.Saga;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Roster.Infrastructure.Storage
{
    public class LockStatementProvider : ILockStatementProvider
    {
        const string DefaultSchemaName = "public";
        const string DefaultRowLockStatement = "SELECT * FROM \"{0}\".\"{1}\" WHERE \"{2}\" = @p0 FOR UPDATE";
        protected static readonly ConcurrentDictionary<Type, SchemaTableColumnTrio> TableNames = new ConcurrentDictionary<Type, SchemaTableColumnTrio>();
        
        string ILockStatementProvider.GetRowLockStatement<TSaga>(DbContext context)
        {
            var schemaTableTrio = GetSchemaAndTableNameAndColumnName(context, typeof(TSaga));
            return string.Format(DefaultRowLockStatement, schemaTableTrio.Schema, schemaTableTrio.Table, schemaTableTrio.ColumnName);
        }

        SchemaTableColumnTrio GetSchemaAndTableNameAndColumnName(DbContext context, Type type)
        {
            if (TableNames.TryGetValue(type, out var result))
                return result;

            var entityType = context.Model.FindEntityType(type);
            string schema = entityType.GetSchema();
            string tableName = entityType.GetTableName();
 
            var property = entityType.GetProperties().Single(x => x.Name.Equals(nameof(ISaga.CorrelationId), StringComparison.OrdinalIgnoreCase));
            string columnName = property.GetColumnName(StoreObjectIdentifier.Table(tableName));

            if (string.IsNullOrWhiteSpace(tableName))
                throw new MassTransitException($"Unable to determine saga table name: {TypeMetadataCache.GetShortName(type)} (using model metadata).");

            result = new SchemaTableColumnTrio(schema ?? DefaultSchemaName, tableName, columnName);
            TableNames.TryAdd(type, result);

            return result;
        }

        protected readonly struct SchemaTableColumnTrio
        {
            public SchemaTableColumnTrio(string schema, string table, string columnName)
            {
                Schema = schema;
                Table = table;
                ColumnName = columnName;
            }

            public readonly string Schema;
            public readonly string Table;
            public readonly string ColumnName;
        }                
    }
}