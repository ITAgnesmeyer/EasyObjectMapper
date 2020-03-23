using System;
using System.Collections.Generic;
using EasyObjectMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyObjectMapperTest
{
    [TestClass]
    public class MapperTest
    {
        private Source _Source;
        [TestInitialize]
        public void Initialize()
        {
            this._Source = new Source
            {
                StringValue = "abcd",
                IntValue= 10,
                BoolValue = true,
                FloatValue = 1.11F,
                DecimalValue = (decimal) 1.21,
                DoubleValue = 1.12344,
                CharValue = 'a',
                ByteValue = 5,
                DynamicValue = "dynamic Value",
                ObjectValue = "object Value",
                NullAbleIntValue = 2,
                NullAbleBoolValue = true,
                NullAbleFloatValue = 2.54F,
                NullAbleDecimalValue = (decimal)2.21,
                NullAbleDoubleValue = 2.2244,
                NullAbleCharValue = 'b',
                NullAbleByteValue = 4,
                DictionaryValue = new Dictionary<string, string>{{"hallo","hallo"}, {"welt","welt"}},
                ListValue = new List<string>{"hallo","welt"}
            };

        }
        [TestMethod]
        public void TestValueTypes()
        {
            var mapper = new Mapper<DestinationValueTypes>();
            DestinationValueTypes valueTypes = mapper.Get(this._Source);

            Assert.AreEqual(valueTypes.IntValue, this._Source.IntValue);
            Assert.AreEqual(valueTypes.BoolValue, this._Source.BoolValue);
            Assert.AreEqual(valueTypes.FloatValue, this._Source.FloatValue);
            Assert.AreEqual(valueTypes.DecimalValue, this._Source.DecimalValue);
            Assert.AreEqual(valueTypes.DoubleValue, this._Source.DoubleValue);
            Assert.AreEqual(valueTypes.CharValue, this._Source.CharValue);
            Assert.AreEqual(valueTypes.ByteValue, this._Source.ByteValue);
        }

        [TestMethod]
        public void TestNullAbleTypes()
        {
            var mapper = new Mapper<DestinationNullAbleTypes>();
            var valueTypes = mapper.Get(this._Source);
            Assert.AreEqual(valueTypes.NullAbleIntValue, this._Source.NullAbleIntValue);
            Assert.AreEqual(valueTypes.NullAbleBoolValue, this._Source.NullAbleBoolValue);
            Assert.AreEqual(valueTypes.NullAbleFloatValue, this._Source.NullAbleFloatValue);
            Assert.AreEqual(valueTypes.NullAbleDecimalValue, this._Source.NullAbleDecimalValue);
            Assert.AreEqual(valueTypes.NullAbleDoubleValue, this._Source.NullAbleDoubleValue);
            Assert.AreEqual(valueTypes.NullAbleCharValue, this._Source.NullAbleCharValue);
            Assert.AreEqual(valueTypes.NullAbleByteValue, this._Source.NullAbleByteValue);
        }

        [TestMethod]
        public void TestObjectTypes()
        {
            var mpper = new Mapper<DestinationObjectTypes>();
            DestinationObjectTypes objTypes = mpper.Get(this._Source);
            Assert.AreEqual(objTypes.StringValue, this._Source.StringValue);
            Assert.AreEqual(objTypes.DynamicValue, this._Source.DynamicValue);
            Assert.AreEqual(objTypes.ObjectValue, this._Source.ObjectValue);
            Assert.AreEqual(objTypes.DictionaryValue, this._Source.DictionaryValue);
            Assert.AreEqual(objTypes.ListValue, this._Source.ListValue);
        }

        [TestMethod]
        public void TestValueTypesIgnoreCase()
        {
            var mapper = new Mapper<DestinationValueTypesIgnoreCase>();
            mapper.IgnoreCase = true;
            var valueTypes = mapper.Get(this._Source);
            Assert.AreEqual(valueTypes.intValue, this._Source.IntValue);
            Assert.AreEqual(valueTypes.Boolvalue, this._Source.BoolValue);
            Assert.AreEqual(valueTypes.FLOATVALUE, this._Source.FloatValue);
            Assert.AreEqual(valueTypes.decimalvalue, this._Source.DecimalValue);
        }

        [TestMethod]
        public void TestWrongType()
        {
            var mapper = new Mapper<DestinationWrongType>();

            Assert.ThrowsException<MappingException>(() =>
            {
                var valueTypes = mapper.Get(this._Source);
            });

            try
            {
                var valueTypes = mapper.Get(this._Source);
            }
            catch (Exception e)
            {
                Assert.AreEqual(e.Message, "Mapping Error:Source.IntValue=>DestinationWrongType.IntValue");
            }
        }

        [TestMethod]
        public void TestUpdate()
        {
            var mapper = new Mapper<DestinationValueTypes>();
            var valueTypes = mapper.Get(this._Source);
            var valueTypesDest = mapper.Get(this._Source);
            valueTypes.BoolValue = false;
            valueTypes.IntValue = 50;
            mapper.Update(valueTypes, valueTypesDest);
            mapper.Update(this._Source, valueTypes);
            Assert.AreEqual(valueTypes.BoolValue, this._Source.BoolValue);
            Assert.AreEqual(valueTypes.IntValue, this._Source.IntValue);
            Assert.AreEqual(valueTypesDest.BoolValue, false);
            Assert.AreEqual(valueTypesDest.IntValue, 50);
        }

        [TestMethod]
        public void TestGetList()
        {
            var mapper = new Mapper<Source>();
            var source1 = mapper.Get(this._Source);
            var source2 = mapper.Get(this._Source);
            var source3 = mapper.Get(this._Source);
            source1.IntValue = 20;
            source2.IntValue = 30;
            source3.IntValue = 55;
            List<Source> sourceList = new List<Source> {source1, source2, source3};
            var listMapper = new Mapper<DestinationValueTypes>();
            var list = listMapper.Get(sourceList);

            Assert.AreEqual(list[0].IntValue, source1.IntValue);
            Assert.AreEqual(list[1].IntValue, source2.IntValue);
            Assert.AreEqual(list[2].IntValue, source3.IntValue);
        }
    }
}
