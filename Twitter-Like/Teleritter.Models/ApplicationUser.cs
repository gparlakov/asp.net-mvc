using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace Teleritter.Models
{
    public class ApplicationUser : User
    {
        private ICollection<Telereet> tweets;

        public ApplicationUser()
        {
            this.tweets = new List<Telereet>();
        }

        public virtual ICollection<Telereet> Tweets
        {
            get
            {
                return this.tweets;
            }
            set
            {
                this.tweets = value;
            }
        }
    }
}