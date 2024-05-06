namespace MovieApi.DTO.ResponseTO
{
    public class FullCrewResponceTo
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Patronymic { get; set; }
        public string? PhotoPath { get; set; }
        public int Age { get; set; }
        public DateOnly? Date { get; set; }
    }
}
