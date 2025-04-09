using ET;
using GameConfig;
using Log = TEngine.Log;

namespace GameLogic.Battle
{


    /// <summary>
    /// 施加状态行动
    /// </summary>
    public class AddBuffAction : Entity, IAwake, IDestroy,IAction
    {
    
        
        public Actor Creator { get; set; }
        public Actor Target { get; set; }
        public IAction SourceAction { get; set; }

        public Buff Buff { get; set; }
        
        
        /// 行动实体

        public int BuffId;//要添加的buffid
        public int AddLayer; //要添加的buff层数
       

        
    }
     
    [EntitySystemOf(typeof(AddBuffAction))]
    public static partial class AddBuffActionSystem
    {
        [EntitySystem]
        public static void Destroy(this AddBuffAction self)
        {
            self.Creator = null;
            self.Target = null;
            self.Buff = null;
            self.SourceAction = null;
        }

        public static async ETTask DoAction(this AddBuffAction self)
        {
            await self.PreProcess();
            await self.DoActionInner();
            await self.PostProcess();
            //延时销毁，可能有些事件监听需要延时处理该行为，不能立即销毁
            self.AddChild<DestroyTimer, int, bool>(1000, true, true);
        }
        
        private static async ETTask PreProcess(this AddBuffAction self)
        {
            await ETTask.CompletedTask;
        }
        private static async ETTask PostProcess(this AddBuffAction self)
        {
            await ETTask.CompletedTask;
        }
        private static async ETTask DoActionInner(this AddBuffAction self)
        {
            var buffComponent = self.Target.GetComponent<BuffComponent>();
            var buff = buffComponent.GetBuff(self.BuffId);
            if (buff != null)
            {
                //todo 刷新逻辑层数
                buff.RefreshBuff(self.AddLayer);
                return;
            }
            
            buff = buffComponent.AddChild<Buff, int>(self.BuffId, true);
            buff.Creator = self.Creator;
            buff.Layer = self.AddLayer;
            buff.Activate().NoContext();
            await ETTask.CompletedTask;
        }
    }
}