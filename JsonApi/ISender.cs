namespace JsonApi
{
    /// <summary>
    ///     Converts objects into JSON that can be send to Resolver.
    /// </summary>
    public interface ISender
    {
        /// <summary>
        ///     Convert object into JSON string.
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        string GetDataString(object content);
    }
}