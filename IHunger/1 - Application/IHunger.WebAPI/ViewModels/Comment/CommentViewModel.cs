using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IHunger.WebAPI.ViewModels.Comment
{
    public class CommentViewModel : BaseViewModel
    {
        public string Text { get; set; }
        public decimal Starts { get; set; }
    }
}
