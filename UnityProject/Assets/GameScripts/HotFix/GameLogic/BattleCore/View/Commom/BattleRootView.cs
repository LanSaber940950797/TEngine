using ET;
using UnityEngine;

namespace GameLogic.Battle
{
    [ComponentOf]
    public class BattleRootView : Entity, IAwake, IDestroy
    {
        public GameObject BattleRoot;
    }
}