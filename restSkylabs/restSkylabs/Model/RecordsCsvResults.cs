using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using restSkylabs.Model;

namespace RecordsCsvResults.Models
{
    public class RecordsCsvResult : FileResult
    {
        private readonly List<Records> _recordsData;
        public RecordsCsvResult(List<Records> recordsData, string fileDownloadName) : base("text/csv")
        {
            _recordsData = recordsData;
            FileDownloadName = fileDownloadName;
        }
        public async override Task ExecuteResultAsync(ActionContext context)
        {
            var response = context.HttpContext.Response;
            context.HttpContext.Response.Headers.Add("Content-Disposition", new[] { "attachment; filename=" + FileDownloadName });
            using (var streamWriter = new StreamWriter(response.Body))
            {
                await streamWriter.WriteLineAsync(
                  $"ID_Records, Age, Workclass_ID"
                );
                foreach (var p in _recordsData)
                {
                    await streamWriter.WriteLineAsync(
                      $"{p.ID_Records}, {p.Age}, {p.Workclass_ID}"
                    );
                    await streamWriter.FlushAsync();
                }
                await streamWriter.FlushAsync();
            }
        }
    }
}