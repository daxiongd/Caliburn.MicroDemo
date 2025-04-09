using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;
using Caliburn.Micro;
using Caliburn.MicroDemo.Message;

namespace Caliburn.MicroDemo.ViewModels
{
    [Export(typeof(ShellViewModel))]
    class ShellViewModel:Conductor<object>,IHandle<ColorEvent>
    {
        private readonly IWindowManager _windowManager;
        private readonly IEventAggregator _eventManager;
        public ColorViewModel ColorModel { get; private set; }
        [ImportingConstructor]
        public ShellViewModel(ColorViewModel colorModel,IEventAggregator events, IWindowManager windowManager)
        {
            ColorModel = colorModel;
            events.SubscribeOnUIThread(this);
            _windowManager = windowManager;
        }
        private SolidColorBrush _Color;
        public SolidColorBrush Color
        {
            get { return _Color; }
            set
            {
                _Color = value;
                NotifyOfPropertyChange(() => Color);
            }
        }
        private int _count = 50;

        public int Count
        {
            get { return _count; }
            set
            {
                _count = value;
                NotifyOfPropertyChange(() => Count);
                NotifyOfPropertyChange(() => CanIncrementCount);
            }
        }
        public void IncrementCount(int delay)
        {
            Count += delay;
        }


        public bool CanIncrementCount
        {
            get { return Count < 100; }
        }
        public void OpenWindow()
        {
            _windowManager.ShowWindowAsync(new ColorViewModel(_eventManager));
        }

        public Task HandleAsync(ColorEvent message, CancellationToken cancellationToken)
        {
            Color = message.Color;
            return Task.CompletedTask;
        }
        public void ShowRedScreen()
        {
            ActivateItemAsync(new RedViewModel());
        }

        public void ShowGreenScreen()
        {
            ActivateItemAsync(new GreenViewModel());
        }

        public void ShowBlueScreen()
        {
            ActivateItemAsync(new BlueViewModel());
        }

    }
}
