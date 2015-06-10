using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BardicTree.Models
{
    public class Story
    {
        public int StoryID { get; set; }

        [Required]
        [StringLength(100)]
        [Index("UIX_Story_title", IsUnique = true)]
        public string Title { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        [Required]
        public string OwnerUserID { get; set; }

        [ForeignKey("OwnerUserID")]
        public virtual ApplicationUser Owner { get; set; }

        public int RootNodeID { get; set; }

        [ForeignKey("RootNodeID")]
        public virtual Node RootNode { get; set; }
    }
}