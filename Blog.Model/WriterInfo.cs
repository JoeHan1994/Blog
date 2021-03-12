﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

namespace Blog.Model
{
   public  class WriterInfo:BaseId
    {
        [SugarColumn(ColumnName ="nvarchar(12)")]
        public string Name { get; set; }
        [SugarColumn(ColumnName = "nvarchar(16)")]
        public string UserName { get; set; }
        [SugarColumn(ColumnName = "nvarchar(64)")]
        public string UserPwd { get; set; }
    }
}
