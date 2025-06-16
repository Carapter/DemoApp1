using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DemoApp.Entities;
using DemoApp.Views;
using Microsoft.EntityFrameworkCore;

namespace DemoApp.ViewModels
{
    public partial class PartnersViewModel : ViewModelBase
    {
        [ObservableProperty]
        private ObservableCollection<Models.Partner> _partners = new();

        [ObservableProperty]
        private Models.Partner? _selectedPartner;
        private readonly DbDemoFdbContext _context;
        internal readonly MainWindowViewModel _mainViewModel;


        public PartnersViewModel(DbDemoFdbContext context, MainWindowViewModel mainViewModel)
        {
            _context = context;
            _mainViewModel = mainViewModel;
            LoadPartners();
        }
        public void LoadPartners()
        {
            var entities = _context.Partners.Include(p => p.Sales).Include(p => p.PartnerTypeNavigation).ToList();
            Partners = new ObservableCollection<Models.Partner>(
                entities.Select(e => new Models.Partner(e))
            );
        }
        [RelayCommand]
        private void EditPartner()
        {
            if (SelectedPartner == null) return;
            var partnerEntity = _context.Partners.Find(SelectedPartner._partner.PartnerId);
            if (partnerEntity == null) return;

            var viewModel = new EditPartnerViewModel(_context, partnerEntity, this);
            var editControl = new EditPartnerView { DataContext = viewModel };
            _mainViewModel.ShowEditPartner(editControl);
        }
        [RelayCommand]
        private void AddPartner()
        {
            var viewModel = new EditPartnerViewModel(_context, parentViewModel: this);
            var editControl = new EditPartnerView { DataContext = viewModel };
            _mainViewModel.ShowEditPartner(editControl);
        }
        [RelayCommand]
        private void ViewSalesHistory(Partner? partner)
        {
            //история продаж
        }

    }
}
