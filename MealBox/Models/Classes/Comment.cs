using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MealBox.Models.Classes
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        //yorum yapan kullanıcı
        public string CommentUser{ get; set; }

        public DateTime CommentDate { get; set; }
        public string CommentContent { get; set; } 
        public bool CommentStatus {  get; set; }

        // Yorumun hangi kullanıcıya ait olduğunu belirtmek için UserId
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
