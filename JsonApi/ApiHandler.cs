using System;

namespace JsonApi
{
    /// <summary>
    ///     Model handler.
    /// </summary>
    /// <typeparam name="T">Model class</typeparam>
    public abstract class ApiHandler<T> : IApiHandler
    {
        /// <inheritdoc />
        public Type HandledType { get; } = typeof(T);

        /// <inheritdoc />
        public bool Disabled { get; set; }

        /// <inheritdoc />
        public void Handle(object data)
        {
            Handle((T)data);
        }

        /// <summary>
        ///     Function called after receiving data.
        /// </summary>
        /// <param name="data">Model class object</param>
        protected abstract void Handle(T data);
    }
}