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
            var count = GetServer().Keys().Count();
            return count;
        }

        public string DeleteLink(string linkGuid)
        {
            var db = _redis.GetDatabase();
            var jsonLink = db.StringGet(linkGuid);
            Link linkObj = JsonSerializer.Deserialize<Link>(jsonLink);
            DateTime dateOfCreation = linkObj.dateOfCreation;
            if (linkObj != null && DateTime.Now.Subtract(dateOfCreation).TotalMinutes < 5)
            {
                db.KeyDelete(linkGuid);
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
            db.StringSet(link.guid, serialLink);
            return link.guid;
        }

        public string UnpackLink(string linkGuid)
        {
            var db = _redis.GetDatabase();
            var jsonLink = db.StringGet(linkGuid);
            Link linkObj = JsonSerializer.Deserialize<Link>(jsonLink);
            DateTime dateOfCreation = linkObj.dateOfCreation;
            if (linkObj != null && DateTime.Now.Subtract(dateOfCreation).TotalMinutes < 5 )
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