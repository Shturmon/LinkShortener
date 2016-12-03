using System;
using LinkShortener.BLL.Models.Base;

namespace LinkShortener.BLL.Models
{
    public class LinkModel : BaseModel
    {
        public string OriginalUrl { get; set; }
        public int TokenNumber { get; set; }
        public string Token { get; set; }
        public string ShortUrl { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ClickOnLinkCounter { get; set; }
    }
}
