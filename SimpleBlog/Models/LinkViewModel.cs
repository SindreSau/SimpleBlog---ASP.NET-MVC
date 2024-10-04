namespace SimpleBlog.Models
{
    public class LinkViewModel
    {
        public string Href { get; set; } = "#";
        public LinkType Type { get; set; } = LinkType.Default;
        public string Text { get; set; }
    }

    public enum LinkType
    {
        Default,
        Primary,
        Secondary,
        Success,
        Danger,
        Warning,
        Info,
        Light,
        Dark,
        BodyEmphasis
    }
}