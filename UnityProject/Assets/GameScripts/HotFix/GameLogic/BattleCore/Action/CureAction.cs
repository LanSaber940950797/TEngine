using ET;
using GameConfig;

namespace GameLogic.Battle
{
     /// <summary>
    /// 治疗行动
    /// </summary>
    public class CureAction : Entity, IAwake,IDestroy,IAction
    {
       
        /// 治疗数值
        public long CureValue { get; set; }
        
        /// 行动实体
        public Actor Creator { get; set; }
        /// 目标对象
        public Actor Target { get; set; }

        public IAction SourceAction { get; set; }

       

    }
    
    [EntitySystemOf(typeof(CureAction))]
    public static partial class CureActionSystem
    {
        [EntitySystem]
        public static void Destroy(this CureAction self)
        {
            self.Creator = null;
            self.Target = null;
            self.SourceAction = null;
        }
        
   

        //前置处理
        private static async ETTask PreProcess(this CureAction self)
        {
            await ETTask.CompletedTask;
        }

        private static async ETTask DoActionInner(this CureAction self)
        {
            self.Target?.GetComponent<ActorNumericComponent>().Modify(NumericType.Hp, self.CureValue);
            await ETTask.CompletedTask;
        }

        //后置处理
        private static async ETTask PostProcess(this CureAction self)
        {
            self.Creator?.SendEvent(ActionEvent.PostGiveCure, self);
            self.Target?.SendEvent(ActionEvent.PostReceiveCure, self);
            await ETTask.CompletedTask;
        }

        public static async ETTask DoAction(this CureAction self)
        {
            await self.PreProcess();
            await self.DoActionInner();
            await self.PostProcess();
        }
        

    }
}