namespace newTestApi.Models.DTO
{
    public class StudentDto
    {
        public Guid Id { get; set; }
        public string RegNo { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Remarks { get; set; }
        public Guid MarksId { get; set; }
    }
}
