using System.Collections.Generic;

namespace EfCoreSandbox.Model
{
    class Agent
    {
        public int Id { get; set; }
        public IList<Capability> Capabilities { get; set; }
    }
}
