// using Kurisu.AkiBT;
//
// namespace GameLogic.Battle
// {
//     [AkiInfo("Conditional:是否正在施法")]
//     [AkiLabel("BattleAI:是否正在施法")]
//     [AkiGroup("BattleAI")]
//     public class IsSpelling : BattleAIConditional
//     {
//         protected override bool IsUpdatable()
//         {
//             return actor.GetComponent<SkillComponent>().HasSkillSpelling();
//         }
//     }
// }