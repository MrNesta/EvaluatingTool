using Microsoft.AspNet.SignalR;

namespace EvaluatingTool.BLL.Infrastructure
{
    public class ProgressHub : Hub
    {
        public static void SendMessage(string msg, int count)
        {
            string message;
            message = msg != "" ? "Process completed for " + msg : "";
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<ProgressHub>();
            hubContext.Clients.All.sendMessage(string.Format(message), count);
        }
    }
}