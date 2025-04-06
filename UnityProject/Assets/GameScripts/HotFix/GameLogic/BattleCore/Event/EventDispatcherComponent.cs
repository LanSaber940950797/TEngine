using System;
using System.Collections.Generic;
using ET;

namespace GameLogic.Battle
{
    public struct EventDispatcherInfo
    {
        public int EventId { get; }
        public IEventDispatcherBase EventDispatcher { get; }

        public EventDispatcherInfo(int eventId, IEventDispatcherBase dispatcher)
        {
            this.EventId = eventId;
            this.EventDispatcher = dispatcher;
        }
    }
    
    [Code]
    public class EventDispatcherComponent : Singleton<EventDispatcherComponent>, ISingletonAwake
    {
        private readonly UnOrderMultiMap<Type, EventDispatcherInfo> allWatchers = new();
        public void Awake()
        {
            HashSet<Type> types = CodeTypes.Instance.GetTypes(typeof(EventDispatcherAttribute));
            foreach (Type type in types)
            {
                //如果类型不是类
                if (!type.IsClass)
                {
                    continue;
                }
                object[] attrs = type.GetCustomAttributes(typeof(EventDispatcherAttribute), false);

                foreach (object attr in attrs)
                {
                    EventDispatcherAttribute eventDispatcherAttribute = (EventDispatcherAttribute)attr;
                    IEventDispatcherBase obj = (IEventDispatcherBase)Activator.CreateInstance(type);
                    EventDispatcherInfo info = new(eventDispatcherAttribute.EventId, obj);
                    allWatchers.Add(obj.ObserverType, info);
                    
                }
            }
        }
        
        public async ETTask Run(Entity observer, int eventId)
        {
            List<EventDispatcherInfo> list = allWatchers[observer.GetType()];
            if (list == null)
            {
                return;
            }

            foreach (var info in list)
            {
                if (info.EventId == eventId && info.EventDispatcher is IEventDispatcher dispatcher)
                {
                    await dispatcher.Handle(observer);
                    break;
                }
            }
            foreach (var info in list)
            {
                if (info.EventId == BattleEvent.AllEvent && info.EventDispatcher is IEventDispatcherMulParam mulParamDispatcher)
                {
                    await mulParamDispatcher.Handle(observer,eventId);
                    break;
                }
            }
        }
        
        public async ETTask Run<A>(Entity observer, int eventId, A a)
        {
            List<EventDispatcherInfo> list = allWatchers[observer.GetType()];
            if (list == null)
            {
                return;
            }

            foreach (var info in list)
            {
               
                if (info.EventId == eventId && info.EventDispatcher is IEventDispatcher<A> dispatcher)
                {
                    await dispatcher.Handle(observer, a);
                    break;
                }
            }
            
            foreach (var info in list)
            {
                if (info.EventId == BattleEvent.AllEvent && info.EventDispatcher is IEventDispatcherMulParam mulParamDispatcher)
                {
                    await mulParamDispatcher.Handle(observer,eventId, a);
                    break;
                }
            }
        }
        
        public async ETTask Run<A, B>(Entity observer, int eventId, A a, B b)
        {
            List<EventDispatcherInfo> list = allWatchers[observer.GetType()];
            if (list == null)
            {
                return;
            }

            foreach (var info in list)
            {
                if (info.EventId == eventId && info.EventDispatcher is IEventDispatcher<A, B> dispatcher)
                {
                    await dispatcher.Handle(observer, a, b);
                    break;
                }
            }
            
            foreach (var info in list)
            {
                if (info.EventId == BattleEvent.AllEvent && info.EventDispatcher is IEventDispatcherMulParam mulParamDispatcher)
                {
                    await mulParamDispatcher.Handle(observer,eventId, a, b);
                    break;
                }
            }
        }
        
        public async ETTask Run<A, B, C>(Entity observer, int eventId, A a, B b, C c)
        {
            List<EventDispatcherInfo> list = allWatchers[observer.GetType()];
            if (list == null)
            {
                return;
            }

            foreach (var info in list)
            {
                if (info.EventId == eventId && info.EventDispatcher is IEventDispatcher<A, B, C> dispatcher)
                {
                     await dispatcher.Handle(observer, a, b, c);
                    break;
                }
            }
            
            foreach (var info in list)
            {
                if (info.EventId == BattleEvent.AllEvent && info.EventDispatcher is IEventDispatcherMulParam mulParamDispatcher)
                {
                    await mulParamDispatcher.Handle(observer, eventId, a, b, c);
                    break;
                }
            }
        }
    }
}