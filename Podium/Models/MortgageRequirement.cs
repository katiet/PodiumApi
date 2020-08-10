namespace Podium.Models
{
    public class MortgageRequirement
    {
        public int Id { get; set; }
        public int ApplicantId { get; set; }
        public Applicant Applicant { get; set; }
        public decimal PropertyValue { get; set;  }
        public decimal DepositAmount { get; set; }
        
    }
}
