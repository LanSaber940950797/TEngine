using Kurisu.AkiBT;

namespace GameLogic.Battle
{
    [AkiInfo("Action:尝试施法")]
    [AkiLabel("BattleAI:尝试施法")]
    [AkiGroup("BattleAI")]
    public class ActionTrySpell : BattleAIAction
    {
        protected ActorAIComponent aiComponent;
        public override void Awake()
        {
            base.Awake();
            aiComponent = actor.GetComponent<ActorAIComponent>();
        }
        
        protected override Status OnUpdate()
        {
            if (aiComponent.TrySpell())
            {
                return Status.Success;
            }

            return Status.Failure;
        }
        
    }
   
}