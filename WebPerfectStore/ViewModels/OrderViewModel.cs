using System.Collections.Generic;
using WebPerfectStore.Models; 

namespace WebPerfectStore.ViewModels
{
    public class OrderViewModel
    {
        public Order Order { get; set; }
        public List<QuestionViewModel> Questions { get; set; }
    }
}