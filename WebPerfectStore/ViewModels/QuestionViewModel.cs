using System.Collections.Generic;
using WebPerfectStore.Models; 
namespace WebPerfectStore.ViewModels
{

    public class QuestionViewModel
    {
        public QuestionViewModel() { }
        public QuestionViewModel(Item item, List<AnswerViewModel> answers)
        {
            Item = item;
            Answers = answers;
        }
        public Item Item { get; set; }
        public List<AnswerViewModel> Answers { get; set; }
    }
}
