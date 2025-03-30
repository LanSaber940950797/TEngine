using System;

namespace ET
{
	
	public interface IOnEvent
	{
	}

	//事件参数
	public interface IEventArgs
	{
		
	}
	
	public interface IOnEventSystem<E>: ISystemType where E : IEventArgs
	{
		void Run(Entity o, E e);
	}

	[EntitySystem]
	public abstract class OnEventSystem<T, E> : SystemObject, IOnEventSystem<E> where T: Entity, IOnEvent where E : IEventArgs
	{
		void IOnEventSystem<E>.Run(Entity o, E e)
		{
			this.OnEvent((T)o, e);
		}

		Type ISystemType.SystemType()
		{
			return typeof(IOnEventSystem<E>);
		}

		int ISystemType.GetInstanceQueueIndex()
		{
			return InstanceQueueIndex.None;
		}

		Type ISystemType.Type()
		{
			return typeof(T);
		}

		protected abstract void OnEvent(T self, E e);
	}
}
