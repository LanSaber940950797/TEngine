using System.Collections.Generic;
using ET;
using MemoryPack;

namespace GameLogic.Battle
{
  
    public partial class SpellAction : Entity, IAwake,IDestroy, IAction
    {
        public Actor Creator { get; set; }
       
        public Actor Target { get; set; }



        public Spell Spell { get; set; }
        
        //public SkillExecute SkillExecute { get; set; }
       
        //public List<EntityRef<Actor>> SkillTargets { get; set; } = new List<EntityRef<Actor>>();
        

        public SpellCastParam SpellCastParam;
        public ETTask Tsc;

    }
    
    [EntitySystemOf(typeof(SpellAction))]
   
    public static partial class SpellActionSystem
    {

        [EntitySystem]
        public static void Destroy(this SpellAction self)
        {
            self.Creator = null;
            self.Target = null;
            
            self.Tsc = null;
        }
        
        
        private static void PreProcess(this SpellAction self)
        {
            //self.Creator.TriggerActionPoint(ActionPointType.PreSpell, self);
        }
        
        private static void SpellSkill(this SpellAction self)
        {
           
            // self.SkillExecute = self.SkillAbility.CreateExecution();
            // if (self.SkillExecute == null)
            // {
            //     self.FinishAction();
            //     return;
            // }
            // self.SkillExecute.ViewName = self.SkillAbility.ViewName;
            // if (self.SkillTargets.Count > 0)
            // {
            //     self.SkillExecute.SkillTargets.AddRange(self.SkillTargets);
            // }
            // self.SkillExecute.SpellCastParam = self.SpellCastParam;
            // self.SkillExecute.BeginExecute();
        }
        


        //后置处理
        private static void PostProcess(this SpellAction self)
        {
            //self.Creator.TriggerActionPoint(ActionPointType.PostSpell, self);
        }

        public static async ETTask DoAction(this SpellAction self)
        {
            self.Tsc = ETTask.Create();
            self.PreProcess();
            self.SpellSkill();
            await self.Tsc;
        }




       
    }
}