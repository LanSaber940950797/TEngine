using TEngine;

namespace GameLogic.Battle
{
    public class BattleEvent
    {
        
        public  const int ActorStatusChange = 1;
        public  const int ActorDead = 2;
        public  const int ActorAttrUpdate = 3;
        public  const int ForceChangePosition = 4;
        
        //这个事件是逻辑层创建
        public  const int ActorCreate = 5;
        //这个事件通知渲染层，有些actor是数据加载进来的，不走逻辑层ActorCreate
        public  const int ActorCreateView = 6;
        public  const int BattleEnd = 7;
        public const int AllEvent = 8; //所有事件
    }
}