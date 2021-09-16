namespace Game.Module.Currency
{
    public struct CurrencyMessage
    {
        public int Gold {get; private set;}
        public CurrencyMessage(int gold){
            Gold = gold;
        }
    }
}
