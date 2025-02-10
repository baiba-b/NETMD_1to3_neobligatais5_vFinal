using bb23028_MD1;
using CommunityToolkit.Mvvm.Messaging.Messages;
namespace bb23028MD2;

public class UpdateResultMessage : ValueChangedMessage<Collections> //Klase, ko izmanto Tookkit messaging
{
    public UpdateResultMessage(Collections message) :base(message) { }
    
}
