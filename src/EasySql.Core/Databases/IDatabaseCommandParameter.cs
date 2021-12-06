using System.Data;

namespace EasySql.Databases
{
    public interface IDatabaseCommandParameter
    {
        string Name { get; }

        DbType DbType { get; }

        int? Length { get; set; }
    }
}
