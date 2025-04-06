using System;
using ET;
using Kurisu.AkiBT;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameLogic.Battle
{
    [AkiInfo("Action:获取触发行动action")]
    [AkiLabel("Skill:GetTriggerActionTarget")]
    [AkiGroup("Skill")]
    public class GetTriggerActionTarget : SkillAction
    {
        

        [SerializeField, Tooltip("目标类型")]
        public EffectiveActorType effectiveActorType;

        [SerializeField, Tooltip("目标")] 
        public SharedSTObject<Actor> actorShared;
        
      


        protected override Status OnUpdate()
        {
            GetEffectActor();
            return Status.Success;
        }
        
        protected void GetEffectActor()
        {
            BuffExecution execute = skillTree.Execute as  BuffExecution;
            if (execute == null)
            {
                Log.Error($"GetTriggerActionTarget is not BuffExecution");
                return;
            }
            IAction action = execute.TriggerAction;
            Actor actor = null;
            if (effectiveActorType == EffectiveActorType.Creator)
            {
                actor = action.Creator;
            }
            else if(effectiveActorType == EffectiveActorType.Target)
            {
                actor = action.Target;
            }
            
            if (actor == null)
            {
                return;
            }
            
            actorShared.Value = actor;
        }
    }
}