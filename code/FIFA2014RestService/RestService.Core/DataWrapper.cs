using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestService.Core
{
    public class DataWrapper<DbModel>
    {
        public string ID { get; set; }

        public DbModel Entity { get; set; }

        public DateTime updatedAt { get; set; }

        public DateTime createdAt { get; set; }
    }
}
