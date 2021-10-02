namespace JsonApi
{
    /// <summary>
    ///     Processes the data and call the appropriate handler.
    /// </summary>
    public interface IResolver
    {
        /// <summary>
        ///     Call handler.
        /// </summary>
        /// <param name="json">JSON string</param>
        void CallHandler(string json);
    }
}