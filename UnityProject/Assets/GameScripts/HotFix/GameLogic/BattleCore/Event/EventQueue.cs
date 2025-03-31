using System;
using System.Linq;
using ET;

namespace GameLogic.Battle
{
    [ChildOf]
    public class EventQueue : Entity, IAwake
    {
        
    }

    [EntitySystemOf(typeof(EventQueue))]
    public static partial class EventQueueSystem
    {
        [EntitySystem]
        public static void Awake(this EventQueue self)
        {
            
        }
        public static void New(this EventQueue self, Entity owner)
        {
            self.AddChildWithId<Observer, Entity>(owner.InstanceId, owner);
        }

        public static void Remove(this EventQueue self, Entity owner)
        {
            self.RemoveChild(owner.InstanceId);
        }
        
        public static void Send(this EventQueue self)
        {
            var list = self.Children.Keys.ToList();
            foreach (var id in list)
            {
                var observer = self.GetChild<Observer>(id);
                Entity owner = observer.Owner;
                if (owner == null)
                {
                    self.RemoveChild(id);
                    continue;
                }
                EventDispatcherComponent.Instance.Run(owner, (int)self.Id).NoContext();
            }
        }
        
        public static void Send<TArg>(this EventQueue self, TArg arg)
        {
            var list = self.Children.Keys.ToList();
            foreach (var id in list)
            {
                var observer = self.GetChild<Observer>(id);
                Entity owner = observer.Owner;
                if (owner == null)
                {
                    self.RemoveChild(id);
                    continue;
                }
                EventDispatcherComponent.Instance.Run(owner, (int)self.Id, arg).NoContext();
            }
        }
        
        public static void Send<TArg1, TArg2>(this EventQueue self, TArg1 arg1, TArg2 arg2)
        {
            var list = self.Children.Keys.ToList();
            foreach (var id in list)
            {
                var observer = self.GetChild<Observer>(id);
                Entity owner = observer.Owner;
                if (owner == null)
                {
                    self.RemoveChild(id);
                    continue;
                }
                EventDispatcherComponent.Instance.Run(owner, (int)self.Id, arg1, arg2).NoContext();
            }
        }
        
        public static void Send<TArg1, TArg2, TArg3>(this EventQueue self, TArg1 arg1, TArg2 arg2, TArg3 arg3)
        {
            var list = self.Children.Keys.ToList();
            foreach (var id in list)
            {
                var observer = self.GetChild<Observer>(id);
                Entity owner = observer.Owner;
                if (owner == null)
                {
                    self.RemoveChild(id);
                    continue;
                }
                EventDispatcherComponent.Instance.Run(owner, (int)self.Id, arg1, arg2, arg3).NoContext();
            }
        }
        
        public static async ETTask SendAsync(this EventQueue self)
        {
            using ListComponent<ETTask> handles = ListComponent<ETTask>.Create();
            var list = self.Children.Keys.ToList();
            foreach (var id in list)
            {
                var observer = self.GetChild<Observer>(id);
                Entity owner = observer.Owner;
                if (owner == null)
                {
                    self.RemoveChild(id);
                    continue;
                }
                handles.Add(EventDispatcherComponent.Instance.Run(owner, (int)self.Id));
            }
            try
            {
                await ETTaskHelper.WaitAll(handles);
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }
        
        public static async ETTask SendAsync<TArg1>(this EventQueue self, TArg1 arg1)
        {
            using ListComponent<ETTask> handles = ListComponent<ETTask>.Create();
            var list = self.Children.Keys.ToList();
            foreach (var id in list)
            {
                var observer = self.GetChild<Observer>(id);
                Entity owner = observer.Owner;
                if (owner == null)
                {
                    self.RemoveChild(id);
                    continue;
                }
                handles.Add(EventDispatcherComponent.Instance.Run(owner, (int)self.Id, arg1));
            }
            try
            {
                await ETTaskHelper.WaitAll(handles);
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }
        public static async ETTask SendAsync<TArg1,TArg2>(this EventQueue self, TArg1 arg1, TArg2 arg2)
        {
            using ListComponent<ETTask> handles = ListComponent<ETTask>.Create();
            var list = self.Children.Keys.ToList();
            foreach (var id in list)
            {
                var observer = self.GetChild<Observer>(id);
                Entity owner = observer.Owner;
                if (owner == null)
                {
                    self.RemoveChild(id);
                    continue;
                }
                handles.Add(EventDispatcherComponent.Instance.Run(owner, (int)self.Id, arg1, arg2));
            }
            try
            {
                await ETTaskHelper.WaitAll(handles);
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }
        
        public static async ETTask SendAsync<TArg1,TArg2, TArg3>(this EventQueue self, TArg1 arg1, TArg2 arg2, TArg3 arg3)
        {
            using ListComponent<ETTask> handles = ListComponent<ETTask>.Create();
            var list = self.Children.Keys.ToList();
            foreach (var id in list)
            {
                var observer = self.GetChild<Observer>(id);
                Entity owner = observer.Owner;
                if (owner == null)
                {
                    self.RemoveChild(id);
                    continue;
                }
                handles.Add(EventDispatcherComponent.Instance.Run(owner, (int)self.Id, arg1, arg2, arg3));
            }
            try
            {
                await ETTaskHelper.WaitAll(handles);
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }
        
    }
}