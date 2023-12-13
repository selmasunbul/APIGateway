namespace Core.Abstract
{
    public interface IServiceOutput<T> where T : class, new()
    {
        public int Code { get; set; }
        public bool Status { get; set; }
        public string? Message { get; set; }
        public int? RowCount { get; set; }
        public int? TotalCount { get; set; }
        public T? Data { get; set; }
    }


}
