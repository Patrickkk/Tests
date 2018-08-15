namespace RestberryPiApi.HostedService
{
    internal class ActionConfig
    {
        public string Name { get; set; }
        public EventTrigger Trigger { get; set; }
        public ReadI2CPinActionConfig Action { get; set; }
    }
}