using Mensagem.MessageBus;

namespace OrderProducerAPI
{
    public interface IOrderRepository
    {
        void SendMessage(BaseMessage baseMessage, string queueName);
    }
}
