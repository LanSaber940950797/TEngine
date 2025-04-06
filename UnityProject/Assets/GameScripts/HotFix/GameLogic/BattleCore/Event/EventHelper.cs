using System;
using ET;

namespace GameLogic.Battle
{
    public static class EventHelper
    {
        public static void SendEvent(this Entity self, int eventId)
        {
            self.GetComponent<EventComponent>()?.SendEvent(eventId);
        }
        
        public static void SendEvent<TArg1>(this Entity self, int eventId, TArg1 arg1)
        {
            self.GetComponent<EventComponent>()?.SendEvent(eventId, arg1);
        }
        
        public static void SendEvent<TArg1, TArg2>(this Entity self, int eventId, TArg1 arg1, TArg2 arg2)
        {
            self.GetComponent<EventComponent>()?.SendEvent(eventId, arg1, arg2);
        }
        
        public static void SendEvent<TArg1, TArg2, TArg3>(this Entity self, int eventId, TArg1 arg1, TArg2 arg2, TArg3 arg3)
        {
            self.GetComponent<EventComponent>()?.SendEvent(eventId, arg1, arg2, arg3);
        }
        
        //发送事件异步，事件尽量少异步
        public static async ETTask SendEventAsync(this Entity self, int eventId)
        {
            var eventComponent = self?.GetComponent<EventComponent>();
            if (eventComponent != null) await  eventComponent.SendEventAsync(eventId);
        }
        
        public static async ETTask SendEventAsync<TArg1>(this Entity self, int eventId, TArg1 arg1)
        {
            var eventComponent = self?.GetComponent<EventComponent>();
            if (eventComponent != null) await  eventComponent.SendEventAsync(eventId, arg1);
        }
        
        public static async ETTask SendEventAsync<TArg1, TArg2>(this Entity self, int eventId, TArg1 arg1, TArg2 arg2)
        {
            var eventComponent = self?.GetComponent<EventComponent>();
            if (eventComponent != null) await  eventComponent.SendEventAsync(eventId, arg1, arg2);
        }
        
        public static async ETTask SendEventAsync<TArg1, TArg2, TArg3>(this Entity self, int eventId, TArg1 arg1, TArg2 arg2, TArg3 arg3)
        {
            var eventComponent = self?.GetComponent<EventComponent>();
            if (eventComponent != null) await  eventComponent.SendEventAsync(eventId, arg1, arg2, arg3);
        }
        


        private static EventComponent GetOrCreateEventComponent(this Entity self)
        {
            var componet = self.GetComponent<EventComponent>();
            if(componet == null)
            {
                componet = self.AddComponent<EventComponent>(true);
            }

            return componet;
        }
        public static void AddEventListener(this Entity self, int eventId,  Entity owner)
        {
            self.GetOrCreateEventComponent().AddListener(eventId,  owner);
        }
        
        public static void RemoveEventListener(this Entity self, int eventId,  Entity owner)
        {
            self?.GetComponent<EventComponent>()?.RemoveListener(eventId,  owner);
        }

    }
}