using System.Collections.Generic;
using ET;
using UnityEngine;

namespace GameLogic.Battle
{
    public class ActorCreateInfo
    {
        public ActorType ActorType;
        public SideType SideType;
        public int DescId;
        public Vector3 Position;
        public Vector3 Forward;
    }
    
    [ComponentOf]
    public class ActorComponent : Entity, IAwake, IDestroy
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
    
    [EntitySystemOf(typeof(ActorComponent))]
    public static partial class ActorComponentSystem
    {
        [EntitySystem]
        private static void Awake(this ActorComponent self)
        {
            self.CreateSystemActor();
        }

        [EntitySystem]
        private static void Destroy(this ActorComponent self)
        {
            self.SystemActor = null;
        }
        
        private static void CreateSystemActor(this ActorComponent self)
        {
            self.SystemActor = self.AddChild<Actor, ActorType, SideType>(ActorType.System, SideType.Neutral);
        }
        
       
        public static List<EntityRef<Actor>> GetActors(this ActorComponent self, TargetSideType targetSide, Actor filter = null, ActorType actorType = ActorType.CanSelect)
        {
            var list = new List<EntityRef<Actor>>();
            if (self.ChildrenCount() == 0)
            {
                return list;
            }
            foreach (Actor actor in self.Children.Values)
            {
                if (actor == filter)
                {
                    continue;
                }
                
                if (!actor.IsActorType(actorType))
                {
                    continue;
                }
                
                list.Add(actor);
            }

            return list;
        }
    }
}