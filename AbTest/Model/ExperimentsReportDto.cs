namespace AbTest.Model
{
    public class ExperimentsReportDto
    {
        public long DeviceWithExperimentCount { get; set; }
        public List<ExperimentListItem> Experiments { get; set; }
        public List<ExperimentsDetail> ExperimentsDetails { get; set; }
    }

    public class ExperimentsDetail
    {    
        public string ExperimentKey { get; set; }
        public Dictionary<string, int> ExperimentValueFrequency { get; set; }
    }

    public class ExperimentListItem
    {
        public string ExperimentKey { set; get; }
        public Dictionary<string, double> ValueProbability { set; get; }
    }
}
