using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;

namespace GeeksForLessForum.Models
{
    public class TopicsViewModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(40, MinimumLength = 2)]
        public string Topic { get; set; }
        [Required]
        [StringLength(40, MinimumLength = 2)]
        public string Body { get; set; }
        public List<TopicMessageViewModel> Messages { get; set; }
    }
    public class TopicMessageViewModel
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string AuthorMessage { get; set; }
        public DateTime MessagesDate { get; set; }
        public int TopicId { get; set; }
    }
}