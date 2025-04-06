using Kurisu.AkiBT;

namespace GameLogic.Battle
{
    [AkiInfo("Conditional:是否有目标")]
    [AkiLabel("BattleAI:是否有目标")]
    [AkiGroup("BattleAI")]
    public class IsHasTarget : BattleAIConditional
    {
        protected ActorAIComponent actorAIComponent;
        
        protected override void OnAwake()
        {
            base.OnAwake();
            actorAIComponent = actor.GetComponent<ActorAIComponent>();
        }
        protected override bool IsUpdatable()
        {
            return actorAIComponent.Target != null;
        }
    }
   
}