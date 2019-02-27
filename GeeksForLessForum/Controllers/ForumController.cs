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
            using (var db = Models.ApplicationDbContext.Create())
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
            using (var db = Models.ApplicationDbContext.Create())
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
            using (var db = Models.ApplicationDbContext.Create())
            {
                data = db.Topics.Where(t => t.Id == id);
                try
                {
                    top = new TopicsViewModel();
                    top.Topic = data.Cast<Topic>().FirstOrDefault().Header;
                    top.Body = data.Cast<Topic>().FirstOrDefault().Body;
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

        [WebMethod]
        public ActionResult GetComments()
        {
            return View("Hi");
        }

        [HttpPost]
        public ActionResult AddComment(Comment comment)
        {
            return View();
        }
       
    }
}
