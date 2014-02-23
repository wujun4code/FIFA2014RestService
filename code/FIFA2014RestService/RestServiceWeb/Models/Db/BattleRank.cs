using BaaSReponsitory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace RestServiceWeb.Models.Db
{
    [CloudObject(ClassName = "Group")]
    [DataContract] 
    public class BattleRank
    {
        [CloudFiled(ColumnName = "Key", IsPrimaryKey = true)]
        public string Id { get; set; }

        [CloudFiled(ColumnName = "RelatedTeam", IsRelation = true, RelationType = CloudFiledType.OneToOne)]
        public Team RelatedTeam { get; set; }

        [CloudFiled(ColumnName = "RelatedGroup", IsRelation = true, RelationType = CloudFiledType.OneToOne)]
        public Group RelatedGroup { get; set; }

        [DataMember]
        public int MatchedPlayed { get; set; }

        [DataMember]
        public int Wons { get; set; }

        [DataMember]
        public int Ties { get; set; }

        [DataMember]
        public int Losts { get; set; }

        [DataMember]
        public int GoalsScored { get; set; }

        [DataMember]
        public int GoalsAgainst { get; set; }

        [DataMember]
        public int Points { get; set; }
    }
}