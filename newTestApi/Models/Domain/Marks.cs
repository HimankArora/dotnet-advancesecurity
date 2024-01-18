namespace newTestApi.Models.Domain
{
    public class Marks
    {
        public Guid Id { get; set; }
        public string StudentRegNo { get; set; }
        public string Physics { get; set; }
        public string Chemistry { get; set; }
        public string Maths { get; set; }
        public Boolean Pass { get; set; }
    }
}
