using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace App.Models
{
    public class Laptop
    {
        private ICollection<Vote> votes;

        private ICollection<Comment> comments;

        public int Id { get; set; }

        [Required]
        public string Model { get; set; }

        /// <summary>
        /// Monitor size in inches
        /// </summary>
        [Required]
        public double Monitor { get; set; }

        /// <summary>
        /// Hard disk size in GB
        /// </summary>
        [Required]
        public int HardDisk { get; set; }

        /// <summary>
        /// RAM size in GB
        /// </summary>
        [Required]
        public int Ram { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        /// <summary>
        /// Hard disk size in $(chineese)
        /// </summary>
        [Required]
        public decimal Price{ get; set; }
        
        /// <summary>
        /// In kg
        /// </summary>
        public double Weight { get; set; }


        public string AdditionaParts { get; set; }

        public string Description { get; set; }

        public Laptop()
        {
            this.votes = new List<Vote>();
            this.comments = new List<Comment>();
        }

        public virtual ICollection<Vote> Votes
        {
            get
            {
                return this.votes;
            }
            set
            {
                this.votes = value;
            }
        }

        public virtual ICollection<Comment> Comments
        {
            get
            {
                return this.comments;
            }
            set
            {
                this.comments = value;
            }
        }

        public int ManufacturerId { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }
    }
}
