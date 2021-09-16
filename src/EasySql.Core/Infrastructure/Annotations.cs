using System.Collections.Generic;

namespace EasySql.Infrastructure
{
    public class Annotations : IAnnotations
    {
        private readonly Dictionary<string, object> _annotations = new Dictionary<string, object>();

        public object this[string key]
        {
            get => _annotations[key];
            set => _annotations[key] = value;
        }

        public bool ContainsKey(string key) => _annotations.ContainsKey(key);

        public object Get(string key)
        {
            return _annotations[key];
        }

        public void Set(string key, object value)
        {
            _annotations[key] = value;
        }
    }
}
