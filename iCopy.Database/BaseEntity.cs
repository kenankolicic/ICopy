using System;
using System.ComponentModel.DataAnnotations;

namespace iCopy.Database
{
    public abstract class BaseEntity<TKey>
    {
        [Key]
        public TKey ID { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool Active { get; set; }
    }
}