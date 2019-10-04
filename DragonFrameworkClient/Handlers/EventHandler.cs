using System;
using CitizenFX.Core;

namespace DragonFrameworkClient
{
    class EventHandler : BaseScript
    {
        ClientResourceStart ClientResourceStart = new ClientResourceStart();
        public EventHandler()
        {
            EventHandlers["onClientResourceStart"] += new Action<string>(ClientResourceStart.OnClientResourceStart);
        }
    }
}
