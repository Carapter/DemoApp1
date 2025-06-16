using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DemoApp.Entities;
using DemoApp.Helpers;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;

namespace DemoApp.ViewModels
{
    public partial class EditPartnerViewModel : ViewModelBase
    {
        private readonly DbDemoFdbContext _context;
        private readonly PartnersViewModel? _parentViewModel;
        private readonly Partner _partnerEntity;
        [ObservableProperty]
        private string? _partnerName;

        [ObservableProperty]
        private string? _partnerInn;

        [ObservableProperty]
        private string? _director;

        [ObservableProperty]
        private string? _directorPhone;

        [ObservableProperty]
        private string? _directorMail;

        [ObservableProperty]
        private int? _partnerRating;

        [ObservableProperty]
        private int? _selectedPartnerType;

        [ObservableProperty]
        private ObservableCollection<PartnerType> _partnerTypes = new();

        [ObservableProperty]
        private string? _errorMessage;
        public EditPartnerViewModel(DbDemoFdbContext context, Partner? partner = null, PartnersViewModel? parentViewModel = null)
        {
            _context = context;
            _parentViewModel = parentViewModel;
            _partnerEntity = partner ?? new Partner();

            LoadPartnerTypes();
            LoadPartnerData();
        }

        private void LoadPartnerTypes()
        {
            var types = _context.PartnerTypes.ToList();
            PartnerTypes = new ObservableCollection<PartnerType>(types);
        }
        private void LoadPartnerData()
        {
            PartnerName = _partnerEntity.PartnerName;
            PartnerInn = _partnerEntity.PartnerInn;
            Director = _partnerEntity.Director;
            DirectorPhone = _partnerEntity.DirectorPhone;
            DirectorMail = _partnerEntity.DirectorMail;
            PartnerRating = _partnerEntity.PartnerRating;
            SelectedPartnerType = _partnerEntity.PartnerType;
        }

        private bool ValidateData()
        {
            if (string.IsNullOrWhiteSpace(PartnerName))
            {
                ErrorMessage = "Название партнера обязательно для заполнения";
                return false;
            }

            if (!ValidationHelper.IsValidInn(PartnerInn))
            {
                ErrorMessage = "ИНН должен содержать 10 или 12 цифр";
                return false;
            }

            if (string.IsNullOrWhiteSpace(Director))
            {
                ErrorMessage = "Имя директора обязательно для заполнения";
                return false;
            }

            if (!ValidationHelper.IsValidPhoneNumber(DirectorPhone))
            {
                ErrorMessage = "Неверный формат номера телефона. Пример: +7(999)999-99-99";
                return false;
            }

            if (!ValidationHelper.IsValidEmail(DirectorMail))
            {
                ErrorMessage = "Неверный формат электронной почты";
                return false;
            }

            if (!ValidationHelper.IsValidRating(PartnerRating))
            {
                ErrorMessage = "Рейтинг должен быть от 0 до 100";
                return false;
            }

            if (SelectedPartnerType == null)
            {
                ErrorMessage = "Выберите тип партнера";
                return false;
            }

            ErrorMessage = null;
            return true;
        }



        [RelayCommand]
        private async Task SaveAsync()
        {
            if (!ValidateData())
            {
                var box = MessageBoxManager
           .GetMessageBoxStandard("Error", ErrorMessage,
               ButtonEnum.Ok);

                var result = await box.ShowAsync();
                return;
            }

            _partnerEntity.PartnerName = PartnerName;
            _partnerEntity.PartnerInn = PartnerInn;
            _partnerEntity.Director = Director;
            _partnerEntity.DirectorPhone = DirectorPhone;
            _partnerEntity.DirectorMail = DirectorMail;
            _partnerEntity.PartnerRating = PartnerRating;
            _partnerEntity.PartnerType = SelectedPartnerType;

            if (_partnerEntity.PartnerId == 0)
            {
                _context.Partners.Add(_partnerEntity);
            }

            _context.SaveChanges();
            _parentViewModel?.LoadPartners();
            Cancel();
        }

        [RelayCommand]
        private void Cancel()
        {
            _parentViewModel?._mainViewModel.ShowPartnersList();
        }
    }
}