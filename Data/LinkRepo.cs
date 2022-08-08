using System.Text.Json;
using StackExchange.Redis;
using LinkApi.Models;

namespace LinkApi.Data
{
    public class LinkRepo : ILinkRepo
    {
        private readonly IConnectionMultiplexer _redis;

        public LinkRepo(IConnectionMultiplexer redis)
        {
            _redis = redis;
        }

        public int CountNumbersOfLinks()
        {
            var count = GetServer().Keys(pattern: "link_*").Count();
            return count;
        }

        public string DeleteLink(string linkGuid)
        {
            var db = _redis.GetDatabase();
            var jsonLink = db.StringGet("link_" + linkGuid);
            if (jsonLink.IsNull) return null;
            Link linkObj = JsonSerializer.Deserialize<Link>(jsonLink);
            DateTime dateOfCreation = linkObj.dateOfCreation;
            if (linkObj != null && DateTime.UtcNow.Subtract(dateOfCreation).TotalMinutes < 5)
            {
                db.KeyDelete("link_" + linkGuid);
                return "Data is deleted";
                
            }
            return null;
        }

        public string PackLink(string stringLink)
        {
            var db = _redis.GetDatabase();
            Link link = new Link();
            link.stringLink = stringLink;
            var serialLink = JsonSerializer.Serialize(link);
            db.StringSet("link_" + link.guid, serialLink);
            return link.guid;
        }

        public string UnpackLink(string linkGuid)
        {
            var db = _redis.GetDatabase();
            var jsonLink = db.StringGet("link_" + linkGuid);
            if (jsonLink.IsNull) return null;
            Link linkObj = JsonSerializer.Deserialize<Link>(jsonLink);
            DateTime dateOfCreation = linkObj.dateOfCreation;
            if (linkObj != null && DateTime.UtcNow.Subtract(dateOfCreation).TotalMinutes < 5)
            {
                return linkObj.stringLink;
            }
            return null;
        }
        protected IServer GetServer()
        {
            var server = _redis.GetServer(_redis.GetEndPoints()[0]);
            return server;
        }
    }
}