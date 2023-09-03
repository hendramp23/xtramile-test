using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xtramile.WheatherApp.Core;
using Xtramile.WheatherApp.Core.Enum;

namespace Xtramile.WheatherApp.ServiceContract.Response
{
    public class BasicResponse
    {
        #region Fields

        private Collection<Message> messages = new Collection<Message>();

        #endregion

        #region Properties

        public Collection<Message> Messages => messages ?? (messages = new Collection<Message>());

        #endregion

        #region (public) Methods

        public bool IsError()
        {
            return Messages.Count(item => item.Type == MessageType.Error) > 0;
        }

        public bool IsContainInfo()
        {
            return Messages.Count(item => item.Type == MessageType.Info) > 0;
        }

        public string[] GetMessageTextArray()
        {
            return Messages.Select(item => item.MessageText).ToArray();
        }

        public string[] GetMessageErrorTextArray()
        {
            return Messages.Where(item => item.Type == MessageType.Error)
                .Select(item => item.MessageText)
                .ToArray();
        }

        public string[] GetMessageInfoTextArray()
        {
            return Messages.Where(item => item.Type == MessageType.Info)
                .Select(item => item.MessageText)
                .ToArray();
        }

        public string GetErrorMessage()
        {
            var messageBuilder = new StringBuilder();
            foreach (var message in Messages)
            {
                messageBuilder.AppendLine(message.MessageText);
            }

            return messageBuilder.ToString().Trim();
        }

        public void AddErrorMessage(string errorMessage)
        {
            Messages.Add(new Message
            {
                MessageText = errorMessage,
                Type = MessageType.Error,
            });
        }

        public void AddInfoMessage(string infoMessage)
        {
            Messages.Add(new Message
            {
                MessageText = infoMessage,
                Type = MessageType.Info,
            });
        }

        #endregion
    }
}
