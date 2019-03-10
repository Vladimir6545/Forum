using System;
using System.ComponentModel.DataAnnotations;

namespace GeeksForLessForum.Models
{
    public class Topic
    {
        public int Id { get; set; }
        [Required]
        [StringLength(40, MinimumLength = 2)]
        public string Header { get; set; }
        [Required]
        [StringLength(1000, MinimumLength = 1)]
        public string Body { get; set; }
        public string UserID { get; set; }
    }
    public class Comment
    {
        public int Id { get; set; }
        [MaxLength(150)]
        public string Message { get; set; }
        public DateTime CommentedDate { get; set; }
        public int TopicId { get; set; }
        public string UserID { get; set; }
        public string UserName { get; set; }
    }
}