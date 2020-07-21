using GigHub.Models;
using System.Data.Entity.ModelConfiguration;

namespace GigHub.Persistence.EntityConfigurations
{
    public class UserNotificationConfiguration : EntityTypeConfiguration<Notification>
    {
        public UserNotificationConfiguration()
        {
            HasRequired(n => n.Gig);
        }
    }
}