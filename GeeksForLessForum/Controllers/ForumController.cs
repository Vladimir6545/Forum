using GeeksForLessForum.Helpers;
using GeeksForLessForum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;

namespace GeeksForLessForum.Controllers
{
    public class ForumController : Controller
    {
        // GET: Forum
        public ActionResult Index()
        {
            var topics = new List<TopicsViewModel>();
            using (var db = ApplicationDbContext.Create())
            {
                var data = db.Topics.ToList();

                foreach (var item in data)
                {
                    try
                    {
                        var top = new TopicsViewModel();
                        top.Topic = item.Header;
                        top.Body = item.Body;
                        top.Id = item.Id;
                        topics.Add(top);
                    }
                    catch (Exception)
                    {
                        return null;
                    }

                }
            }
            return View(topics);
        }
        [HttpPost]
        public ActionResult CreateTopic(string topic, string body)
        {
            using (var db = ApplicationDbContext.Create())
            {
                Topic top = new Topic
                {
                    Header = topic,
                    Body = body
                };
                db.Topics.Add(top);
                db.SaveChanges();
            }
            return View();
        }

        public ActionResult Topic(int id)
        {
            IQueryable data;
            TopicsViewModel top;
            using (var db = ApplicationDbContext.Create())
            {
                data = db.Topics.Where(t => t.Id == id);
                try
                {
                    top = new TopicsViewModel();
                    top.Topic = data.Cast<Topic>().FirstOrDefault().Header;
                    top.Body = data.Cast<Topic>().FirstOrDefault().Body;
                    top.Id = data.Cast<Topic>().FirstOrDefault().Id;
                    // top.Messages = data.Cast<Topic>().FirstOrDefault().Message;
                    // topics.Add(top);
                }
                catch (Exception ex)
                {
                    return HttpNotFound(ex.Message);
                }

            }
            return View(top);
        }

        [HttpGet]
        public PartialViewResult GetComments(int topicId)
        {
            List<TopicMessageViewModel> comments = new List<TopicMessageViewModel>();
            using (var db = ApplicationDbContext.Create())
            {
                var commentsEntity = db.Comments.Where(c => c.TopicId == topicId).ToList();

                if (commentsEntity != null)
                {
                    foreach (var item in commentsEntity)
                    {
                        TopicMessageViewModel comment = new TopicMessageViewModel();
                        comment.Message = item.Message;
                        comment.MessagesDate = item.CommentedDate;
                        comments.Add(comment);
                    }

                }
            }
            return PartialView("~/Views/Shared/_Comments.cshtml", comments);
        }

        [HttpPost]
        public ActionResult AddComment(Comment comment)
        {
            using (var db = ApplicationDbContext.Create())
            {
                Comment commentEntity = new Comment
                {
                    TopicId = comment.TopicId,
                    CommentedDate = comment.CommentedDate,
                    Message = comment.Message
                };
                db.Comments.Add(commentEntity);
                db.SaveChanges();
            }
            return RedirectToAction("GetComments", new { topicId = comment.TopicId }); 
        }

    }
}
