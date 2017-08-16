using System;
using Microsoft.EntityFrameworkCore;

namespace TheWall.Models
{
    public class Comment
    {
        public int CommentId {get; set;}

        public int UserId {get; set;}
        public User User {get; set;}
        public int MessageId {get; set;}
        public Message Message {get; set;}
        public string Content {get; set;}
        public DateTime CreatedAt {get; set;}
        public DateTime UpdatedAt {get; set;}

    }
}