using System.Collections.Generic;
using ET;

namespace GameLogic.Battle
{
    

    /// <summary>
    /// 赋给效果行动
    /// </summary>
    public class EffectAssignAction : Entity
        , IAwake, IAction,IDestroy
    {
        
        /// 行动实体
        public Actor Creator { get; set; }
        /// 目标对象
        public Actor Target { get; set; }

        public IAction SourceAction { get; set; }

        public AbilityEffect AbilityEffect;
        public Actor AssignTarget;
        public List<Actor> Targets;


    }

    
    [EntitySystemOf(typeof(EffectAssignAction))]
    public static partial class EffectAssignActionSystem
    {
        [EntitySystem]
        public static void Destroy(this EffectAssignAction self)
        {
            self.Creator = null;
            self.Target = null;
            self.AbilityEffect = null;
            self.AssignTarget = null;
            self.Target = null;
        }
        
        



 
        
        public static async ETTask DoAction(this EffectAssignAction self)
        {
            await self.PreProcess();
            await self.DoActionInner();
            await self.PostProcess();
            self.Dispose();
        }

        private static async ETTask PreProcess(this EffectAssignAction self)
        {
            //
            await ETTask.CompletedTask;
        }
        private static async ETTask PostProcess(this EffectAssignAction self)
        {
            await ETTask.CompletedTask;
        }
        private static async ETTask DoActionInner(this EffectAssignAction self)
        {
            await self.Target.SendEventAsync(BattleEvent.DoEffect, self);
            await ETTask.CompletedTask;
        }

    }
}