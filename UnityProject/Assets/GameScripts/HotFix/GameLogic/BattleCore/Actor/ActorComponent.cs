using ET;

namespace GameLogic.Battle
{
    [ComponentOf]
    public class ActorComponent : Entity
    {
        /// <summary>
        /// 系统actor，有些全局效果或者acotr创建由系统actor执行
        /// </summary>
        private EntityRef<Actor> systemActor;
        public Actor SystemActor
        {
            get => systemActor;
            set => systemActor = value;
        }
    }
}