namespace AbTest.Model
{
    public class ExperimentsReport
    {
        public long DeviceTotalCount { get; set; }
        public List<ExperimentsDetail> ExperimentsDetails { get; set; }
    }

    public class ExperimentsDetail
    {
        public int DeviceCount { get; set; }
        public string ExperimentKey { get; set; }
        public Dictionary<string, int> ExperimentValueCount { get; set; }
    }
}
