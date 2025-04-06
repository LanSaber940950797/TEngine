// using System.Collections.Generic;
// using ET;
// using Kurisu.AkiBT;
//
//
// namespace GameLogic.Battle
// {
//     [AkiInfo("Action:改变AI目标")]
//     [AkiLabel("BattleAI:改变AI目标")]
//     [AkiGroup("BattleAI")]
//     public class ChangeAITargetAction : BattleAIAction
//     {
//         protected override Status OnUpdate()
//         {
//             var aiComponent = actor.GetComponent<ActorAIComponent>();
//             aiComponent.Target = null;
//             var targets = ActorSelectHelper.GetActors(actor, TargetSideType.Enemy, false);
//             if (targets.Count == 0)
//             {
//                 return  Status.Success;
//             }
//             SortByDistance(targets);
//             aiComponent.Target = targets[0];
//             return Status.Success;
//         }
//         
//         private void SortByDistance(List<EntityRef<Actor>> list)
//         {
//           
//           
//             var actorPos = actor.GetComponent<TransformComponent>().Position;
//             Dictionary<long, FP> distanceDic = new Dictionary<long, FP>();
//             for (int i = 0; i < list.Count; i++)
//             {
//                 Actor target = list[i];
//                 var distance = TSVector.Distance(actorPos, target.GetComponent<TransformComponent>().Position);
//                 distanceDic.Add(target.Id, distance);
//             }
//             
//             //距离最近的排在前面
//             list.Sort((a, b) =>
//             { 
//                 Actor at = a;
//                 Actor bt = b;
//                 return distanceDic[at.Id].CompareTo(distanceDic[bt.Id]);
//             });
//         }
//     }
// }