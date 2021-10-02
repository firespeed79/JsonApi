using System;

namespace JsonApi
{

    internal interface IApiHandler
    {
        Type HandledType { get; }
        public bool Disabled { get; }
        public void Handle(object data);
    }
}