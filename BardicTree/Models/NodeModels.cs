using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BardicTree.Models
{
    public class Node
    {
        public int NodeID { get; set; }
        public string Title { get; set; }
        public string BodyText { get; set; }
        public virtual ICollection<NodeChoice> NodeChoices { get; set; }
        public DateTime CreationDate { get; set; }

        public string CreatorUserID { get; set; }

        [ForeignKey("CreatorUserID")]
        public virtual ApplicationUser Creator { get; set; }
        public virtual ICollection<NodeVisit> Visits { get; set; }
    }

    public class NodeChoice
    {
        public int NodeChoiceID { get; set; }
        public int childNodeID { get; set; }
        public string text { get; set; }
        public int NodeID { get; set; }
        public virtual Node Node { get; set; }
    }

    public class NodeVisit
    {
        public int NodeVisitID { get; set; }
        public int NodeID { get; set; }
        public string UserID { get; set; }
        public DateTime Visit { get; set; }

        public virtual Node Node { get; set; }
        public virtual ApplicationUser User { get; set; }
    }

    public class UIAddStoryElement
    {
        [Display(Name = "Choice")]
        public string ChoiceText { get; set; }

        [Display(Name = "Title")]
        public string TitleText { get; set; }

        [Display(Name = "Story")]
        public string StoryText { get; set; }

        public int ParentNode { get; set; }
    }
}