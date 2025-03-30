using System;

namespace ET
{
	public interface ILogicUpdate
	{
	}
	
	public interface ILogicUpdateSystem: ISystemType
	{
		void Run(Entity o);
	}

	[EntitySystem]
	public abstract class LogicUpdateSystem<T> : SystemObject, ILogicUpdateSystem where T: Entity, ILogicUpdate
	{
		void ILogicUpdateSystem.Run(Entity o)
		{
			this.LogicUpdate((T)o);
		}

		Type ISystemType.Type()
		{
			return typeof(T);
		}

		Type ISystemType.SystemType()
		{
			return typeof(ILogicUpdateSystem);
		}

		int ISystemType.GetInstanceQueueIndex()
		{
			return InstanceQueueIndex.LogicUpdate;
		}

		protected abstract void LogicUpdate(T self);
	}
}
