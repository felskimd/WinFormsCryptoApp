namespace WinFormsCryptoApp.Models
{
    internal class MarketsMediator
    {

        public BinanceDataProvider binanceDataProvider {  get; private set; }
        public BybitDataProvider bybitDataProvider { get; private set; }
        public BitgetDataProvider bitgetDataProvider { get; private set; }
        public KucoinDataProvider kucoinDataProvider { get; private set; }

        public MarketsMediator() 
        { 
            binanceDataProvider = new BinanceDataProvider(this);
            bybitDataProvider = new BybitDataProvider(this);
            bitgetDataProvider = new BitgetDataProvider(this);
            kucoinDataProvider = new KucoinDataProvider(this);
        }

        public void ChangePair(string newPair1, string newPair2)
        {
            Task.Run(() => binanceDataProvider.ChangePair(newPair1 + newPair2));
            Task.Run(() => bybitDataProvider.ChangePair(newPair1 + newPair2));
            Task.Run(() => bitgetDataProvider.ChangePair(newPair1 + newPair2));
            Task.Run(() => kucoinDataProvider.ChangePair(newPair1 + '-' + newPair2));
        }
    }
}
