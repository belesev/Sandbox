using System;
using System.Collections.Generic;
using System.Linq;
using EfCoreSandbox.Model;
using Microsoft.EntityFrameworkCore;

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
                existing = context.Agents.Include(p => p.Capabilities).First();
            }
            existing.Capabilities.Clear();
            existing.Capabilities.Add(new Capability {AgentId = existing.Id, Name = "platform", Value = "win8"});
            existing.Owner = "KIS";


            using (var context = new AgentContext())
            {
                var agentFromDb = context.Agents.Include(p => p.Capabilities).Single(p => p.Id == existing.Id);
                var entry = context.Entry(agentFromDb);
                entry.CurrentValues.SetValues(existing);
                entry.State = EntityState.Modified;
                foreach (var capability in agentFromDb.Capabilities)
                {
                    context.Entry(capability).State = EntityState.Deleted;
                }

                foreach (var capability in existing.Capabilities.ToArray())
                {
                    agentFromDb.Capabilities.Add(capability);
                    context.Entry(capability).State = EntityState.Added;
                }

                context.SaveChanges();
            }
        }
    }
}