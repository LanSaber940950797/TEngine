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
        public ETCancellationToken Cancel;
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
            if (self.Cancel != null)
            {
                self.Cancel.Cancel();
                self.Cancel = null;
            }
            self.Creator = null;
            self.Target = null;
            self.Spell = null;
            
        }
        
        

        public static async ETTask DoAction(this SpellAction self)
        {
            await self.PreDoAction();
            await self.DoActionInner();
            await self.PostDoAction();
        }

        private static async ETTask PreDoAction(this SpellAction self)
        {
            await ETTask.CompletedTask;
        }
        private static async ETTask PostDoAction(this SpellAction self)
        {
            await ETTask.CompletedTask;
        }
        private static async ETTask DoActionInner(this SpellAction self)
        {
            var clips = self.Spell.GetComponent<SpellClipComponent>().GetClips();
            foreach (var clip in clips)
            {
                if (clip.ClipTriggerType == ClipTriggerType.Time)
                {
                    self.AddChild<ClipTimeTrigger, SpellClipData>(clip, true);
                }
                else if(clip.ClipTriggerType == ClipTriggerType.ClipEvent)
                {
                    self.AddChild<ClipEventTrigger, SpellClipData>(clip, true);
                }
            }

            var time = self.Spell.GetComponent<SpellClipComponent>().TotalTime();
            self.Cancel = new ETCancellationToken();
            await self.Root().GetComponent<TimerComponent>().WaitAsync(time, self.Cancel);
            self.Cancel = null;
            await ETTask.CompletedTask;
        }

       
    }
}