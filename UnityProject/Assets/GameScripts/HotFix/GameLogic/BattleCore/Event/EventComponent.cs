using System.Collections.Generic;
using ET;
using TEngine;
using Log = ET.Log;

namespace GameLogic.Battle
{
    [ComponentOf]
    public class EventComponent : Entity, IAwake,IDestroy
    {
        //观察者
        public Dictionary<int, List<EntityRef<Entity>>> ObserverMap = new Dictionary<int, List<EntityRef<Entity>>>();
    }
    
    [EntitySystemOf((typeof(EventComponent)))]
    public static partial class EventComponentSystem
    {
        [EntitySystem]
        public static void Awake(this EventComponent self)
        {
            
        }
       

        public static void AddListener(this EventComponent self, int eventId, Entity owner)
        {
            EventQueue queue = self.GetChild<EventQueue>(eventId) ?? self.AddChildWithId<EventQueue>(eventId, true);
        }

        public static void RemoveListener(this EventComponent self, int eventId, Entity owner)
        {
            EventQueue queue = self.GetChild<EventQueue>(eventId);
            if (queue != null)
            {
                queue.Remove(owner);
            }
        }
        public static void SendEvent(this EventComponent self, int eventId)
        {
            var queue = self.GetChild<EventQueue>(eventId);
            queue?.Send();
        }
        
        public static void SendEvent<TArg1>(this EventComponent self, int eventId, TArg1 arg)
        {
            var queue = self.GetChild<EventQueue>(eventId);
            queue?.Send(arg);
        }
        public static void SendEvent<TArg1, TArg2>(this EventComponent self, int eventId, TArg1 arg1, TArg2 arg2)
        {
            var queue = self.GetChild<EventQueue>(eventId);
            queue?.Send(arg1, arg2);
        }
        
        public static void SendEvent<TArg1, TArg2,TArg3>(this EventComponent self, int eventId, TArg1 arg1, TArg2 arg2, TArg3 arg3)
        {
            var queue = self.GetChild<EventQueue>(eventId);
            queue?.Send(arg1, arg2, arg3);
        }
        
        public static async ETTask SendEventAsync(this EventComponent self, int eventId)
        {
            var queue = self.GetChild<EventQueue>(eventId);
            if(queue == null) return;
            await queue.SendAsync();
        }
        
        public static async ETTask SendEventAsync<TArg1>(this EventComponent self, int eventId, TArg1 arg)
        {
            var queue = self.GetChild<EventQueue>(eventId);
            if(queue == null) return;
            await queue.SendAsync(arg);
        }
        public static async ETTask SendEventAsync<TArg1, TArg2>(this EventComponent self, int eventId, TArg1 arg1, TArg2 arg2)
        {
            var queue = self.GetChild<EventQueue>(eventId);
            if(queue == null) return;
            await queue.SendAsync(arg1, arg2);
        }
        
        public static async ETTask SendEventAsync<TArg1, TArg2,TArg3>(this EventComponent self, int eventId, TArg1 arg1, TArg2 arg2, TArg3 arg3)
        {
            var queue = self.GetChild<EventQueue>(eventId);
            if(queue == null) return;
            await queue.SendAsync(arg1, arg2, arg3);
        }
        
    }
}