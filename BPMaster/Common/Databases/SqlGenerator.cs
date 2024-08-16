using System.Reflection;
using Common.Application.Enums;
using Common.Application.Settings;
using Common.Databases.Interfaces;
using Common.Databases.PostgresSql;
using Common.Domains.Entities;
using Dapper.Contrib.Extensions;
using Microsoft.AspNetCore.Identity;
using TableAttribute = Dapper.Contrib.Extensions.TableAttribute;

namespace Common.Databases
{
    public abstract class SqlGenerator
    {
        private static readonly string _createTableTemplate = File.ReadAllText("./Common/Databases/Templates/CreateTable.txt");


        protected abstract IDictionaryDataType DictionaryDataType { get; }

        public static SqlGenerator GetInstance(DatabaseSetting? setting)
        {
            if (setting != null && setting.DatabaseType == DatabaseType.Postgres)
            {
                return new PostgresSqlGenerator();
            }

            throw new NotImplementedException();
        }

        public virtual void GenerateCreateTableSqlScripts(Type type, string path = "")
        {
            var existFiles = Directory.GetFiles(path).ToList() ?? [];

            var assembly = type.Assembly;
            var entityTypes = assembly.GetTypes().Where(x => 
                x.IsSubclassOf(typeof(BaseEntity<Guid>)) ||
                x.IsSubclassOf(typeof(BaseEntity<string>))
            );
            foreach( var entityType in entityTypes) {

                //Skip process sql script exists lready
                if (existFiles.Any(x => x.Contains($"_{entityType.Name}"))) {
                    continue;
                }
                string fileName = $"Create_Table_{entityType.Name}.sql";

                var metadata = CreateTableMetadata(entityType);

                var fileContent = string.Format(_createTableTemplate, metadata.TableName,
                    metadata.PkDeclaration, string.Join($",{System.Environment.NewLine}",
                    metadata.ColumnsDeclaration), metadata.PkConstraint);

                File.WriteAllLines($"{path}/{fileName}", new List<string> { fileContent });

            }
                   
        }

        protected virtual ColumnMetadata CreateColumnMetadata(PropertyInfo property)
        {
            var dbColumnName = $"\"{property.Name}\"";
            var dbColumnType = property.PropertyType.IsEnum ? "int" : DictionaryDataType.Dictionary[property.PropertyType];
            return new()
            {
                ColumnName = dbColumnName,
                ColumnNameReference = property.Name,
                ColumnType = dbColumnType,
                ColumnDeclaration = $"{dbColumnName} {dbColumnType}"

            };
        }

        protected virtual TableMetadata CreateTableMetadata(Type type) {
            var dbTableName = type.GetCustomAttribute<TableAttribute>()?.Name ?? type.Name;
            var metadata = new TableMetadata()
            {
                TableName = $"\"{dbTableName}\"",
                TableNameReference = dbTableName ,
            };

            

            foreach (var property in type.GetProperties())
            {
               var columnMetadata = CreateColumnMetadata(property); 
               

                //PK declaration
                if (property.Name.ToLower() == "id" || property.GetCustomAttribute<ExplicitKeyAttribute>() != null)
                {
                    metadata.PkDeclaration = $"{columnMetadata.ColumnDeclaration} NOT NULL";
                    metadata.PkConstraint = $"CONSTRAINT \"{dbTableName}_pkey\" PRIMARY KEY ({columnMetadata.ColumnName})";
                }
                else
                {
                    //No index in the first one
                    var indexSpace = metadata.ColumnsDeclaration.Count() == 0 ? "" : "    ";
                    metadata.ColumnsDeclaration.Add($"{indexSpace}{columnMetadata.ColumnDeclaration}");
                }
            }

            return metadata;
        
        
        }

    }

    public class ColumnMetadata
    {
        public string ColumnName { get; set; } = string.Empty;
        public string ColumnNameReference { get; set; } = string.Empty;
        public string ColumnType { get; set; } = string.Empty;
        public string ColumnDeclaration {  get; set; } = string.Empty;  
    }

    public class TableMetadata
    {
        public string TableName { get; set; } = string.Empty;
        public string TableNameReference { get; set; } = string.Empty;
        public string PkDeclaration { get; set; } = string.Empty;
        public List<string> ColumnsDeclaration { get; set; } = new();
        public string PkConstraint { get; set; } = string.Empty;
    }
}
