namespace H.Skeepy.API.Notifications
{
    public class NotificationDestination
    {
        public readonly string Name;
        public readonly string Address;

        public NotificationDestination(string address, string name)
        {
            Address = address;
            Name = name;
        }
    }
}