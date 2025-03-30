namespace GameLogic.Battle
{
    public static class BattleHelper
    {
        public static int ToFrameTime(float time)
        {
            return (int)(time * 1000);
        }
    }
}