using System;

namespace JsonApi
{
    /// <summary>
    ///     Use <see cref="ApiHandler{T}" /> instead.
    /// </summary>
    public interface IApiHandler
    {
        /// <summary>
        ///     Handled model type.
        /// </summary>
        Type HandledType { get; }

        /// <summary>
        ///     Disable handler.
        /// </summary>
        public bool Disabled { get; }

        /// Handle object
        public void Handle(object data);
    }
}