namespace CoreLibrary.Handlers.Consumers
{
    public interface IReportConsumer
    {
        public object Data { get; set; }
        public void Handle();
    }
}
