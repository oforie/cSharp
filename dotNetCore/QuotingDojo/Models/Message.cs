using System.ComponentModel.DataAnnotations;

namespace QuotingDojo.Models
{
    public class Message : BaseEntity
    {
      
        public int MessageId;
        [Required]
        public int UserId;

        [Required]
        [MinLength(8)]
        public string Content;

        public Message(string message)
        {
            Content = message;
        }
    }

    

}