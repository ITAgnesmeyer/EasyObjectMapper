using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EasyObjectMapper
{
    public class Mapper<T> where T : new()
    {
        public bool IgnoreCase { get; set; }

        public T Get<TInput>(TInput input)
        {
            Type source = typeof(TInput);
            Type dest = typeof(T);

            T newObj = new T();
            PropertyInfo[] properties = source.GetProperties();
            PropertyInfo[] destProperties = dest.GetProperties();
            foreach (PropertyInfo item in properties)
            {
                if (ContainsAny(item, destProperties))
                {
                    var destProp = GetFirst(item, destProperties);
                    if (destProp != null)
                    {
                        try
                        {
                            destProp.SetValue(newObj, item.GetValue(input));
                        }
                        catch (Exception e)
                        {
                            throw new MappingException(
                                $"Mapping Error:{source.Name}.{item.Name}=>{dest.Name}.{destProp.Name}", e);
                        }
                    }
                }
            }

            return newObj;
        }

        public void Update<TInput>(TInput input, T output)
        {
            Type source = typeof(TInput);
            Type dest = typeof(T);
            PropertyInfo[] properties = source.GetProperties();
            PropertyInfo[] destProperties = dest.GetProperties();
            foreach (PropertyInfo item in properties)
            {
                if (ContainsAny(item, destProperties))
                {
                    var destProp = GetFirst(item, destProperties);
                    if (destProp != null)
                    {
                        try
                        {
                            destProp.SetValue(output, item.GetValue(input));
                        }
                        catch (Exception e)
                        {
                            throw new MappingException(
                                $"Mapping Error:{source.Name}.{item.Name}=>{dest.Name}.{destProp.Name}", e);
                        }
                    }
                }
            }
        }

        public List<T> Get<TInput>(List<TInput> input)
        {
            List<T> returnValue = new List<T>();
            foreach (TInput item in input)
            {
                T newObj = Get(item);
                returnValue.Add(newObj);
            }

            return returnValue;
        }

        private PropertyInfo GetFirst(PropertyInfo source, PropertyInfo[] dest)
        {
            if (this.IgnoreCase)
                return dest.First(x => x.Name.ToLower() == source.Name.ToLower());
            return dest.First(x => x.Name == source.Name);
        }

        private bool ContainsAny(PropertyInfo source, PropertyInfo[] dest)
        {
            if (this.IgnoreCase)
                return dest.Any(x => x.Name.ToLower() == source.Name.ToLower());
            return dest.Any(x => x.Name == source.Name);
        }
    }
}

