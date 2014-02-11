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
    public class Player
    {
        [CloudFiled(ColumnName = "Key", IsPrimaryKey = true)]
        public string Id { get; set; }

        [DataMember]
        public string FullName { get; set; }

        [DataMember]
        public string NickName { get; set; }



    }
}