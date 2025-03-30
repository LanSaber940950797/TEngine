using System;
using ET;

namespace GameLogic.Battle
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class EventDispatcherAttribute : BaseAttribute
    {
        public int EventId;

        public EventDispatcherAttribute(int eventId)
        {
            this.EventId = eventId;
        }
    }
}