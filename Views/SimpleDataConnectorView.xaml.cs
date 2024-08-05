using SimpleDataConnector.ViewModels;

namespace SimpleDataConnector.Views
{
    public sealed partial class SimpleDataConnectorView
    {
        public SimpleDataConnectorView(SimpleDataConnectorViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }
    }
}