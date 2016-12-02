using System;
using LinkShortener.Data.Entities.Base;

namespace LinkShortener.Data.Entities
{
    public class Link : BaseEntity
    {
        public string OriginalUrl { get; set; }
        public string Token { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ClickOnLinkCounter { get; set; }

        public byte[] RowVersion { get; set; }
    }
}
