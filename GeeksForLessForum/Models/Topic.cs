using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GeeksForLessForum.Models
{
    public class Topic
    {
        public int Id { get; set; }
        [MaxLength(40)]
        public string Header { get; set; }
        [MaxLength(1000)]
        public string Body { get; set; }
        public Comment Comments { get; set; }
    }
    public class Comment
    {
        public int Id { get; set; }
        [MaxLength(150)]
        public string Message { get; set; }
        public DateTime CommentedDate { get; set; }
    }
}