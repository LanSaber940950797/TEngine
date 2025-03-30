using ET;
using UnityEngine;


namespace GameLogic.Battle
{
    [ComponentOf(typeof(Actor))]
   
    public partial class TransformComponent : Entity, IAwake,ISerializeToEntity
    {
        //坐标
        public Vector3 Position
        {
            get;
            set;
        }


     
        public Vector3 Forward//朝向
        {
            get
            { return this.Rotation * Vector3.forward; }
            set => this.Rotation = Quaternion.LookRotation(value, Vector3.up);
        }

        public Quaternion Rotation //角度
        {
            get;
            set;
        }

        public float Height; //高度

        public Vector3 CenterPosition
        {
            get => this.Position + Vector3.forward * this.Height / 2;
        }
        public ERoleShape Shape = ERoleShape.Circle;
        public float Radius;
    }
}