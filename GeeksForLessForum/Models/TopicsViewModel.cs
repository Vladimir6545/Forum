﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeeksForLessForum.Models
{
    public class TopicsViewModel
    {
        public TopicMessageViewModel message;
        public TopicsViewModel()
        {
            message = new TopicMessageViewModel();
        }
        public int Id { get; set; }
        public string Topic { get; set; }
        public string Body { get; set; }
        //public TopicMessageViewModel Messages { get; set; }
    }
    public class TopicMessageViewModel
    {
        public int Id { get; set; }
        public string Message { get; set; }
    }
}