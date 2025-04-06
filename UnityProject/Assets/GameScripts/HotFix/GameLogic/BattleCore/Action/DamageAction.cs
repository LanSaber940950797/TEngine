
using ET;
using GameConfig;
using Log = TEngine.Log;


namespace GameLogic.Battle
{

    
   
    public class DamageAction : Entity, IAwake,IDestroy,IAction
    {
        /// 伤害数值
        public long DamageValue { get; set; }
        //实际造成伤害
        public long RealDamageValue { get; set; }
        /// 是否是暴击
        public bool IsCritical { get; set; }
        
        //伤害类型
        public DamageType DamageType { get; set; }
        
        /// 行动实体
        public Actor Creator { get; set; }
        /// 目标对象
        public Actor Target { get; set; }
        public IAction SourceAction { get; set; }

        public IAbility Ability; //所属能力

        public bool IsTargetDead;

    }
    
    [EntitySystemOf(typeof(DamageAction))]
    public static partial class DamageActionSystem
    {
        [EntitySystem]
        public  static void Destroy(this DamageAction self)
        {
            self.Creator = null;
            self.Target = null;
            self.SourceAction = null;
            self.IsTargetDead = false;
        }
        
        /// 前置处理
        private static async  ETTask PreProcess(this DamageAction self)
        {
            self.CalculateDamage();

            //触发 造成伤害前 行动点
            self.Creator?.SendEvent(ActionEvent.PreCauseDamage, self);
            //触发 承受伤害前 行动点
            self.Target?.SendEvent(ActionEvent.PreReceiveDamage, self);
            await ETTask.CompletedTask;
        }
       
        //计算最终伤害
        private static void CalculateDamage(this DamageAction self)
        {
            self.RealDamageValue = self.DamageValue;
        }
        

        /// 应用伤害
        public static async ETTask DoActionInner(this DamageAction self)
        {
            Log.Debug($"DamageAction ApplyDamage {self.Creator.Id} -> {self.Target.Id} {self.RealDamageValue}");
            var cur = self.Target.GetComponent<ActorNumericComponent>().GetAsLong(NumericType.Hp);
            cur -= self.RealDamageValue;
            if (cur <= 0)
            {
                cur = 0;
            }
            self.Target.GetComponent<ActorNumericComponent>().Set(NumericType.Hp, cur);
            if (cur == 0)
            {
                self.IsTargetDead = true;
            }

            await ETTask.CompletedTask;
        }

        /// 后置处理
        private static async ETTask PostProcess(this DamageAction self)
        {
            //触发 造成伤害后 行动点
            self.Creator.SendEvent(ActionEvent.PostCauseDamage, self);
            //触发 承受伤害后 行动点
            self.Target.SendEvent(ActionEvent.PostReceiveDamage, self);
            if (self.IsTargetDead)
            {
                //todo 目标死亡
            }
        }

        public static async ETTask DoAction(this DamageAction self)
        {
            await self.PreProcess();
            if(self.IsDisposed) return;
            await self.DoActionInner();
            await self.PostProcess();
            self.Dispose();
        }
    }
}