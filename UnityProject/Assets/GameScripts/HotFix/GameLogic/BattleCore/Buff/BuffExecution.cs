
using ET;

namespace GameLogic.Battle
{
    /// <summary>
    /// 状态能力执行体
    /// </summary>
    public  class BuffExecution : Entity, IAbilityExecute, IAwake
    {
        public Entity AbilityEntity => Buff;
        public Actor Owner => Buff.Owner;
        public IAbility Ability => Buff;
        public Buff Buff => GetParent<Buff>();
        
        public int TriggerTimes;
        public int CanTriggerTimes;

        //触发行为
        public IAction TriggerAction;
        public float? TriggerIntervalRemainDuration;
        public bool IsNeedRefresh;

        public int LastUpdateTime;
        
        public void EndExecute()
        {
            BuffExecutionSystem.EndExecute(this);
        }

    }
    
    
    [EntitySystemOf(typeof(BuffExecution))]
    public static partial class BuffExecutionSystem
    {
        [EntitySystem]
        public static void Awake(this BuffExecution self)
        {
          
            self.TriggerTimes = 0;
            self.CanTriggerTimes = 0;
            self.TriggerIntervalRemainDuration = null;
        }

        public static void BeginExecute(this BuffExecution self)
        {
            self.Buff.GetComponent<SkillTreeComponent>().Enable = true;
        }

        public static void EndExecute(BuffExecution self)
        {
            self.Buff.Dispose();
        }
        

        private static void DoTrigger(this BuffExecution self)
        {
            self.CanTriggerTimes++;
            //强行更新技能树
            self.Buff.GetComponent<SkillTreeComponent>().Update();
        }

        public static void OnFinishTrigger(this BuffExecution self)
        {
            self.TriggerTimes++;
        }

        public static bool IsCanTrigger(this BuffExecution self)
        {
            return self.CanTriggerTimes > self.TriggerTimes;
        }

        public static void OnChangeLayer(this BuffExecution self)
        {
            self.IsNeedRefresh = true;
        }
    }
}