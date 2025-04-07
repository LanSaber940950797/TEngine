using System;
using ET;

namespace GameLogic.Battle
{
    [ComponentOf]
    public class BuffDurationTimer : Entity,IAwake<int>,IDestroy
    {
        public long Timer;
        public long LastTime;
    }
    
    
    
    [EntitySystemOf(typeof(BuffDurationTimer))]
    public static partial class DestroyTimerSystem
    {
        [Invoke(TimerInvokeType.BuffDurationTimer)]
        public class BuffDurationTimerHandle: ATimer<BuffDurationTimer>
        {
            protected override void Run(BuffDurationTimer self)
            {
                self.Timer = 0;
                self.LastTime = 0;
                self.GetParent<Buff>().OnDurationTimer();
            }
        }
        
        [EntitySystem]
        private static void Awake(this BuffDurationTimer self, int duration)
        {
            self.ReSet(duration);
        }

        public static void AddDuration(this BuffDurationTimer self, int duration)
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

        public static void ReSet(this BuffDurationTimer self, int duration)
        {
            if (self.Timer != 0)
            {
                self.Scene().GetComponent<BattleTimerComponent>()?.Remove(ref self.Timer);
            }

            self.LastTime = BattleHelper.FrameTime() + duration;
            self.Timer = self.Scene().GetComponent<BattleTimerComponent>()
                .NewOnceTimer(self.LastTime, TimerInvokeType.BuffDurationTimer, self);
        }

        [EntitySystem]
        private static void Destroy(this BuffDurationTimer self)
        {
            if (self.Timer != 0)
            {
                self.Scene().GetComponent<BattleTimerComponent>()?.Remove(ref self.Timer);
            }
        }
    }
}