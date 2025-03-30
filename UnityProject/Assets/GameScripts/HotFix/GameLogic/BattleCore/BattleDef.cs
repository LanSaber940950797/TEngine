using System;

namespace GameLogic.Battle
{
    // 角色类型
    public enum ActorType
    {
        None = 0,
        Player = 1, //玩家
        Monster = 2,//怪物
        MagicField = 3,//法术场
        Bullet = 4, //子弹
        System = 5, //系统actor，管理战斗的
        Hero = 6, //英雄
    }

    public enum SideType
    {
        SideA = 1, //阵营A
        SideB = 2, //阵容B
        Neutral = 3, //中立
        Max = 4,
        All = 5,
    }
    
    public enum EffectiveActorType
    {
        None = 0,
        Caster, //施法者
        Owner, //拥有者
        Target, //当前目标
        Creator, //创建者
    }
    public enum ERoleShape
    {
        None = 0,
        Circle = 1,
        Rect = 2,
    }
    [Flags]
    public enum StatusFlag {
        None = 0,
        Rooted = 1, //定身，不能移动，可以正常释放技能和普通攻击
        Disarm = 1<<1,//缴械，不能普通攻击，可以释放技能
        Slienced = 1<<2, //沉默，不能放技能，可以移动和普攻
        TimeFrozen = 1<<3, //时间暂停，身上的计时器也会暂停
        DamageImmune = 1<<4, //伤害免疫
        Unselectable = 1<<5, //不可选中
        ControlImmune = 1<<6, //控制免疫
        StrongControlImmune = 1 << 7,//强控免疫

        Stunned = Rooted | Disarm | Slienced, //不能控制（不能移动、不能释放任何技能包括普通攻击）
        AllSlienced = Disarm|Slienced, //全沉默，不能放技能以及普攻，可以移动
    }
    
    [Flags]
    public enum ActorFlag
    {
        None,
        Summoned = 1,
        Dead = 1<<1,
        Delete = 1 << 2,
        Borning = 1<<3,
        Borned = 1 << 4,
        Leave = 1<<5,
    }
}