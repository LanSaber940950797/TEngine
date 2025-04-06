using Kurisu.AkiBT;

namespace GameLogic.Battle
{
    [AkiInfo("Conditional:是否可以施法")]
    [AkiLabel("BattleAI:是否可以施法")]
    [AkiGroup("BattleAI")]
    public class IsCanSpell : BattleAIConditional
    {
        protected ActorAIComponent aiComponent;
        protected override void OnAwake()
        {
            base.OnAwake();
        }
        protected override bool IsUpdatable()
        {
            return  actor.GetComponent<ActorAIComponent>().IsCanSpell();
        }
    }
}