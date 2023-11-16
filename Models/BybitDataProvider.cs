using Bybit.Net.Clients;
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
    internal class BybitDataProvider: ICryptoMarketClientProvider, INotifyPropertyChanged
    {
        private MarketsMediator mediator;
        private readonly BybitSocketClient bybitSocketClient;
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

        public BybitDataProvider(MarketsMediator mediator)
        {
            this.mediator = mediator;
            Pair = "...";
            pairPrice = "...";
            bybitSocketClient = new BybitSocketClient(options =>
            {
                options.AutoReconnect = true;
            });
            Subscribe("BTCUSDT");
        }

        public void ChangePair(string newPair)
        {
            Pair = "...";
            pairPrice = "...";
            if (subscriptionId != null)
            {
                bybitSocketClient.UnsubscribeAsync(subscriptionId.Value).Wait();
            }
            Subscribe(newPair);
        }

        private void Subscribe(string pair)
        {
            var result = bybitSocketClient.V5SpotApi.SubscribeToTickerUpdatesAsync(pair, data =>
            {
                var dataResult = data.Data;
                PairPrice = dataResult.LastPrice.ToString();
                Pair = dataResult.Symbol;
            });
            if (result.Result.Success)
            {
                subscriptionId = result.Result.Data.Id;
            }
        }

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
