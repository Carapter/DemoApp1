using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using DemoApp.Entities;
using DemoApp.Views;

namespace DemoApp.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private readonly DbDemoFdbContext _context;

    [ObservableProperty]
    private Control? _content;
    public void ShowContent(Control content)
    {
        Content = content;
    }
    public void ShowPartnersList()
    {
        Content = new PartnersView
        {
            DataContext = new PartnersViewModel(_context, this)
        };
    }

    public MainWindowViewModel()
    {
        _context = new DbDemoFdbContext();
        ShowPartnersList();
    }

    public void ShowEditPartner(EditPartnerView editControl)
    {
        Content = editControl;
    }

}
