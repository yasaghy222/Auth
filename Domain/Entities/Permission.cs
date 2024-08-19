using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auth.Domain.Aggregates;
using LanguageExt;

namespace Auth.Domain.Entities
{
    public class Permission : BaseAggregate<Ulid>
    {
        public required Ulid RoleId { get; set; }
        public Role? Role { get; set; }

        public required Ulid ResourceId { get; set; }
        public Resource? Resource { get; set; }
    }
}