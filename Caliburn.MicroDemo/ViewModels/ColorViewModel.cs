using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Caliburn.Micro;
using Caliburn.MicroDemo.Message;

namespace Caliburn.MicroDemo.ViewModels
{
    [Export(typeof(ColorViewModel))]
    class ColorViewModel
    {
        private readonly IEventAggregator _events;
        [ImportingConstructor]
        public ColorViewModel(IEventAggregator events)
        {
            _events=events;
        }
        public void Red()
        {
            _events.PublishOnBackgroundThreadAsync(new ColorEvent(new SolidColorBrush(Colors.Red)));
        }

        public void Green()
        {
            _events.PublishOnCurrentThreadAsync(new ColorEvent(new SolidColorBrush(Colors.Green)));
        }

        public void Blue()
        {
            _events.PublishOnUIThreadAsync(new ColorEvent(new SolidColorBrush(Colors.Blue)));
        }
    }
}
