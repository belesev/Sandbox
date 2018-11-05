using System.Collections.Generic;

namespace EfCoreSandbox.Model
{
    class Agent
    {
        public int Id { get; set; }
        public string Owner { get; set; }
        public ICollection<Capability> Capabilities { get; set; } = new List<Capability>();
    }
}
