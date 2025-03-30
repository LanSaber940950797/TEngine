using System;
using ET;

namespace GameLogic.Battle
{
    public interface IEventDispatcherBase
    {
        Type ObserverType { get; }
    }
    
    public interface IEventDispatcher
    {
        ETTask Handle(Entity o);
    }
    
    public interface IEventDispatcher<A>
    {
        ETTask Handle(Entity o, A a);
    }
    
    public interface IEventDispatcher<A, B>
    {
        ETTask Handle(Entity o, A a, B b);
    }
    
    public interface IEventDispatcher<A, B, C>
    {
        ETTask Handle(Entity o, A a, B b, C c);
    }
    
    public abstract class      AEventDispatcher<E> : IEventDispatcher,IEventDispatcherBase  where E : Entity 
    {
        Type IEventDispatcherBase.ObserverType
        {
            get => typeof(E);
        }
        
        protected abstract ETTask Run(E observer);

        async ETTask IEventDispatcher.Handle(Entity observer)
        {
            try
            {
                await Run((E)observer);
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }
    }
    
    public abstract class AEventDispatcher<E, A> : IEventDispatcher<A>,IEventDispatcherBase where E : Entity 
    {

        Type IEventDispatcherBase.ObserverType
        {
            get => typeof(E);
        }
        protected abstract ETTask Run(E observer, A a);

        async ETTask IEventDispatcher<A>.Handle(Entity observer, A a)
        {
            try
            {
                await Run((E)observer, a);
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }
    }
    
    public abstract class AEventDispatcher<E, A, B> : IEventDispatcher<A, B>,IEventDispatcherBase where E : Entity 
    {
        Type IEventDispatcherBase.ObserverType
        {
            get => typeof(E);
        }

        protected abstract ETTask Run(E observer, A a, B b);

        async ETTask IEventDispatcher<A, B>.Handle(Entity observer, A a, B b)
        {
            try
            {
                await Run((E)observer, a, b);
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }
    }
    
    public abstract class AEventDispatcher<E, A, B, C> : IEventDispatcher<A, B, C>,IEventDispatcherBase where E : Entity 
    {
        Type IEventDispatcherBase.ObserverType
        {
            get => typeof(E);
        }
        protected abstract ETTask Run(E observer, A a, B b, C c);

        public async ETTask Handle(Entity observer, A a, B b, C c)
        {
            try
            {
                await Run((E)observer, a, b, c);
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }
    }
}