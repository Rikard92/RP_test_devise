namespace RP_test_devise.Models
{
    public class TestDevise
    {
        //This is the model that is used here and in the DB
        public long Id { get; set; }
        public string? Name { get; set; }
        public int? Amount { get; set; }   
        public bool IsComplete { get; set; }
        public string? SecretVal { get; set; }
    }
}
