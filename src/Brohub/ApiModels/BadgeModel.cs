using System.Collections.Generic;

namespace Brohub
{
    /// <summary>
    /// Summary description for BadgeModel
    /// </summary>
    public class BadgeModel
    {
        public int id { get; set; }

        public string title { get; set; }

        public string image_url { get; set; }

        public List<UserModel> users { get; set; }
    }
}