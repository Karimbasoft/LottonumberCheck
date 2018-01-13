using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace TestApp.Services
{
    public class FormsMessengerService : IMessenger
    {
        private static readonly Lazy<FormsMessengerService> Lazy =
            new Lazy<FormsMessengerService>(() => new FormsMessengerService());

        public static IMessenger Instance => Lazy.Value;

        private FormsMessengerService()
        {
        }
        public void Send<TMessage>(TMessage message, object sender = null) where TMessage : IMessage
        {
            if (sender == null)
                sender = new object();

            MessagingCenter.Send(sender, typeof(TMessage).FullName, message);
        }

        public void Subscribe<TMessage>(object subscriber, Action<object, TMessage> callback) where TMessage : IMessage
        {
            MessagingCenter.Subscribe(subscriber, typeof(TMessage).FullName, callback);
        }

        public void Unsubscribe<TMessage>(object subscriber) where TMessage : IMessage
        {
            MessagingCenter.Unsubscribe<object, TMessage>(subscriber, typeof(TMessage).FullName);
        }
    }
}
