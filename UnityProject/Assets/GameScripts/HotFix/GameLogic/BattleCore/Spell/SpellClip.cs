using ET;


namespace GameLogic.Battle
{
    [ChildOf(typeof(SpellClipComponent))]
    public class SpellClip : Entity, IAwake<SpellClipData>
    {
        public SpellClipData Clip;
    }
    
    [EntitySystemOf(typeof(SpellClip))]
    public static partial class SpellClipSystem
    {
        public static void Awake(this SpellClip self, SpellClipData clip)
        {
            self.Clip = clip;
        }

        public static async ETTask Start(this SpellClip self)
        {
            if (self.Clip.ClipTriggerType == ClipTriggerType.ClipEvent)
            {
                return;
            }

           
            
        }

        private static int ToFrameTime(float time)
        {
            return (int)(time * 1000);
        }
    }
    
    
}