using BaaSReponsitory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace RestServiceWeb.Models.Db
{
    [CloudObject(ClassName = "MatchResult")]
    [DataContract]
    public class MatchResult
    {
        [CloudFiled(ColumnName = "Key", IsPrimaryKey = true)]
        public string Id { get; set; }

        [DataMember]
        public string HostPoints { get; set; }

        [DataMember]
        public string GuestPoints { get; set; }

        [DataMember]
        public bool IsOT { get; set; }

        [DataMember]
        public bool IsPenalty { get; set; }

    }
}