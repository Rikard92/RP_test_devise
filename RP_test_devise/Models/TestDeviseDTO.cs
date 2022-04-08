namespace RP_test_devise.Models
{
    public class TestDeviseDTO
    {
        //This is a DTO class used to prevent over-posting.
        public long Id { get; set; }
        public string? Name { get; set; }
        public int? Amount { get; set; }   
        public bool IsComplete { get; set; }
    }
}
