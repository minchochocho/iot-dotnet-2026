using CommunityToolkit.Mvvm.ComponentModel;
using MahApps.Metro.Controls.Dialogs;

namespace WpfMvvm02.ViewModels {
    internal class DivisionViewModel : ObservableObject {
        private readonly IDialogCoordinator _coordinator;

        public DivisionViewModel(IDialogCoordinator coordinator) {
            _coordinator = coordinator;
        }
    }
}
