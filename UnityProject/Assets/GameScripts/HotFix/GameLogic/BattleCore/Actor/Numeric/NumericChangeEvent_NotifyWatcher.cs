using ET;
namespace GameLogic.Battle
{
	// 分发数值监听
	[Event(SceneType.All)]  // 服务端Map需要分发, 客户端CurrentScene也要分发
	public class NumericChangeEvent_NotifyWatcher: AEvent<Scene, ActorNumbericChange>
	{
		protected override async ETTask Run(Scene scene, ActorNumbericChange args)
		{
			ActorNumericWatcherComponent.Instance.Run(args.Actor, args);
			await ETTask.CompletedTask;
		}
	}
}
