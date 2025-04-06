using ET;
using TEngine;
using UnityEngine;
using Log = ET.Log;

namespace GameLogic.Battle
{
    [EntitySystemOf(typeof(ActorView))]
    public static partial class ActorViewSystem
    {

        
        [EntitySystem]
        public static void Awake(this ActorView self, Actor actor)
        {
            actor.ActorView = self;
            self.Actor = actor;
            self.DeathTime = 0.5f;
            self.AddComponent<ViewComponent, Entity, string>(self.Actor, null);
            self.SendActorCreateViewEvent().NoContext();
        }

        private static async ETTask SendActorCreateViewEvent(this ActorView self)
        {
            //下一帧再发出事件，让逻辑层先加载
            await self.Root().GetComponent<TimerComponent>().WaitFrameAsync();
            self.Scene().SendEvent(BattleEvent.ActorCreateView, self);
        }

       

        [EntitySystem]
        public static void LateUpdate(this ActorView self)
        {
            if (self.Actor == null || self.Actor.IsDisposed)
            {
                self.DeathTime -= GameTime.deltaTime;
                if (self.DeathTime < 0)
                {
                    self.Dispose();
                }
                return;
            }
        }
    }
}