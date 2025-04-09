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



        
        public static List<EntityRef<Actor>> GetActors(Actor self, TargetSideType targetSide, bool isNoSelf = true, ActorType actorType = ActorType.CanSelect)
        {
            return self.GetParent<ActorComponent>().GetActors(targetSide, isNoSelf ? self : null, actorType);
        }

       
    }
}