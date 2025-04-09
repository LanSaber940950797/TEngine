using System.Collections.Generic;
using ET;
using MemoryPack;

namespace GameLogic.Battle
{
  
    public partial class SpellAction : Entity, IAwake,IDestroy, IAction
    {
        public Actor Creator { get; set; }
       
        public Actor Target { get; set; }
        public IAction SourceAction { get; set; }


        public Skill Skill { get; set; }
        
        public SkillExecute SkillExecute { get; set; }
       
        //public List<EntityRef<Actor>> SkillTargets { get; set; } = new List<EntityRef<Actor>>();
        
        public SpellCastParam SpellCastParam;
    }
    
    [EntitySystemOf(typeof(SpellAction))]
   
    public static partial class SpellActionSystem
    {

        [EntitySystem]
        public static void Awake(this SpellAction self)
        {
            
        }

        [EntitySystem]
        public static void Destroy(this SpellAction self)
        {
           
            self.Creator = null;
            self.Target = null;
            self.Skill = null;
            
        }
        
        

        public static async ETTask DoAction(this SpellAction self)
        {
            await self.PreProcess();
            if(self.IsDisposed) return; //被打断了
            await self.DoActionInner();
            await self.PostProcess();
            //延时销毁，可能有些事件监听需要延时处理该行为，不能立即销毁
            self.AddChild<DestroyTimer, int, bool>(1000, true, true);
        }

        private static async ETTask PreProcess(this SpellAction self)
        {
            await self.Creator.SendEventAsync(ActionEvent.PreSpell, self);
            await ETTask.CompletedTask;
        }
        private static async ETTask PostProcess(this SpellAction self)
        {
            await self.Creator.SendEventAsync(ActionEvent.PostSpell, self);
            await ETTask.CompletedTask;
        }
        private static async ETTask DoActionInner(this SpellAction self)
        {
            self.SkillExecute = self.Skill.CreateExecution();
            if (self.SkillExecute == null)
            {
                return;
            }
            
            self.SkillExecute.BeginExecute();
            await self.SkillExecute.WaitEntityDestroy();
        }

       
    }
}