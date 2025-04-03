using System.Collections.Generic;
using ET;

using TEngine;
using UnityEngine;

namespace GameLogic.Battle
{
    [ComponentOf(typeof(Spell))]
    public class SpellClipComponent : Entity, IAwake<string>
    {
        public SpellAsset Asset;
        public List<SpellClipData> ClipDatas = new List<SpellClipData>();

        public SpellClipComponent(List<SpellClipData> clipDatas)
        {
            ClipDatas = clipDatas;
        }
    }
    
    [EntitySystemOf(typeof(SpellClipComponent))]
    public static partial class SpellClipComponentSystem
    {
        [EntitySystem]
        public static void Awake(this SpellClipComponent self, string assetName)
        {
            self.ClipDatas.Clear();
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
                            self.ClipDatas.Add(spellClipData);
                        }
                    }
                }
            }
        }


        public static List<SpellClipData> GetClips(this SpellClipComponent self)
        {
            return self.ClipDatas;
        }

        public static long TotalTime(this SpellClipComponent self)
        {
            long time = (long)(self.Asset.Length * 1000);
            return time;
        }
        
    }
}