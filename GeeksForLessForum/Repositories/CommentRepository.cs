using GeeksForLessForum.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace GeeksForLessForum.Repositories
{
    public class CommentRepository
    {
        public List<TopicMessageViewModel> GetAllComments(int topicId)
        {
            List<TopicMessageViewModel> comments = new List<TopicMessageViewModel>();
            using (var db = ApplicationDbContext.Create())
            {
                var commentsEntity = db.Comments.Where(c => c.TopicId == topicId);
                if (commentsEntity != null)
                {
                    commentsEntity = commentsEntity.OrderByDescending(commentTime => commentTime.Id);
                    var commentsOrder = commentsEntity.ToList();

                    foreach (var item in commentsOrder)
                    {
                        TopicMessageViewModel comment = new TopicMessageViewModel();
                        comment.Message = item.Message;
                        comment.MessagesDate = item.CommentedDate;
                        comment.Id = item.Id;
                        comment.AuthorMessage = item.UserName;
                        comments.Add(comment);
                    }
                    return comments;
                }
                return null;
            }
        }

        public void AddComment(Comment comment, string currentUserId, string currentUserName)
        {
            using (var db = ApplicationDbContext.Create())
            {
                ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);
                Comment commentEntity = new Comment
                {
                    TopicId = comment.TopicId,
                    CommentedDate = comment.CommentedDate,
                    Message = comment.Message,
                    UserName = currentUserName,
                    UserID = currentUserId.ToString()
                };
                db.Comments.Add(commentEntity);
                db.SaveChanges();
            }
        }

        public bool EditComment(Comment comment, string currentUserId)
        {
            using (var db = ApplicationDbContext.Create())
            {
                try
                {
                    var commentUserID = (from commentEntity in db.Comments
                                         where commentEntity.UserName == comment.UserName
                                         select commentEntity.UserID)
                                 .FirstOrDefault();

                    ApplicationUser currentUser = db.Users.FirstOrDefault(
                        x => x.Id == currentUserId
                        && x.Id == commentUserID);
                    if (currentUser != null)
                    {
                        foreach (var item in db.Comments.Where(c => c.Id == comment.Id && c.TopicId == comment.TopicId))
                        {
                            item.CommentedDate = comment.CommentedDate;
                            item.Message = comment.Message;
                        }
                        db.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var entityValidationErrors in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in entityValidationErrors.ValidationErrors)
                        {
                            // Response.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                        }
                    }
                    return false;
                }
            }
            return false;
        }

        public bool DeleteComment(Comment comment, string currentUserId)
        {
            using (var db = ApplicationDbContext.Create())
            {
                try
                {
                    string commentUserID = (from commentEntity in db.Comments
                                            where commentEntity.Id == comment.Id
                                            select commentEntity.UserID)
                                         .FirstOrDefault();

                    ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId
                                                                          && commentUserID == currentUserId);

                    if (currentUser != null)
                    {
                        var delete = (from commentEntity in db.Comments
                                      where commentEntity.Id == comment.Id
                                      && commentEntity.TopicId == comment.TopicId
                                      select commentEntity)
                                 .FirstOrDefault();

                        db.Comments.Remove(delete);
                        db.SaveChanges();
                        return true;
                    }

                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var entityValidationErrors in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in entityValidationErrors.ValidationErrors)
                        {
                            // Response.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                        }
                    }
                }
            }
            return false;
        }
    }
}