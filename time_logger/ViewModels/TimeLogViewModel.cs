namespace time_logger.ViewModels
{
    public class TimeLogViewModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string ProjectName { get; set; }
        public DateTime Date { get; set; }
        public float TimeWorked { get; set; }
    }

    public class TopUserOrProjectViewModel
    {
        public string Name { get; set; }
        public float TotalHours { get; set; }
    }
    public class UserComparisonViewModel
    {
        public DateTime Date { get; set; }
        public float HoursWorked { get; set; }
    }
}
