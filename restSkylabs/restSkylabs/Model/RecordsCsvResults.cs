using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using restSkylabs.Model;

namespace RecordsCsvResults.Models
{
    public class RecordsCsvResult : FileResult
    {
        private readonly IEnumerable<Records> _recordsData;
        public RecordsCsvResult(IEnumerable<Records> recordsData, string fileDownloadName) : base("text/csv")
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
                  $"ID_Records, Age, Workclass_ID, Workclass, Education_Level_ID, Education_Level," +
                  $"Education_Num, Marital_Status_ID, Marital_Status, Occupation_ID, Occupation," +
                  $"Relationship_ID, Relationship, Race_ID, Race, Sex_ID, Sex, Capital_Gain, Capital_Loss," +
                  $"Hours_week, Country_ID, Country, Over_50k"
                );
                foreach (var p in _recordsData)
                {
                    await streamWriter.WriteLineAsync(
                      $"{p.ID_Records}, {p.Age}, {p.Workclass_ID}, {p.Workclasses.Name}, {p.Education_Levels}, {p.Education_Levels.Name}" +
                      $"{p.Education_Num}, {p.Marital_Status_ID}, {p.Marital_Statuses.Name}, {p.Occupation_ID}, {p.Occupations.Name}," +
                      $"{p.Relationship_ID}, {p.Relationships.Name}, {p.Race_ID}, {p.Races.Name}, {p.Sex_ID}, {p.Sexes.Name}, {p.Capital_Gain}, {p.Capital_Loss}," +
                      $"{p.Hours_week}, {p.Country_ID}, {p.Countries.Name}, {p.Over_50k}"
                    );
                    await streamWriter.FlushAsync();
                }
                await streamWriter.FlushAsync();
            }
        }
    }
}