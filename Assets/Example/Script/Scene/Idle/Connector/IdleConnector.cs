using Agate.MVC.Base;
using Example.Scene.Idle.Gold;

namespace Example.Scene.Idle
{
    public class IdleConnector : BaseConnector
    {
        private GoldController _gold;

        protected override void Connect()
        {
            Subscribe<EarnGoldMessage>(ProcessEarnGold);
        }

        protected override void Disconnect()
        {
            Unsubscribe<EarnGoldMessage>(ProcessEarnGold);
        }

        private void ProcessEarnGold(EarnGoldMessage message)
        {
            _gold.AddGold(message.Value);
        }
    }
}
