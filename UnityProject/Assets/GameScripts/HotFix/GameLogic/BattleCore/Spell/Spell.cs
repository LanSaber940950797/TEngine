using ET;
using GameConfig;
namespace GameLogic.Battle
{
    [ChildOf(typeof(SpellComponent))]
    public class Spell : Entity, IAwake<int>
    {
        public Actor Owner => Parent.GetParent<Actor>();
        public int Level;
        public SpellDesc Desc;
    }
    
    [EntitySystemOf(typeof(Spell))]
    public static partial class SpellSystem
    {
        [EntitySystem]
        private static void Awake(this Spell self, int level)
        {
            self.Level = level;
            self.Desc = TbSpell.Instance.Get((int)self.Id);
        }
    }
}