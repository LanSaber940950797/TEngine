using ET;
using UnityEngine;

namespace GameLogic.Battle
{
    [ComponentOf]
    public class ActorView : Entity, IAwake<Actor>, ET.ILateUpdate
    {
        private EntityRef<Actor> actor;

        public Actor Actor
        {
            get
            {
                return actor;
            }
            set
            {
                actor = value;
            }
        }

        public float totalTime;
        public float t;
        public float DeathTime = 2.5f;
    }
}