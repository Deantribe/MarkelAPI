using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarkelAPI.Models
{
    public class Claims
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string UCR { get; set; }
        public int CompanyID { get; set; }
        public DateTime ClaimDate { get; set; }
        public DateTime LossDate { get; set; }
        public string Assured_Name { get; set; }
        public float Incurred_Loss { get; set; }
        public bool Closed { get; set; }
    }

    public class ClaimDetails
    {
        public ClaimDetails(Claims claim)
        {
            Claim = claim;
        }
        public Claims Claim { get; set; }
        public int AgeOfClaimInDays => DateTime.Now.Subtract(Claim?.ClaimDate ?? DateTime.Now).Days ;
    }
}
