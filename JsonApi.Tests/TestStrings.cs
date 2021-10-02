namespace JsonApi.Tests
{
    public static class TestStrings
    {
        public const string ValidJson = "{\"Model\":{\"Text\":\"text\",\"RequiredTest\":\"text\"}}";
        
        public const string TooLongStringJson =
            "{\"Model\":{\"Text\":\"Very long string, too long\",\"RequiredTest\":\"text\"}}";

        public const string NoRequiredFieldJson = "{\"Model\":{\"Text\":\"text\"}}";
    }
}