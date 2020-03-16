using Microsoft.AspNetCore.Mvc.Rendering;
using RevisionApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RevisionApplication.ViewModels
{
    public class QuestionViewModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string Answer1 { get; set; }
        public string Answer2 { get; set; }
        public string Answer3 { get; set; }
        public string Answer4 { get; set; }
        public int CorrectAnswer { get; set; }
        public string Reference { get; set; }
        public List<string> Units { get; set; }
        public string SelectedUnit { get; set; }

        public QuestionViewModel()
        {
            Units = new List<string>();
        }
    }
}
