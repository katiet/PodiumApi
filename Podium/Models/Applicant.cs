using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Podium.Models
{
    public class Applicant
    {
        public int Id { get; set; }
        public string FirstName { get; set;  }
        public  string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }

        public List<MortgageRequirement> MortgageRequirements { get; set; }

    }
}
