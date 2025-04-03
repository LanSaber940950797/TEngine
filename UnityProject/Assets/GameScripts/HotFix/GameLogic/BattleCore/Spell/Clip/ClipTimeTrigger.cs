using ET;

namespace GameLogic.Battle
{
    [ChildOf]
    public class ClipTimeTrigger : Entity, IAwake<SpellClipData>, IDestroy
    {
        public SpellClipData Data;
        public ETCancellationToken Token;
    }
    
    [EntitySystemOf(typeof(ClipTimeTrigger))]
    public static partial class ClipTimeTriggerSystem
    {
        [EntitySystem]
        public static void Awake(this ClipTimeTrigger self, SpellClipData data)
        {
            self.Data = data;
            self.WaitTrigger().NoContext();
            
        }

        private static async ETTask WaitTrigger(this ClipTimeTrigger self)
        {
            self.Token = new ETCancellationToken();
            long time = (long)(self.Data.Length * 1000);
            await self.Root().GetComponent<TimerComponent>().WaitAsync(time, self.Token);
            if (self.Token == null || self.Token.IsCancel())
            {
                return;
            }

            self.Token = null;
            self.Data.AttachClip(self.Parent);
            self.Dispose();
        }
        
        [EntitySystem]
        public static void Destroy(this ClipTimeTrigger self)
        {
            if (self.Token != null)
            {
                var token = self.Token;
                self.Token = null;
                token.Cancel();
            }
            self.Data = null;
        }
    }
}