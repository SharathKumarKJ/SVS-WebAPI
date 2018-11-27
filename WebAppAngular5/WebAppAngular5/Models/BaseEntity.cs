using System;

namespace WebAppAngular5.Models
{
    public abstract class BaseEntity
    {
        public long Id { get; set; }
        public DateTimeOffset? Created { get; set; }
        public DateTimeOffset? Updated { get; set; }
        public User CreatedBy { get; set; }
        public User UpdatedBy { get; set; }
    }
}
