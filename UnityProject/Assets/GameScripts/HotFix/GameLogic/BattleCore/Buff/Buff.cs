using System;
using ET;
using GameConfig;

namespace GameLogic.Battle
{
    [ChildOf(typeof(BuffComponent))]
    public class Buff : Entity, IAwake<int>, IAbility,IDestroy
    {
        public Actor Owner { get => Parent.GetParent<Actor>(); }
        public Actor Creator { get; set; }
        public BuffDesc Desc;
        public int Layer; //buff层数
    }
    
    [EntitySystemOf(typeof(Buff))]
    public static partial class BuffSystem
    {
        [EntitySystem]
        private static void Awake(this Buff self, int id)
        {
            self.Desc = TbBuff.Instance.Get(id);
           
        }

        [EntitySystem]
        private static void Destroy(this Buff self)
        {
            self.Desc = null;
            self.Creator = null;
        }

        public static async ETTask Activate(this Buff self)
        {
            var tree = self.AddComponent<SkillTreeComponent, string>(null, true);
            tree.TreeName = self.Desc.TreeName;
            await tree.LoadSkillTree();
            var execution = self.AddComponent<BuffExecution>(true);
            execution.BeginExecute();
            if (self.Desc.Duration > 0)
            {
                self.AddComponent<BuffDurationTimer,int>(self.Desc.Duration, true);
            }
        }

        public static void OnDurationTimer(this Buff self)
        {
            var delLayer = self.Desc.DecreaseLayer > 0 ? self.Desc.DecreaseLayer : self.Layer;
            self.OnChangeLayer(delLayer);
            if (!self.IsDisposed)
            {
                self.GetComponent<BuffDurationTimer>().ReSet(self.Desc.Duration);
            }
        }

        public static void OnChangeLayer(this Buff self, int layer)
        {
            self.Layer += layer;
            if (self.Layer < 0)
            {
                self.OnRemove();
                return;
            }
            if (self.Desc.MaxLayer > 0 && self.Layer > self.Desc.MaxLayer)
            {
                self.Layer = self.Desc.MaxLayer;
            }
        }

        public static void RefreshBuff(this Buff self, int layer)
        {
            if ((self.Desc.RefreshMode & EBuffRefreshMode.AddLayer) > 0)
            {
                self.OnChangeLayer(layer);
            }

            if ((self.Desc.RefreshMode & EBuffRefreshMode.AddDuration) > 0)
            {
                self.GetComponent<BuffDurationTimer>()?.AddDuration(self.Desc.Duration);
            }

            if ((self.Desc.RefreshMode & EBuffRefreshMode.RefreshDuration) > 0)
            {
                self.GetComponent<BuffDurationTimer>()?.ReSet(self.Desc.Duration);
            } 
        }

        private static void OnRemove(this Buff self)
        {
            self.Dispose();
        }
        
    }
}