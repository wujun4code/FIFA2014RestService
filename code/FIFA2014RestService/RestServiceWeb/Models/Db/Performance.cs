using BaaSReponsitory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace RestServiceWeb.Models.Db
{
    [CloudObject(ClassName = "Player")]
    [DataContract]
    public class Performance
    {
        [CloudFiled(ColumnName = "Key", IsPrimaryKey = true)]
        public string Id { get; set; }

        [CloudFiled(ColumnName = "RelatedMatch", IsRelation = true, RelationType = CloudFiledType.ManyToOne)]
        public Match RelatedMatch { get; set; }

        [CloudFiled(ColumnName = "RelatedPlayer", IsRelation = true, RelationType = CloudFiledType.ManyToOne)]
        public Player RelatedPlayer { get; set; }

        [DataMember]
        public string MIN { get; set; }

        [DataMember]
        public string Grade { get; set; }

        [DataMember]
        public bool IsAlternate { get; set; }
    }
}