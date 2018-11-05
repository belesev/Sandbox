using System;
using System.Collections.Generic;
using System.Linq;
using EfCoreSandbox.Model;

namespace EfCoreSandbox
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            using (var context = new AgentContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }

            using (var context = new AgentContext())
            {
                var agent = new Agent { Owner = "hive" };
                agent.Capabilities = new List<Capability>
                {
                    new Capability {Name = "platform", Value = "win7" },
                    new Capability {Name = "dms", Value = "new" }
                };
                context.Agents.Add(agent);
                context.SaveChanges();
            }

            Agent existing;
            using (var context = new AgentContext())
            {
                existing = context.Agents.First();
            }
            existing.Capabilities.Clear();
            existing.Capabilities.Add(new Capability {AgentId = existing.Id, Name = "platform", Value = "win7"});
        }
    }
}