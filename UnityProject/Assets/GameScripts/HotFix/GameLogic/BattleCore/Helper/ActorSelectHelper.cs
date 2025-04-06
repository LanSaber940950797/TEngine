using System.Collections.Generic;
using ET;

//using DG.DemiEditor;

namespace GameLogic.Battle
{
    /// <summary>
    /// 战斗单位选择器
    /// </summary>
    public static class ActorSelectHelper
    {
        public static TargetSideType GetTargetSideType(this Actor self, Actor target)
        {
            if (self.SideType == target.SideType)
            {
                return TargetSideType.Friend;
            }

            return TargetSideType.Enemy;
        }


        public static bool IsCanSelect(this Actor self)
        {
            // 系统角色不能选中
            //子弹跟法术场一般是没血条不可打击的，所以不可选择，如果有弹子弹之类的游戏需要修改
            return self.ActorType != ActorType.System
                   && self.ActorType != ActorType.Bullet
                   && self.ActorType != ActorType.MagicField;
        }
        
        public static List<EntityRef<Actor>> GetActors(Actor self, TargetSideType targetSide, bool isNoSelf = true, ActorType actorType = ActorType.CanSelect)
        {
            return self.GetParent<ActorComponent>().GetActors(targetSide, isNoSelf ? self : null, actorType);
        }

       
    }
}