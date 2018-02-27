﻿using System.Collections.Generic;

namespace FansPen.Web.Models.ViewModels
{
    public class CommentScriptModel
    {
        public int Id { get; set; }
        public PreviewUserViewModel User { get; set; }
        public List<PreviewUserViewModel> UsersLiked { get; set; }
        public string Text { get; set; }
        public string DataCreate { get; set; }
        public bool IsLiked { get; set; }
        public bool IsYour { get; set; }

        public CommentScriptModel(CommentViewModel commentView, List<PreviewUserViewModel> users, string currentUser, bool isAdmin)
        {
            Id = commentView.Id;
            User = commentView.ApplicationUser;
            Text = commentView.Text;
            DataCreate = commentView.DataCreate.ToShortDateString();
            IsLiked = false;
            IsYour = commentView.ApplicationUser.Id == currentUser || isAdmin;
            UsersLiked = new List<PreviewUserViewModel>();
            foreach(var like in commentView.Likes)
            {
                foreach(var user in users)
                {
                    if(like.ApplicationUserId == user.Id)
                    {
                        if(user.Id == currentUser)
                        {
                            IsLiked = true;
                        }
                        UsersLiked.Add(user);
                        break;
                    }
                }
            }
        }
    }
}
