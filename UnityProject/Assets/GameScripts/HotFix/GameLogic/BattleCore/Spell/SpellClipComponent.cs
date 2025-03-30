using ET;
using NativeCollection.UnsafeType;
using TEngine;
using UnityEngine;

namespace GameLogic.Battle
{
    [ComponentOf(typeof(Spell))]
    public class SpellClipComponent : Entity, IAwake<string>
    {
        public SpellAsset Asset;
        public long StartTime;
        public ETTask Tsc;
        public ETCancellationToken CancelToken;
        public readonly NativeCollection.MultiMap<long, long> timeId = new(100);
    }
    
    [EntitySystemOf(typeof(SpellClipComponent))]
    public static partial class SpellClipComponentSystem
    {
        public static void Awake(this SpellClipComponent self, string assetName)
        {
            var asset = GameModule.Resource.LoadAsset<TextAsset>(assetName);
            self.Asset = Utility.Json.ToObject<SpellAsset>(asset.text);
            self.Init();
            
        }

        private static void Init(this SpellClipComponent self)
        {
            foreach (var group in self.Asset.groups)
            {
                if (!group.IsActive)
                {
                    continue;
                }
                foreach (var track in group.Tracks)
                {
                    if (!track.IsActive)
                    {
                        continue;
                    }
                    foreach (var clip in track.Clips)
                    {
                        if (!clip.IsActive)
                        {
                            continue;
                        }

                        if (clip is SpellClipData spellClipData)
                        {
                            self.AddChild<SpellClip, SpellClipData>(spellClipData);
                        }
                    }
                }
            }
        }


        public static async ETTask StartSpell(this SpellClipComponent self)
        {
            self.FinishSpell();
            self.StartTime = TimeInfo.Instance.ClientFrameTime();
            using ListComponent<ETTask> list = ListComponent<ETTask>.Create();
            foreach (SpellClip clip in self.Children.Values)
            {
                list.Add(clip.Start());
            }

            self.CancelToken = new ETCancellationToken();
            self.Tsc = ETTaskHelper.WaitAll(list).AddCancel(self.CancelToken);
            await self.Tsc;
            self.FinishSpell();
        }

        private static void FinishSpell(this SpellClipComponent self)
        {
            self.StartTime = 0;
            self.timeId.Clear();
            if (self.Tsc != null)
            {
                ETTask tsc = self.Tsc;
                self.Tsc = null;
                
                tsc.SetResult();
            }
        }
        
    }
}