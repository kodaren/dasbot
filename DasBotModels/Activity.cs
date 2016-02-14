namespace DasBotModels
{
    public enum ActivityType
    {
        None, Sailing, Swim, Fishing
    }

    public class Activity
    {
        public string Description { get; set; }
        public bool Status { get; set; }
        public ActivityType Name { get; set; }

        public string Data { get; set; }
    }
}