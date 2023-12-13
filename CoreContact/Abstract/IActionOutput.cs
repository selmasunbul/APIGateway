namespace Core.Abstract
{
    public interface IActionOutput
    {
        public int Code { get; set; }
        public bool Status { get; set; }
        public string? Message { get; set; }
        public int? RowCount { get; set; }
        public int? TotalCount { get; set; }
        public object? Data { get; set; }
    }
}
