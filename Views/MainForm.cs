using CryptoExchange.Net.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsCryptoApp.Models;

namespace WinFormsCryptoApp.Views
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            var mediator = new MarketsMediator();

            var pairLabel = new Label();
            pairLabel.Text = "Введите пару:";
            pairLabel.Location = new Point(12, 0);
            Controls.Add(pairLabel);

            var pairTextBox1 = new TextBox();
            pairTextBox1.Text = "BTC";
            pairTextBox1.Location = new Point(12, 20);
            Controls.Add(pairTextBox1);

            var pairTextBox2 = new TextBox();
            pairTextBox2.Text = "USDT";
            pairTextBox2.Location = new Point(112, 20);
            Controls.Add(pairTextBox2);

            var applyButton = new Button();
            applyButton.Text = "Принять";
            applyButton.Location = new Point(12, 40);
            applyButton.Click += (s, a) => mediator.ChangePair(pairTextBox1.Text, pairTextBox2.Text);
            Controls.Add(applyButton);

            AddBinance(mediator);
            AddBybit(mediator);
            AddBitget(mediator);
            AddKucoin(mediator);
        }

        private void AddBinance(MarketsMediator mediator)
        {
            var binanceLabel = new Label();
            binanceLabel.Text = "Binance";
            binanceLabel.Location = new Point(12, 65);
            Controls.Add(binanceLabel);

            var binanceLabelPair = new Label();
            binanceLabelPair.Location = new Point(12, 85);
            Controls.Add(binanceLabelPair);

            var binanceLabelIndex = new Label();
            binanceLabelIndex.Location = new Point(12, 105);
            Controls.Add(binanceLabelIndex);

            binanceLabelIndex.DataBindings.Add(new Binding(
                "Text", mediator.binanceDataProvider, "PairPrice", false, DataSourceUpdateMode.OnPropertyChanged));
            binanceLabelPair.DataBindings.Add(new Binding(
                "Text", mediator.binanceDataProvider, "Pair", false, DataSourceUpdateMode.OnPropertyChanged));
        }

        private void AddBybit(MarketsMediator mediator)
        {
            var bybitLabel = new Label();
            bybitLabel.Text = "Bybit";
            bybitLabel.Location = new Point(150, 65);
            Controls.Add(bybitLabel);

            var bybitLabelPair = new Label();
            bybitLabelPair.Location = new Point(150, 85);
            Controls.Add(bybitLabelPair);

            var bybitLabelIndex = new Label();
            bybitLabelIndex.Location = new Point(150, 105);
            Controls.Add(bybitLabelIndex);

            bybitLabelIndex.DataBindings.Add(new Binding(
                "Text", mediator.bybitDataProvider, "PairPrice", false, DataSourceUpdateMode.OnPropertyChanged));
            bybitLabelPair.DataBindings.Add(new Binding(
                "Text", mediator.bybitDataProvider, "Pair", false, DataSourceUpdateMode.OnPropertyChanged));
        }

        private void AddKucoin(MarketsMediator mediator)
        {
            var kucoinLabel = new Label();
            kucoinLabel.Text = "Kucoin";
            kucoinLabel.Location = new Point(12, 145);
            Controls.Add(kucoinLabel);

            var kucoinLabelPair = new Label();
            kucoinLabelPair.Location = new Point(12, 165);
            Controls.Add(kucoinLabelPair);

            var kucoinLabelIndex = new Label();
            kucoinLabelIndex.Location = new Point(12, 185);
            Controls.Add(kucoinLabelIndex);

            kucoinLabelIndex.DataBindings.Add(new Binding(
                "Text", mediator.kucoinDataProvider, "PairPrice", false, DataSourceUpdateMode.OnPropertyChanged));
            kucoinLabelPair.DataBindings.Add(new Binding(
                "Text", mediator.kucoinDataProvider, "Pair", false, DataSourceUpdateMode.OnPropertyChanged));
        }

        private void AddBitget(MarketsMediator mediator)
        {
            var bitgetLabel = new Label();
            bitgetLabel.Text = "Bitget";
            bitgetLabel.Location = new Point(150, 145);
            Controls.Add(bitgetLabel);

            var bitgetLabelPair = new Label();
            bitgetLabelPair.Location = new Point(150, 165);
            Controls.Add(bitgetLabelPair);

            var bitgetLabelIndex = new Label();
            bitgetLabelIndex.Location = new Point(150, 185);
            Controls.Add(bitgetLabelIndex);

            bitgetLabelIndex.DataBindings.Add(new Binding(
                "Text", mediator.bitgetDataProvider, "PairPrice", false, DataSourceUpdateMode.OnPropertyChanged));
            bitgetLabelPair.DataBindings.Add(new Binding(
                "Text", mediator.bitgetDataProvider, "Pair", false, DataSourceUpdateMode.OnPropertyChanged));
        }
    }
}
