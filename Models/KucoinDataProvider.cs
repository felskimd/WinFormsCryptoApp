using Kucoin.Net.Clients;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WinFormsCryptoApp.Models.Interfaces;
using WinFormsCryptoApp.Views;

namespace WinFormsCryptoApp.Models
{
    internal class KucoinDataProvider: ICryptoMarketClientProvider, INotifyPropertyChanged
    {
        private MarketsMediator mediator;
        private readonly KucoinSocketClient kucoinSocketClient;
        private string pair;
        public string Pair
        {
            get { return pair; }
            set
            {
                if (MainForm.ActiveForm != null)
                    MainForm.ActiveForm.Invoke(() =>
                    {
                        pair = value;
                        OnPropertyChanged();
                    });
            }
        }
        private string pairPrice;
        public string PairPrice
        {
            get { return pairPrice; }
            set
            {
                if (MainForm.ActiveForm != null)
                    MainForm.ActiveForm.Invoke(() =>
                    {
                        pairPrice = value;
                        OnPropertyChanged();
                    });
            }
        }
        private int? subscriptionId;

        public event PropertyChangedEventHandler? PropertyChanged;

        public KucoinDataProvider(MarketsMediator mediator)
        {
            this.mediator = mediator;
            Pair = "...";
            pairPrice = "...";
            kucoinSocketClient = new KucoinSocketClient(options =>
            {
                options.AutoReconnect = true;
            });
            Subscribe("BTC-USDT");
        }

        public void ChangePair(string newPair)
        {
            Pair = "...";
            pairPrice = "...";
            if (subscriptionId != null)
            {
                kucoinSocketClient.UnsubscribeAsync(subscriptionId.Value).Wait();
            }
            Subscribe(newPair);
        }

        private void Subscribe(string pair)
        {
            var result = kucoinSocketClient.SpotApi.SubscribeToTickerUpdatesAsync(pair, data =>
            {
                var dataResult = data.Data;
                PairPrice = dataResult.BestBidPrice.ToString();
                Pair = dataResult.Symbol;
            });
            subscriptionId = result.Result.Data.Id;
        }

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
