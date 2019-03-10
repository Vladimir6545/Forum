using GeeksForLessForum.Models;
using GeeksForLessForum.Repositories;
using Microsoft.AspNet.Identity;
using System;
using System.Web.Mvc;

namespace GeeksForLessForum.Controllers
{
    public class ForumController : Controller
    {
        private TopicRepository _topicRepo;
        private CommentRepository _commentRepo;

        public ForumController()
        {
            _topicRepo = new TopicRepository();
            _commentRepo = new CommentRepository();
        }
       
        public ActionResult Index()
        {
            var topics = _topicRepo.GetAllTopics();
            return View(topics);
        }
        [HttpPost]
        [Authorize(Roles = "Admin, User")]
        [ValidateAntiForgeryToken]
        public ActionResult CreateTopic()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin, User")]
        [ValidateAntiForgeryToken]
        public ActionResult AddTopic(string topic, string body)
        {
            if (!String.IsNullOrEmpty(topic) & !String.IsNullOrEmpty(body))
            {
                string currentUserId = User.Identity.GetUserId();
                int userId = _topicRepo.AddTopic(topic, body, currentUserId);
                if (userId > 0)
                {
                    ViewBag.IdTopic = userId;
                    return View();
                }
            }
            return HttpNotFound();
        }

        [AllowAnonymous]
        public ActionResult Topic(int id)
        {
            var top = _topicRepo.GetTopic(id);
            if (top != null)
            {
                return View(top);
            }
            return HttpNotFound();
        }

        [HttpGet]
        [AllowAnonymous]
        public PartialViewResult GetComments(int topicId)
        {
            var comments = _commentRepo.GetAllComments(topicId);
            return PartialView("~/Views/Shared/_Comments.cshtml", comments);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult AddComment(Comment comment)
        {
            string currentUserId = User.Identity.GetUserId();
            string currentUserName = User.Identity.GetUserName();
            _commentRepo.AddComment(comment, currentUserId, currentUserName);
            return RedirectToAction("GetComments", new { topicId = comment.TopicId });
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult EditComment(Comment comment)
        {
            string currentUserId = User.Identity.GetUserId();
            bool result = _commentRepo.EditComment(comment, currentUserId);
            if (result)
            {
                return RedirectToAction("GetComments", new { topicId = comment.TopicId });
            }
            return HttpNotFound();
        }


        [HttpPost]
        [AllowAnonymous]
        public ActionResult DeleteComment(Comment comment)
        {
            string currentUserId = User.Identity.GetUserId();
            var result = _commentRepo.DeleteComment(comment, currentUserId);
            if (result)
            {
                return RedirectToAction("GetComments", new { topicId = comment.TopicId });
            }
            return HttpNotFound();
        }

    }
}
