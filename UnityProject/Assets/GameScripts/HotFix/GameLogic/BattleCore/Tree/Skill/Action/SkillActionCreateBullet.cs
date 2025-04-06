// using Kurisu.AkiBT;
//
// using UnityEngine;
//
// namespace GameLogic.Battle
// {
//     [AkiInfo("Action:创建子弹")]
//     [AkiLabel("Skill:创建子弹")]
//     [AkiGroup("Skill")]
//     public class SkillActionCreateBullet : SkillAction
//     {
//         [AkiLabel("子弹id")]
//         [SerializeField]
//         public SharedInt bulletId;
//         [AkiLabel("存活时间")]
//         [SerializeField]
//         public SharedFloat duration;
//         [AkiLabel("速度")]
//         [SerializeField]
//         public SharedFloat speed;
//         [AkiLabel("目标类型")]
//         [SerializeField]
//         public SpellTargetType targetType;
//         [AkiLabel("目标阵营")]
//         [SerializeField]
//         public TargetSideType targetSideType;
//         public override void Awake()
//         {
//             base.Awake();
//         }
//
//         protected override Status OnUpdate()
//         {
//             Actor actor = skillTree.Owner;
//             var bullet = actor.CreateBullet(skillTree.Execute, 1, (int)(duration.Value * 1000));
//             var actorTransform = actor.GetComponent<TransformComponent>();
//             var transform = bullet.GetComponent<TransformComponent>();
//             transform.Position = actorTransform.CenterPosition;
//             transform.Forward = actorTransform.Forward;
//             transform.Radius = 0.5f;
//             transform.Shape = ERoleShape.Circle;
//             transform.Height = 0.2f;
//             var bulletAIComponent  = bullet.AddComponent<BulletAIComponent>();
//             var aiComponent = actor.GetComponent<ActorAIComponent>();
//             bulletAIComponent.Init(targetType, targetSideType, aiComponent.Target, TSVector.zero);
//             bulletAIComponent.IsDestryWhenDoEffect = true;
//
//             var move = bullet.GetComponent<BulletMoveComponent>();
//             move.Speed = speed.Value;
//             
//            
//            
//             return Status.Success;
//         }
//     }
// }