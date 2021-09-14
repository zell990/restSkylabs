using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

    namespace restSkylabs.Model
{

    public class RecordsStatistics
    {
        public string AggregationType { get; set; }
        public int AggregationFilter { get; set; }
        public double Capital_Gain_Sum { get; set; }
        public double Capital_Gain_Avg { get; set; }
        public double Capital_Loss_Sum { get; set; }
        public double Capital_Loss_Avg { get; set; }
        public int Over_50k_count { get; set; }
        public int Under_50k_count { get; set; }
    }

}