namespace EasySql.Databases
{
    public interface IDatabaseProvider
    {
        string Name { get; }

        string Version { get; }
    }
}
