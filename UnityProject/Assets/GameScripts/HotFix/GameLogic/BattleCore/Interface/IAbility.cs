using ET;

namespace GameLogic.Battle
{
    /// <summary>
    /// 能力实体，存储着某个英雄某个能力的数据和状态
    /// </summary>
    public interface IAbility
    {
        /// <summary>
        /// 能力拥有者
        /// </summary>
        public Actor Owner { get; }
        /// <summary>
        /// 能力创造者
        /// </summary>
        public Actor Creator { get; set; }
        //public bool Enable { get; set; }
        
    }
}