using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestServiceWeb.Models.DataContracts
{
    public class DataWrapper<DbModel>
    {
        public string ID { get; set; }

        public DbModel Entity{get;set;}

        public DateTime updatedAt { get; set; }

        public DateTime createdAt { get; set; }
    }
}