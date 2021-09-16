namespace EasySql.Databases
{
    public interface IDatabaseCommand
    {
        int ExecuteNonQuery(DatabaseCommandContext context);

        object ExecuteScalar(DatabaseCommandContext context);

        ISqlDataReader ExecuteReader(DatabaseCommandContext context);
    }
}
