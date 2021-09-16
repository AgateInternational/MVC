using Framework.Architecture.Base;
using Game.Module.Currency;

namespace Game.Module.Analytic
{
    public class AnalyticConnector : BaseConnector
    {
        private AnalyticController _analytic;

        protected override void Connect()
        {
            Subscribe<CurrencyMessage>(_analytic.TrackGold);
        }

        protected override void Disconnect()
        {
            Unsubscribe<CurrencyMessage>(_analytic.TrackGold);
        }
    }
}
