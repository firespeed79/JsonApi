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
            _resolver.CallHandler(TestStrings.ValidJson);
            Assert.IsNotNull(_modelHandler.Model);
        }

        [Test]
        public void MaxLengthValidation()
        {
            Assert.Catch<ValidationException>(() => { _resolver.CallHandler(TestStrings.TooLongStringJson); });
        }

        [Test]
        public void RequiredValidation()
        {
            Assert.Catch<ValidationException>(() => { _resolver.CallHandler(TestStrings.NoRequiredFieldJson); });
        }

        [Test]
        public void DisabledHandler()
        {
            _modelHandler.Disabled = true;
            Assert.Catch<InvalidOperationException>(() => { _resolver.CallHandler(TestStrings.ValidJson); });
        }
    }
}