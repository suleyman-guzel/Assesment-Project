using CoreLibrary.Handlers.Consumers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLibrary.Utilities.MessageBrokers
{
    public interface IMessageBroker
    {
        public void MessagePublishObject<T>(T obj, string queueName);

        bool ConsumeQueue(string queueName, IReportConsumer reportConsumer);
    }
}
