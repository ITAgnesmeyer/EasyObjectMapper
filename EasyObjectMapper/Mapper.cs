using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EasyObjectMapper
{
    public class Mapper<T> where T:new()
    {
        public T Get<TInput>(TInput input)
        {
            Type source = typeof(TInput);
            Type dest = typeof(T);

            T newObj = new T();
            PropertyInfo[] properties = source.GetProperties();
            PropertyInfo[] destProperties = dest.GetProperties();
            foreach (PropertyInfo item in properties)
            {
                if(destProperties.Any(x=> x.Name == item.Name))
                {
                    var destProp = destProperties.First(x=> x.Name == item.Name);
                    if(destProp != null)
                    {
                        destProp.SetValue(newObj,item.GetValue(input));
                    }

                }
            }

            return newObj;

        }

        public void Update<TInput>(TInput input , T output)
        {
            Type source = typeof(TInput);
            Type dest = typeof(T);
            PropertyInfo[] properties = source.GetProperties();
            PropertyInfo[] destProperties = dest.GetProperties();
            foreach (PropertyInfo item in properties)
            {
                if(destProperties.Any(x=> x.Name == item.Name))
                {
                    var destProp = destProperties.First(x=> x.Name == item.Name);
                    if(destProp != null)
                    {
                        destProp.SetValue(output,item.GetValue(input));
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
    }
}

