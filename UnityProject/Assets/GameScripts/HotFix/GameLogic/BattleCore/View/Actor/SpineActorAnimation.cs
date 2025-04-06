using Spine.Unity;
using UnityEngine;

namespace GameLogic.Battle
{
    public class SpineActorAnimation : MonoBehaviour,IActorAnimation
    {
       
        [SpineAnimation]
        public string moveAnimationName;
        [SpineAnimation]
        public string idleAnimationName;
        [SpineAnimation]
        public string hurtAnimationName;
        [SpineAnimation]
        public string deathAnimationName;
        SkeletonAnimation skeletonAnimation;

        // Spine.AnimationState and Spine.Skeleton are not Unity-serialized objects. You will not see them as fields in the inspector.
        public Spine.AnimationState spineAnimationState;
        public Spine.Skeleton skeleton;
        
        private ActorAnimationTag _tag;

        public ActorAnimationTag Tag
        {
            get => _tag;
            set
            {
                _tag = value;
            }
        }
        
        void Start () {
            // Make sure you get these AnimationState and Skeleton references in Start or Later.
            // Getting and using them in Awake is not guaranteed by default execution order.
            skeletonAnimation = GetComponent<SkeletonAnimation>();
            spineAnimationState = skeletonAnimation.AnimationState;
            skeleton = skeletonAnimation.Skeleton;
            PlayIde();
        }
        
        public void PlayIde()
        {
            if (spineAnimationState == null)
            {
                return;
            }
            var entry = spineAnimationState.GetCurrent(0);
            if (entry != null)
            {
                if (entry.Animation.Name == moveAnimationName)
                {
                    spineAnimationState.SetAnimation(0, idleAnimationName, true);
                }
                else
                {
                    spineAnimationState.AddAnimation(0, idleAnimationName, true, 0);
                }
            }
            else
            {
                spineAnimationState.SetAnimation(0, idleAnimationName, true);
            }
           
        }

        public void PlayMove()
        {
            if (spineAnimationState == null)
            {
                return;
            }
            var entry = spineAnimationState.GetCurrent(0);
            if (entry != null)
            {
                if (entry.Animation.Name == idleAnimationName)
                {
                    spineAnimationState.SetAnimation(0, moveAnimationName, true);
                }
                else
                {
                    spineAnimationState.AddAnimation(0, moveAnimationName, true, 0);
                }
            }
            else
            {
                spineAnimationState.SetAnimation(0, moveAnimationName, true);
            }
           
        }

        public void PlayOnHurt()
        {
            if (spineAnimationState == null)
            {
                return;
            }
            if (!string.IsNullOrEmpty(hurtAnimationName))
            {
                PlayAnimation(hurtAnimationName);
            }
        }

        public void PlayDeath()
        {
            if (spineAnimationState == null)
            {
                return;
            }
            if (!string.IsNullOrEmpty(deathAnimationName))
            {
                PlayAnimation(deathAnimationName);
            }
        }
        
        public void PlayAnimation(string animationName)
        {
            if (spineAnimationState == null)
            {
                return;
            }
            if ((_tag & ActorAnimationTag.Idle) > 0)
            {
                spineAnimationState.SetAnimation(0, animationName, false);
                spineAnimationState.AddAnimation(0, idleAnimationName, true, 0);
            }
            else if((_tag & ActorAnimationTag.Move) > 0)
            {
                spineAnimationState.SetAnimation(0, animationName, false);
                spineAnimationState.AddAnimation(0, moveAnimationName, true, 0);
            }
        }
    }
}