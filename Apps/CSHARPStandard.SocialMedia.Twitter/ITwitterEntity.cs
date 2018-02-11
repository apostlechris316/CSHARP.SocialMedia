using System;
using System.Collections.Generic;
using System.Text;

namespace CSHARPStandard.SocialMedia.Twitter
{
    /// <summary>
    /// Interface implemented by all Twitter entities
    /// </summary>
    public interface ITwitterEntity
    {
        // Unique Id for this entity
        string Id { get; set; }
    }
}
