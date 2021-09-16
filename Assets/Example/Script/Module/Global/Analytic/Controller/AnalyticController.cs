using Framework.Architecture.Base;
using UnityEngine;
using Game.Module.Currency;

namespace Game.Module.Analytic
{
    public class AnalyticController : BaseController<AnalyticController>
    {
        public void TrackGold(CurrencyMessage message)
        {
            Debug.Log($"Analytic Tracked Gold : {message.Gold}");
        }
    }
}
