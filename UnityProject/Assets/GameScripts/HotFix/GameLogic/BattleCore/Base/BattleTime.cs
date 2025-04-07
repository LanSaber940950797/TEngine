using System;
using ET;

namespace GameLogic.Battle
{
    [ComponentOf(typeof(Scene))]
    public class BattleTime : Entity,IAwake,IDestroy
    {
        public int Frame;
        public bool IsPause;
        public long Timer;
    }
    
    [EntitySystemOf(typeof(BattleTime))]
    public static partial class BattleTimeSystem
    {
        [Invoke(TimerInvokeType.BattleFrameTime)]
        public class BattleFrameFixTimer: ATimer<BattleTime>
        {
            protected override void Run(BattleTime self)
            {
                try
                {
                    self.FixUpdate();
                }
                catch (Exception e)
                {
                    Log.Error($"session idle checker timer error: {self.Id}\n{e}");
                }
            }
        }
        
        [EntitySystem]
        private static void Awake(this BattleTime self)
        {
            //直接用定时器吧，最好的做法是自己控制更新
            self.Frame = 0;
            self.Timer = self.Root().GetComponent<BattleTimerComponent>()
                .NewRepeatedTimer(BattleConstValue.FrameInterval, TimerInvokeType.BattleFrameTime, self);
        }

        [EntitySystem]
        private static void Destroy(this BattleTime self)
        {
            self.Root().GetComponent<BattleTimerComponent>().Remove(ref self.Timer);
        }
        private static void FixUpdate(this BattleTime self)
        {
            if (self.IsPause)
            {
                return;
            }

            self.Frame++;
            self.Scene().GetComponent<BattleTimerComponent>().FixUpdate();
        }

        public static long Now(this BattleTime self)
        {
            return self.Frame * BattleConstValue.FrameInterval;
        }
    }
}