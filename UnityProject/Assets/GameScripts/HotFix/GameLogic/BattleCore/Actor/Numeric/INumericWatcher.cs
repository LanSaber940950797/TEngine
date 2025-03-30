using GameLogic.Battle;

namespace GameLogic.Battle
{
	public interface INumericWatcher
	{
		void Run(Actor actor, ActorNumbericChange args);
	}
}
