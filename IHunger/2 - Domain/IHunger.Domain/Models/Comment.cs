using System;
using System.Collections.Generic;
using System.Text;

namespace IHunger.Domain.Models
{
    public class Comment : Entity
    {
        public string Text { get; set; }
        public decimal Starts { get; set; }

        #region EFCRelations
        public Guid IdRating { get; set; }
        public virtual Rating Rating { get; set; }
        public Guid IdRestaurant { get; set; }
        public virtual Restaurant Restaurant { get; set; }

        #endregion

        public Comment()
        {

        }
    }
}
