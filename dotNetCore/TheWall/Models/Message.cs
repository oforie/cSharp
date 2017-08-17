using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TheWall.Models
{
    public class Message
    {
        public int MessageId {get; set;}
        public int UserId {get; set;}
        public User User {get; set;}

        [Required(ErrorMessage="Cannot save an empty message")]
        [MinLength(4)]
        public string Content {get; set;}

        public List<Comment> Comments { get; set; }
        public DateTime CreatedAt {get; set;}
        public DateTime UpdatedAt {get; set;}

        public Message()
        {
             Comments = new List<Comment>();
        }
        public Message(string message)
        {
            Content = message;
            Comments = new List<Comment>();
        }
    }
}