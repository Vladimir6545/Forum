using GeeksForLessForum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeeksForLessForum.Repositories
{
    public class TopicRepository
    {
        public List<TopicsViewModel> GetAllTopics()
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
                return topics;
            }
        }

        public int AddTopic(string topic, string body, string currentUserId)
        {
            using (var db = ApplicationDbContext.Create())
            {
                ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);
                if (currentUser != null)
                {
                    Topic top = new Topic
                    {
                        Header = topic,
                        Body = body,
                        UserID = currentUser.Id.ToString()
                    };
                    db.Topics.Add(top);
                    db.SaveChanges();
                    return top.Id;
                }
                return 0;
            }
        }

        public TopicsViewModel GetTopic(int id)
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
                }
                catch (Exception)
                {
                    return null;
                }

            }
            return top;
        }
    }
}