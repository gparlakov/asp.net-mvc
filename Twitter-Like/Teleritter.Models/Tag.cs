using System.Collections.Generic;

namespace Teleritter.Models
{
    public class Tag
    {
        private ICollection<Telereet> tweets;

        public Tag()
        {
            this.tweets = new List<Telereet>();
        }

        public int Id { get; set; }

        
        public string Name { get; set; }

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