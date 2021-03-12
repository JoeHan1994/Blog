using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

namespace Blog.Model
{
    public class BlogNews:BaseId
    {
        [SugarColumn(ColumnDataType = "nvarchar(30)")]
        public string Title { get; set; }

        [SugarColumn(ColumnDataType = "text")]
        public string Content { get; set; }
        public DateTime Time { get; set; }
        public long BrowseCount { get; set; }
        public long LikeCount { get; set; }

        public int TypeId { get; set; }
        public int WriterId { get; set; }

        /// <summary>
        /// Dont mapping to DB
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public TypeInfo TypeInfo { get; set; }
        [SugarColumn(IsIgnore = true)]
        public WriterInfo WriterInfo { get; set; }
    }
}
