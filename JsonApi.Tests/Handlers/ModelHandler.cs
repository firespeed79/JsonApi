using JsonApi.Tests.Models;

namespace JsonApi.Tests.Handlers
{
    public class ModelHandler : ApiHandler<Model>
    {
        public Model Model { get; private set; }

        protected override void Handle(Model data)
        {
            Model = data;
        }

        public void Reset()
        {
            Model = null;
        }
    }
}