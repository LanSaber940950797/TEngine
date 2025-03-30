using System;
using ET;

namespace GameLogic.Battle
{
	[AttributeUsage(AttributeTargets.Class)]
	public class ActorNumericWatcherAttribute : BaseAttribute
	{
		public SceneType SceneType { get; }
		
		public int NumericType { get; }

		public ActorNumericWatcherAttribute(SceneType sceneType, int type)
		{
			this.SceneType = sceneType;
			this.NumericType = type;
		}
	}
}