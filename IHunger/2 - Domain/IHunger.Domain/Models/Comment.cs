using System;
using System.Collections.Generic;
using System.Text;

namespace IHunger.Domain.Models
{
    public class Comment : Entity
    {
        public string Text { get; set; }
        public double Starts { get; set; }

        #region EFCRelations
        public Guid RatingId { get; set; }
        public Rating Rating { get; set; }

        #endregion
    }
}
