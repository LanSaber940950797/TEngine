using ET;

namespace GameLogic.Battle
{
    //施法参数
    public class SpellCastParam
    {
        public int SpellId;
        public int SpellLevel = 1;
    }
    
    [ComponentOf(typeof(Actor))]
    public class SpellComponent : Entity, IAwake
    {
        
    }
    
    [EntitySystemOf(typeof(SpellComponent))]
    public static partial class SpellComponentSystem
    {
        public static Spell AddSpell(this SpellComponent self, int spellId, int level)
        {
            var spell = self.AddChildWithId<Spell, int>(spellId, level);
            return spell;
        }

        public static void RemoveSpell(this SpellComponent self, int spellId)
        {
            self.RemoveChild(spellId);
        }

        public static async ETTask<bool> TrySpell(this SpellComponent self, SpellCastParam castParam, bool isLoad = true)
        {
            var spell = self.GetChild<Spell>(castParam.SpellId);
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
                action.Spell = spell;
                await action.DoAction();
                return true;
            }

            return false;

        }
    }
}