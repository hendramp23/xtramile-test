using Xtramile.WheatherApp.Core.Enum;

namespace Xtramile.WheatherApp.Core
{
    public class Message
    {
        public MessageType Type { get; set; }

        public string MessageText { get; set; } = string.Empty;
    }
}
