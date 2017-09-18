﻿namespace H.Skeepy.API.Contracts.Notifications
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

        public override string ToString()
        {
            return $"\"{Name}\" <{Address}>";
        }
    }
}