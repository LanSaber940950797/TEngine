using System.Collections.Generic;
using ET;
using GameConfig.Battle;


namespace GameLogic.Battle
{
    public enum SkillExecutionState
    {
        None,
        Prepare, //施法准备
        Attack, //攻击
        Hit, //命中
        End, //结束
    }
    /// <summary>
    /// 技能执行体，执行体就是控制角色表现和技能表现的，包括角色动作、移动、变身等表现的，以及技能生成碰撞体等表现
    /// </summary>
    public  partial class SkillExecute : Entity, IAwake, ET.IUpdate, IAbilityExecute,IDestroy
    {
        public Actor Target ;

        public Skill Skill => GetParent<Skill>();
        public Entity AbilityEntity => Skill;
        public Actor Owner => Skill.Owner;
        public IAbility Ability => Skill;

    
        //目标列表
        public List<EntityRef<Actor>> SkillTargets = new List<EntityRef<Actor>>();
        
       
        public long OriginTime;
        public bool ActionOccupy = true;
        public SpellAction SpellAction;
        //public BattleTimer Timer;
       
      
        public void EndExecute()
        {
            SkillExecuteSystem.EndExecute(this);
        }
      
    }
    
    [EntitySystemOf(typeof(SkillExecute))]
    public static partial class SkillExecuteSystem
    {
        [EntitySystem]
        public static void Awake(this SkillExecute self)
        {
            self.SkillTargets = ObjectPool.Instance.Fetch<List<EntityRef<Actor>>>();
        }

        [EntitySystem]
        public static void Destroy(this SkillExecute self)
        {
            self.SkillTargets.Clear();
            ObjectPool.Instance.Recycle(self.SkillTargets);
            self.SkillTargets = null;
            self.SpellAction = null;
        }
        

        public static void BeginExecute(this SkillExecute self)
        {
            self.Skill.GetComponent<SkillTreeComponent>().Enable = true;
        }

        public static void EndExecute(SkillExecute self)
        {
            //self.Skill.Spelling = false;
            self.Skill.GetComponent<SkillTreeComponent>().Enable = false;
            self.Dispose();
        }
    }
   
}