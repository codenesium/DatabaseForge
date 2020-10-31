using System.Collections.Generic;

namespace Codenesium.DatabaseContracts
{
    public interface ISQLGenerator
    {
        string GenerateCreateDatabase(DatabaseContainer database);
        string GenerateCreateDefaultValueConstraint(DefaultConstraint constraint);
        string GenerateCreateDefaultValueConstraints(List<DefaultConstraint> constraints);
        string GenerateCreateSchema(Schema schema);
        string GenerateCreateTableStatement(string schema, Table table, List<ForeignKey> foreignKeys);
        string GenerateDisableForeignKeyContraints(Schema schema);
        string GenerateDropAllTables(Schema schema);
        string GenerateDropDefaultValueConstraint(DefaultConstraint constraint);
        string GenerateForeignKeyConstraints(Schema schema);
        string GenerateIndex(Constraint index);
        string GenerateIndexes(List<Constraint> indexes);
        string GenerateInsertStatmentsForDatabase(DatabaseContainer database);
        string GeneratePrimaryKeyConstraint(Constraint constraint);
    }
}