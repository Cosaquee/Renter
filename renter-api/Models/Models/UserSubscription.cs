using System;

namespace Models.Models
{
    public class UserSubscription : BaseEntity
    {
        public long Id { get; set; }

        public int SubscriptionId { get; set; }

        public virtual Subscription Subscription { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public DateTime StartDate { get; set; }
    }
}
