using JsonApi.Tests.Models;
using NUnit.Framework;

namespace JsonApi.Tests
{
    public class SenderTests
    {
        private ISender _sender;
        
        [SetUp]
        public void Setup()
        {
            _sender = new Sender();
        }

        [Test]
        public void BasicSerialize()
        {
            var testObject = new Model
            {
                Text = "text",
                RequiredTest = "text"
            };

            var dataString = _sender.GetDataString(testObject);
            Assert.AreEqual(TestStrings.ValidJson, dataString);
        }
    }
}