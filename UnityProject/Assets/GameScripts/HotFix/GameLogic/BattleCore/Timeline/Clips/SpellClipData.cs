using System;
using NBC.ActionEditor;

namespace GameLogic.Battle
{
    public abstract class SpellClipData : Clip
    {
        [MenuName("触发类型")] [OptionParam(typeof(ClipTriggerType))]
        public int ClipTriggerType;
        
        [MenuName("触发事件")] [OptionParam(typeof(SpellClipEvent))]
        public int Event;

        public abstract Type ClipType();
    }
}