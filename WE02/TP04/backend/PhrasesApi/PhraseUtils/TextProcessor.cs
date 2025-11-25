namespace PhraseUtils
{
    public static class TextProcessor
    {
        public static string Normalize(string text)
        {
            return text.Trim().Replace("\n", " ");
        }
    }
}