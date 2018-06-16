using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Threading;


namespace Rocket.Chat.MongoReader
{
    class Program
    {
        static void Main(string[] args)
        {
            var sleep = 15000;
            if (args.Length > 0) { int.TryParse(args[0], out sleep); }
            while (true)
            {
                var client = new MongoClient("mongodb://172.16.100.100:27017");
                var db = client.GetDatabase("rocketchat");
                var collection = db.GetCollection<rocketchat_livechat_inquiry>("rocketchat_livechat_inquiry");
                var queryable = collection.AsQueryable();


                /*
                var query = collection.AsQueryable()
                    .GroupBy(p => p.t)
                    .Select(g => new { name = g.Key, Count = g.Count() });
                    */

                var filter = Builders<rocketchat_livechat_inquiry>.Filter.Eq("status", "open");
                var projection = Builders<rocketchat_livechat_inquiry>.Projection.Include("name").Include("status");
                var results = collection.Find(filter).Project(projection).ToList();

                if (results.Count >= 1)
                {
                    Console.WriteLine("Running");
                    WebRequest request = WebRequest.Create("http://172.21.200.43:8081");
                    WebResponse response = request.GetResponse();
                    response.Close();

                }
                Console.WriteLine($"Working, pausing for {sleep}ms");
                Thread.Sleep(sleep);
            }

            }
    }

    class rocketchat_livechat_inquiry
    {
        public string _id { get; set; }
        public string rid { get; set; }
        public string message { get; set; }
        public string name { get; set; }
        public DateTime ts { get; set; }
        public Int32 code { get; set; }
        public string department { get; set; }
        public HashSet<string> agents { get; set; }
        public string status { get; set; }
        public IEnumerable<vObject> v { get; set; }
        public string t { get; set; }
        public string _updatedAt { get; set; }

    }

    class vObject
    {
        public string _id { get; set; }
        public string username { get; set; }
        public string token { get; set; }
        public string status { get; set; }

    }
}
