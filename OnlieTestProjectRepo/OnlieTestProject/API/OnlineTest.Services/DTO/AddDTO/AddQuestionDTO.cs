﻿using System.ComponentModel.DataAnnotations;

namespace OnlineTest.Services.DTO.AddDTO
{
    public class AddQuestionDTO
    {

        [Required(ErrorMessage = "Index name is required. Please Enter Index name")]
        public string QuestionName { get; set; }


        [Required(ErrorMessage = "This field is required")]
        public int TestId { get; set; }


        [Required(ErrorMessage = "Question is required. Please Enter Question")]
        public string Que { get; set; }


        [Required(ErrorMessage = "This field is required")]
        public int Type { get; set; }


        [Required(ErrorMessage = "This field is required")]
        public int Weightage { get; set; }



        [Required(ErrorMessage = "This field is required")]
        public int SortOrder { get; set; }

        public int CreatedBy { set; get; }

        public DateTime CreatedOn { set; get; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;

    }
}
