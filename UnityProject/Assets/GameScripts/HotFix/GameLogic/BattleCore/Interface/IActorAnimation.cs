using System;

namespace GameLogic.Battle
{
    [Flags]
    public enum ActorAnimationTag
    {
        None = 0,
        Idle = 1,
        Move = 1 << 1,
        Attack = 1 << 2,
    }
   
    public interface IActorAnimation
    {
        public ActorAnimationTag Tag { get; set; }
        public void PlayIde();
        public void PlayMove();
        public void PlayOnHurt();
        public void PlayDeath();
        public void PlayAnimation(string animationName);
    }
}