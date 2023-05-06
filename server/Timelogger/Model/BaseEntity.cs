using System;
using System.ComponentModel.DataAnnotations;

namespace Timelogger.Model
{
    public class BaseEntity
    {
        public Guid ID { get; set; }
        [Timestamp]
        public byte[] Version { get; set; }

    }
}
