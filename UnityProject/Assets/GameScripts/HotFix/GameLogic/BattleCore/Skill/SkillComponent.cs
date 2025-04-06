using System;
using ET;

namespace GameLogic.Battle
{
        public class SpellCastParam : IDisposable, IPool//施法参数
        {
            public int SpellId;
            public int SpellLevel = 1;
            public bool IsFromPool { get; set; }
           

            public static SpellCastParam Create()
            {
                return ObjectPool.Instance.Fetch<SpellCastParam>();
            }
            public void Dispose()
            {
                ObjectPool.Instance.Recycle(this);
            }

            
        }
    
    [ComponentOf(typeof(Actor))]
    public class SkillComponent : Entity, IAwake
    {
        
    }
    
    [EntitySystemOf(typeof(SkillComponent))]
    public static partial class SkillComponentSystem
    {
        public static Skill AddSpell(this SkillComponent self, int spellId, int level)
        {
            var spell = self.AddChildWithId<Skill, int>(spellId, level);
            return spell;
        }

        public static void RemoveSpell(this SkillComponent self, int spellId)
        {
            self.RemoveChild(spellId);
        }

        public static bool TrySpell(this SkillComponent self, SpellCastParam castParam, bool isLoad = true)
        {
            var spell = self.GetChild<Skill>(castParam.SpellId);
            if (spell == null && isLoad)
            {
                spell = self.AddSpell(castParam.SpellId, castParam.SpellLevel);
            }

            if (spell == null)
            {
                return false;
            }

            if (self.GetParent<Actor>().TryMakeAction<SpellAction>(out var action))
            {
                action.SpellCastParam = castParam;
                action.Skill = spell;
                action.DoAction().NoContext();
                return true;
            }

            return false;

        }
    }
}