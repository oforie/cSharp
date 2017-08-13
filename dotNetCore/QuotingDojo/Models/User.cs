using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace QuotingDojo.Models
{
    public class User : BaseEntity
    {
        public int UserId;
        
        [Required]
        [MinLength(2)]
        public string Name;
        public List<Message> userMessage; 
        public User(string username)
        {
            Name = username;
            userMessage = new List<Message>();

        }

        // public List<Message> UserMessage { get => userMessage; set => userMessage = value; }
    }

    

}