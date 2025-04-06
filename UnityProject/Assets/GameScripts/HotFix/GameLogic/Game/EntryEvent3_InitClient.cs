using Cysharp.Threading.Tasks;
using ET;
using TEngine;

namespace GameLogic
{
    [Event(SceneType.Main)]
    public class EntryEvent3_InitClient : AEvent<Scene, EntryEvent3>
    {
        protected override async ETTask Run(Scene root, EntryEvent3 args)
        {
            // 根据配置修改掉Main Fiber的SceneType
            //SceneType sceneType = EnumHelper.FromString<SceneType>(globalComponent.GlobalConfig.AppType.ToString());
            root.SceneType = SceneType.Client;
            GameModule.ECS.Root = root;
            root.AddComponent<CurrentScenesComponent>();
            await EventSystem.Instance.PublishAsync(root, new AppStartInitFinish());
            await GameHelper.StartGame();
            //TopDownHelper.StartGame();
            //CardGameHelper.StartGame().Forget();
        }
    }
}