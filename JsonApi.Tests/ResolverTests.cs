using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using JsonApi.Tests.Handlers;
using NUnit.Framework;

namespace JsonApi.Tests
{
    public class Tests
    {
        private ModelHandler _modelHandler;
        private IResolver _resolver;

        [SetUp]
        public void Setup()
        {
            _modelHandler = new ModelHandler();
            var handlers = new List<IApiHandler>
            {
                _modelHandler
            };
            _resolver = new Resolver(handlers);
        }

        [TearDown]
        public void Teardown()
        {
            _modelHandler.Reset();
        }

        [Test]
        public void BasicHandling()
        {
            const string data = "{\"Model\": {\"Text\": \"text\", \"RequiredTest\": \"text\"}}";
            _resolver.CallHandler(data);
            Assert.IsNotNull(_modelHandler.Model);
        }

        [Test]
        public void MaxLengthValidation()
        {
            const string data = "{\"Model\": {\"Text\": \"Very long string, too long\", \"RequiredTest\": \"text\"}}";
            Assert.Catch<ValidationException>(() => { _resolver.CallHandler(data); });
        }

        [Test]
        public void RequiredValidation()
        {
            const string data = "{\"Model\": {\"Text\": \"text\"}}";
            Assert.Catch<ValidationException>(() => { _resolver.CallHandler(data); });
        }

        [Test]
        public void DisabledHandler()
        {
            const string data = "{\"Model\": {\"Text\": \"text\", \"RequiredTest\": \"text\"}}";
            _modelHandler.Disabled = true;
            Assert.Catch<InvalidOperationException>(() => { _resolver.CallHandler(data); });
        }
    }
}