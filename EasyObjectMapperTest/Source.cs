using System.Collections.Generic;

namespace EasyObjectMapperTest
{
    public class Source
    {
        public string StringValue{get;set;}
        public int IntValue{get;set;}
        public bool BoolValue { get;set; }
        public float FloatValue{get;set;}
        public decimal DecimalValue{get;set;}
        public double DoubleValue{get;set;}
        public char CharValue{get;set;}
        public byte ByteValue{get;set;}

        public dynamic DynamicValue { get;set; }
        public object ObjectValue{get;set;}
        
        public int? NullAbleIntValue{get;set;}
        public bool? NullAbleBoolValue{get;set;}
        public float? NullAbleFloatValue{get;set;}
        public decimal? NullAbleDecimalValue{get;set;}
        public double? NullAbleDoubleValue{get;set;}
        public char? NullAbleCharValue{get;set;}
        public byte? NullAbleByteValue{get;set;}

        public Dictionary<string,string> DictionaryValue{get;set;}
        public List<string> ListValue{get; set; }



    }
}