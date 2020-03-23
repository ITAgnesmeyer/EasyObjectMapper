using System.Collections.Generic;

namespace EasyObjectMapperTest
{
    public class DestinationObjectTypes
    {
        public string StringValue{get;set;}
        public dynamic DynamicValue { get;set; }
        public object ObjectValue{get;set;}
        public Dictionary<string,string> DictionaryValue{get;set;}
        public List<string> ListValue{get; set; }

    }
}