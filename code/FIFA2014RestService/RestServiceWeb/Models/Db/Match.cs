﻿using BaaSReponsitory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace RestServiceWeb.Models.Db
{
    [CloudObject(ClassName = "Match")]
    [DataContract]
    public class Match
    {
        [CloudFiled(ColumnName = "Key", IsPrimaryKey = true)]
        public string Id { get; set; }

        [DataMember]
        public string SerialNumber { get; set; }

        [CloudFiled(ColumnName = "HostTeam", IsRelation = true, RelationType = CloudFiledType.ManyToOne)]
        public Team HostTeam { get; set; }

        [CloudFiled(ColumnName = "GuestTeam", IsRelation = true, RelationType = CloudFiledType.ManyToOne)]
        public Team GuestTeam { get; set; }

        [CloudFiled(ColumnName = "Result", IsRelation = true, RelationType = CloudFiledType.OneToOne)]
        public MatchResult Result { get; set; }

        [DataMember]
        public DateTime StartUTCTime { get; set; }

        [DataMember]
        public DateTime EndUTCTime { get; set; }


        
    }
}