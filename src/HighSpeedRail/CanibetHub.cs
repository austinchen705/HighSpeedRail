using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;

namespace HighSpeedRail
{
    public class CanibetHub : Hub
    {

        public void UpdateCanibet(bool isUsing, int caniBetID, string functionType) 
        {
            dynamic message = new { isUsing = isUsing, CanibetID = caniBetID, FunctionType = functionType };

            Clients.All.broadcastMessage(message);
        }

        public override Task OnDisconnected(bool isStop)
        {
            var cid = this.Context.ConnectionId;
            //UserHandler.ConnectedIds.Remove(this.Context.ConnectionId);
            //if (UserHandler.ConnectedIds.Count == 0)
            //{
            //    // STOP TASK -> SET CANCELLATION
            //}
            return base.OnDisconnected(isStop);
        }
    }
}