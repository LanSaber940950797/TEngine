using ET;

namespace GameLogic.Battle
{
    [ChildOf]
    public class Observer : Entity, IAwake<Entity>,IDestroy
    {
        private EntityRef<Entity> owner;
        public Entity Owner
        {
            get => owner;
            set => owner = value;
        }
    }

    [EntitySystemOf(typeof(Observer))]
    public static partial class ObserverSystem
    {

        [EntitySystem]
        public static void Awake(this Observer self, Entity owner)
        {
            self.Owner = owner;
        }

        [EntitySystem]
        public static void Destroy(this Observer self)
        {
            self.Owner = null;
        }
        
    }
}