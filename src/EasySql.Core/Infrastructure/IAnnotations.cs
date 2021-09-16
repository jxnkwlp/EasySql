namespace EasySql.Infrastructure
{
    public interface IAnnotations
    {
        bool ContainsKey(string key);
        object Get(string key);
        void Set(string key, object value);
    }
}
