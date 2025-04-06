using ET;
using GameConfig;
namespace GameLogic.Battle
{
    [ChildOf(typeof(SkillComponent))]
    public class Skill : Entity, IAwake<int>,IAbility
    {
        public Actor Owner => Parent.GetParent<Actor>();
        public Actor Creator
        {
            get => Parent.GetParent<Actor>();
            set => throw new System.NotImplementedException();
        }

        public int Level;
        public SpellDesc Desc;
    }
    
    [EntitySystemOf(typeof(Skill))]
    public static partial class SkillSystem
    {
        [EntitySystem]
        private static void Awake(this Skill self, int level)
        {
            self.Level = level;
            self.Desc = TbSpell.Instance.Get((int)self.Id);
            self.ViewName = self.Desc.Name;
        }

        public static bool IsRunning(this Skill self)
        {
            return self.GetComponent<SkillExecute>() != null;
        }
        public static SkillExecute CreateExecution(this Skill self)
        {
            if (!self.GetComponent<SkillTreeComponent>().IsReady())
            {
                return null;
            }

            if (self.IsRunning())
            {
                Log.Error($"SkillSystem.CreateExecution IsRunning actor {self.Owner.Id} skill {self.Id}");
                return null;
            }
            var execute = self.AddComponent<SkillExecute>(true);
            
            self.GetComponent<SkillTreeComponent>().Init(self, execute);
            return execute;
        }

    }
}