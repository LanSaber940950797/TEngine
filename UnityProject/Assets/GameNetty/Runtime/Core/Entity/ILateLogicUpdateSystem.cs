using System;

namespace ET
{
	public interface ILateLogicUpdate
	{
	}
	
	public interface ILateLogicUpdateSystem: ISystemType
	{
		void Run(Entity o);
	}

	[EntitySystem]
	public abstract class LateLogicUpdateSystem<T> : SystemObject, ILateLogicUpdateSystem where T: Entity, ILateLogicUpdate
	{
		void ILateLogicUpdateSystem.Run(Entity o)
		{
			this.LateLogicUpdate((T)o);
		}

		Type ISystemType.Type()
		{
			return typeof(T);
		}

		Type ISystemType.SystemType()
		{
			return typeof(ILateLogicUpdateSystem);
		}

		int ISystemType.GetInstanceQueueIndex()
		{
			return InstanceQueueIndex.LateLogicUpdate;
		}

		protected abstract void LateLogicUpdate(T self);
	}
}
