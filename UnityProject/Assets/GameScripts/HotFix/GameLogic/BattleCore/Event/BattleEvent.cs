using TEngine;

namespace GameLogic.Battle
{
    public class BattleEvent
    {
        //框架1-100 scene触发
        public const int AllEvent = 1; //所有事件,用来给观察者提供通用接口接收枚举，实际不触发
        public const int DoEffect = 2; ////效果执行 Effect实体触发
        public const int InitEffectTrigger = 3; //抛出给上层定义各自的效果触发组件
        public const int InitActor = 4; //actor创建初始化，推荐创建逻辑组件
        public const int InitActorView = 5; //actor创建初始化，推荐创建显示组件
        public const int ActorCreate = 6; //actor创建好了，推荐其他功能模块监听用
    }
    
    //acotr事件 actor触发 101-200
    public static class ActorEvent
    {
        public const int ActorStatusChange = 101;
        public const int ActorDead = 102;
    }

    //action事件 actor触发  201-500
    //不是所有的action都需要触发状态，根据需要
    public static class ActionEvent
    {
        //施法
        public const int PreSpell = 201; //施法前
        public const int PostSpell = 202; //施法后
        //伤害行动
        public const int PreCauseDamage = 211; //造成伤害前
        public const int PreReceiveDamage = 212; //承受伤害前
        public const int PostCauseDamage = 213; //造成伤害后
        public const int PostReceiveDamage = 214; //承受伤害后
        
        //治疗行动
        public const int PostGiveCure = 221; //给予治疗后
        public const int PostReceiveCure = 222; //接受治疗后
        
        //添加buff
        public const int PostGiveBuff = 231; //赋加状态后
        public const int PostReceiveBuff = 232; //承受状态后
        
        // public const int AssignEffect = 7; //赋给技能效果
        // public const int ReceiveEffect = 8; //接受技能效果

    }
}