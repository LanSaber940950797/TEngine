using System;
using ET;

namespace GameLogic.Battle
{
    [ComponentOf]
    public class DestroyTimer : Entity,IAwake<int, bool>,IDestroy
    {
        public long Timer;
        public long LastTime;
        public bool IsDestroyParent;
    }
    
    
    
    [EntitySystemOf(typeof(DestroyTimer))]
    public static partial class DestroyTimerSystem
    {
        [Invoke(TimerInvokeType.DestroyTimer)]
        public class DestroyTimerHandle: ATimer<DestroyTimer>
        {
            protected override void Run(DestroyTimer self)
            {
                if (self.IsDisposed)
                {
                    return;
                }
                self.Timer = 0;
                self.LastTime = 0;
                if (self.IsDestroyParent)
                {
                    self.Parent.Dispose();
                }
                else
                {
                    self.Dispose();
                }
            }
        }
        
        [EntitySystem]
        private static void Awake(this DestroyTimer self, int duration, bool isParent)
        {
            self.ReSet(duration);
        }

        public static void AddDuration(this DestroyTimer self, int duration)
        {
            if (self.Timer == 0)
            {
                self.ReSet(duration);
                return;
            }

            var sub = TimeInfo.Instance.ServerFrameTime() - self.LastTime;
            if (sub < 0)
            {
                self.ReSet(duration);
            }
            self.ReSet(duration + (int)sub);
        }

        public static void ReSet(this DestroyTimer self, int duration)
        {
            if (self.Timer != 0)
            {
                self.Scene().GetComponent<BattleTimerComponent>()?.Remove(ref self.Timer);
            }

            self.LastTime = BattleHelper.FrameTime() + duration;
            self.Timer = self.Scene().GetComponent<BattleTimerComponent>()
                .NewOnceTimer(self.LastTime, TimerInvokeType.DestroyTimer, self);
        }

        [EntitySystem]
        private static void Destroy(this DestroyTimer self)
        {
            if (self.Timer != 0)
            {
                self.Scene().GetComponent<BattleTimerComponent>()?.Remove(ref self.Timer);
            }
        }
    }
}