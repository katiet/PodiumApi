namespace Podium.Models
{
    public class Lender
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public decimal InterestRate { get; set; }

        public LenderType LenderType { get; set; }

        public decimal LoanToValue { get; set; }
    }
}
