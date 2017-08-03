using Itinero;
using System.IO;
using Itinero.IO.Shape;
using Itinero.Profiles;
using System.Collections.Generic;
using System;

namespace routerdb2shape
{
    class Program
    {
        static void Main(string[] args)
        {
            Itinero.Logging.Logger.LogAction = (origin, level, message, parameters) =>
            {
                Console.WriteLine(string.Format("[{0}-{3}] {1} - {2}", origin, level, message, DateTime.Now.ToString()));
            };

            Console.WriteLine("Loading router db...");
            var routerDb = RouterDb.Deserialize(File.OpenRead(args[0]));

            var profiles = new List<Profile>();
            for (var i = 2; i < args.Length; i++)
            {
                profiles.Add(routerDb.GetSupportedProfile(args[i]));
            }
            if (profiles.Count == 0)
            {
                Console.WriteLine("No profiles found.");
                return;
            }
            Console.WriteLine("Write shapefile...");
            routerDb.WriteToShape(args[1], profiles.ToArray());
        }
    }
}
