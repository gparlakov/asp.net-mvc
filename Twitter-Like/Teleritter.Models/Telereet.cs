using System;
using System.Collections.Generic;
using System.Linq;

namespace Teleritter.Models
{
    public class Telereet
    {
        private ICollection<Tag> tags;

        public Telereet()
        {
            this.tags = new List<Tag>();
        }

        public int Id { get; set; }
        
        public string Text { get; set; }

        public int TagId { get; set; }

        public DateTime PostedOn { get; set; }

        public virtual ICollection<Tag> Tags
        {
            get
            {
                return this.tags;
            }
            set
            {
                this.tags = value;
            }
        }

        public virtual ApplicationUser Author { get; set; }
    }
}