using ET;

namespace GameLogic.Battle
{
    //行动是战斗的最小变化单位


    [ComponentOf(typeof(Actor))]
    public class ActionComponent : Entity, IAwake
    {

    }

    [EntitySystemOf((typeof(ActionComponent)))]
    public static partial class ActionComponentSystem
    {

        public static void Awake(this ActionComponent self)
        {

        }

        public static bool TryMakeAction<T>(this ActionComponent self, out T action, IAction source = null) where T : Entity, IAction, IAwake
        {
            //todo 这里应该做些检查是否能创建，禁用等
            action = self.AddChild<T>(true);
            action.Creator = self.GetParent<Actor>();
            action.SourceAction = source;
            return true;
        }
        
        public static bool TryMakeAction<T>(this Actor self, out T action, IAction source = null) where T : Entity, IAction, IAwake
        {
            return self.GetComponent<ActionComponent>().TryMakeAction(out action);
        }
    }
}