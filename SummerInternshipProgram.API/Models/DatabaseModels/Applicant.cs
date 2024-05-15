﻿using System.ComponentModel.DataAnnotations;

namespace SummerInternshipProgram.API.Models.DatabaseModels
{
    public class Applicant
    {

        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string? Phone { get; set; }

        public bool? IsPhoneInternal { get; set; }

        public bool? IsPhoneHidden { get; set; }

        public string? Nationality { get; set; }
        public bool? IsNationalityInternal { get; set; }

        public bool? IsNationalityHidden { get; set; }

        public string? CurrentResidence { get; set; }

        public bool? IsCurrentResidenceInternal { get; set; }

        public bool? IsCurrentResidenceHidden { get; set; }

        public string? IDNumber { get; set; }

        public bool? IsIDNumberInternal { get; set; }

        public bool? IsIDNumberHidden { get; set; }

        public DateTime DateOfBirth { get; set; }

        public bool? IsDateOfBirthInternal { get; set; }

        public bool? IsDateOfBirthHidden { get; set; }

        public Gender Gender { get; set; }

        public List<DateQuestion>? DateAnswers { get; set; }
        public List<DropdownQuestion>? DropdownAnswers { get; set; }
        public List<MultipleChoiceQuestion>? MultipleChoiceAnswers { get; set; }
        public List<NumericQuestion>? NumericAnswers { get; set; }
        public List<ParagraphQuestion>? ParagraphAnswers { get; set; }
        public List<YesOrNoQuestion>? YesOrNoAnswers { get; set; }

    }
}
