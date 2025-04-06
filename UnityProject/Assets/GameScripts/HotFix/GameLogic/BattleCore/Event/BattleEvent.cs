using TEngine;

namespace GameLogic.Battle
{
    public class BattleEvent
    {
        public const int AllEvent = 1; //所有事件,用来给观察者提供通用接口接收枚举，实际不触发
        
        //框架1-100 触发者是战斗scene
        public const int EffectTargetSelect = 2; //效果目标选择
        public const int InitEffectTrigger = 3; //抛出给上层定义各自的效果触发组件
        public const int InitActor = 4; //actor创建初始化，推荐创建逻辑组件
        public const int InitActorView = 5; //actor创建初始化，推荐创建显示组件
        public const int ActorCreate = 6; //actor创建好了，推荐其他功能模块监听用
        
        
        //acotr事件 触发者是actor 101-200
        public  const int ActorStatusChange = 101;
        public  const int ActorDead = 102;
        
        //这个事件通知渲染层，有些actor是数据加载进来的，不走逻辑层ActorCreate
        public  const int ActorCreateView = 104;
        
        //action事件 触发者是actor 会传入行动流程 201-300
       
        
        //effect事件，触发者effect, 301-400
        public const int DoEffect = 301; //效果执行
        
       
        
        
    }
}