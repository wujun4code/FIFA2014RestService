using BaaSReponsitory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace RestServiceWeb.Models.Db
{
    [CloudObject(ClassName = "Team")]
    [DataContract]
    public class Team
    {
        [CloudFiled(ColumnName = "Key", IsPrimaryKey = true)]
        public string Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [CloudFiled(ColumnName = "Matches", IsRelation = true, RelationType = CloudFiledType.OneToMany)]
        public List<Match> Matches { get; set; }

        [CloudFiled(ColumnName = "Players", IsRelation = true, RelationType = CloudFiledType.OneToMany)]
        public List<Player> Players { get; set; }


    }
}