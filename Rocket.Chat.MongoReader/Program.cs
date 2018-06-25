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
                //CheckOpen();
                CloseOld();
Console.WriteLine($"Working, pausing for {sleep}ms");
                Thread.Sleep(sleep);
            }

            }

        static void CheckOpen()
{
    var client = new MongoClient("mongodb://rocketchat.wcibags.com:27017");
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
}

static void CloseOld()
{
    var client = new MongoClient("mongodb://rocketchat.wcibags.com:27017");
    var db = client.GetDatabase("rocketchat");
    var collection = db.GetCollection<rocketchat_livechat_inquiry>("rocketchat_livechat_inquiry");
    var queryable = collection.AsQueryable();


    /*
    var query = collection.AsQueryable()
        .GroupBy(p => p.t)
        .Select(g => new { name = g.Key, Count = g.Count() });
        */

    var filter = Builders<rocketchat_livechat_inquiry>.Filter.Eq("status", "taken");
    var projection = Builders<rocketchat_livechat_inquiry>.Projection.Include("name").Include("status");
    var results = collection.Find(filter).Project(projection).ToList();

    if (results.Count >= 1)
    {
        Console.WriteLine("Running");
        WebRequest request = WebRequest.Create("http://172.21.200.43:8081");
        WebResponse response = request.GetResponse();
        response.Close();

    }
}
    }

    class rocketchat_livechat_inquiry