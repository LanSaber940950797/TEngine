using Kurisu.AkiBT;
using UnityEngine;

namespace GameLogic.Battle
{
    [AkiInfo("Action:获取AI对象")]
    [AkiLabel("BattleAI:获取AI对象")]
    [AkiGroup("BattleAI")]
    public class ActionGetAIActor : BattleAIAction
    {
        [AkiLabel("获取AIActor")]
        [SerializeField]
        public SharedSTObject<Actor> outActor;
        [AkiLabel("0 ower, 1 target")]
        [SerializeField]
        protected int type;
        
        protected ActorAIComponent aiComponent;
        public override void Awake()
        {
            base.Awake();
            InitVariable(outActor);
            aiComponent = actor.GetComponent<ActorAIComponent>();
        }
        
        protected override Status OnUpdate()
        {
            if (type == 1)
            {
                outActor.Value = aiComponent.Target;
            }
            else
            {
                outActor.Value = actor;
            }
            return Status.Success;
        }
    }
}