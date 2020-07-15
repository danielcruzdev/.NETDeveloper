using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GigHub.Models
{
    public class Notification
    {
        protected Notification()
        {

        }

        public Notification(NotificationType type, Gig gig)
        {
            Type = type;
            Gig = gig ?? throw new ArgumentNullException("Null gig");
            DateTime = DateTime.Now;
        }

        public Notification(NotificationType type, Gig gig, DateTime dateTime)
        {
            Type = type;
            Gig = gig ?? throw new ArgumentNullException("Null gig");
            DateTime = dateTime;
            OriginalDateTime = DateTime.Now;
        }



        public int Id { get; private set; }

        public DateTime DateTime { get; private set; }

        public NotificationType Type { get; private set; }

        public DateTime? OriginalDateTime { get; set; }

        public string OriginalValue { get; set; }

        [Required]
        public Gig Gig { get; private set; }
    }
}