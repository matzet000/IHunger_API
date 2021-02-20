using System;
using System.Collections.Generic;
using System.Text;

namespace IHunger.Domain.Models
{
    public class Rating : Entity
    {
        public double Starts { get; set; }

        #region EFCRelations
        public IEnumerable<Comment> Comments { get; set; }
        #endregion
    }
}
