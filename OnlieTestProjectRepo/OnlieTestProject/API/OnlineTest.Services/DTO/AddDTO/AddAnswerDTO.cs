﻿using Castle.MicroKernel.SubSystems.Conversion;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineTest.Services.DTO.AddDTO
{
    public class AddAnswerDTO
    {
        [Required(ErrorMessage = "Ans is required. Please Enter Ans")]
        public string Ans { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { set; get; }
        public DateTime CreateOn { set; get; }
        public int TestId { set; get; }
        public int QuestionId { set; get; }
    }
}
