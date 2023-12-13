using Core.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Core.Helpers
{
    [DataContract]
    public class ActionOutput<T> where T : class, new()
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


        private static IActionResult Generate<T>(IServiceOutput<T>? serviceOutput) where T : class, new()
        {
            ObjectResult output = new(serviceOutput)
            {
                StatusCode = serviceOutput?.Code ?? 204
            };

            return output;
        }

        private static IActionResult Generate<T>(int code, bool status = false, string? message = null, int? rowCount = null, int? totalCount = null, T? data = null) where T: class, new()
        {
            IServiceOutput<T> serviceOutput = new ServiceOutput<T>
            {
                Code = code,
                Status = status,
                Message = message,
                RowCount = rowCount,
                TotalCount = totalCount,
                Data = data
            };

            ObjectResult output = new(serviceOutput)
            {
                StatusCode = serviceOutput?.Code ?? 204
            };

            return output;
        }

        public static async Task<IActionResult> GenerateAsync<T>(IServiceOutput<T> serviceOutput) where T: class, new()
        {
            return await Task.Run(() => Generate<T>(serviceOutput));
        }

        public static async Task<IActionResult> GenerateAsync<T>(int code, bool status = false, string? message = null, int? rowCount = null, int? totalCount = null, T? data = null) where T : class, new()
        {
            return await Task.Run(() => Generate<T>(code, status, message, rowCount, totalCount, data));
        }
    }
}
