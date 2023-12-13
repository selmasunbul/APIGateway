using Core.Abstract;
using System.Runtime.Serialization;

namespace Core.Helpers
{
    [DataContract]
    public class ServiceOutput<T> : IServiceOutput<T> where T : class, new()
    {
        [DataMember]
        public int Code { get; set; } = 100;

        [DataMember]
        public bool Status { get; set; } = false;

        [DataMember]
        public string? Message { get; set; }
        [DataMember]
        public int? RowCount { get; set; }

        [DataMember]
        public int? TotalCount { get; set; }

        [DataMember]
        public T? Data { get; set; }


        private static IServiceOutput<T> Generate<T>(IServiceOutput<T> serviceOutput) where T : class, new()
        {
            return serviceOutput;
        }

        private static IServiceOutput<T> Generate<T>(int code, bool status = false, string? message = null, int? rowCount = null, int? totalCount = null, T? data = null) where T : class, new()
        {
            IServiceOutput<T> output = new ServiceOutput<T>
            {
                Code = code,
                Status = status,
                Message = message,
                RowCount = rowCount,
                TotalCount = totalCount,
                Data = data
            };

            return output;
        }

        public static async Task<IServiceOutput<T>> GenerateAsync<T>(IServiceOutput<T> serviceOutput) where T : class, new()
        {
            return await Task.Run(() => Generate(serviceOutput));
        }

        public static async Task<IServiceOutput<T>> GenerateAsync(int code, bool status = false, string? message = null, int? rowCount = null, int? totalCount = null, T? data = null)
        {
            return await Task.Run(() => Generate(code, status, message, rowCount, totalCount, data));
        }

    }
}

