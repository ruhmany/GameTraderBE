namespace GameTrader.Core.Attributes
{
    public class UrlAttribute : Attribute
    {
        public UrlAttribute(string url) => Url = url;

        public string Url { get; set; }
    }
}
