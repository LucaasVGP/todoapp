using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace TodoApp.ViewModel;

public partial class MainViewModel : ObservableObject
{
    private IConnectivity _connectivity;

    public MainViewModel(IConnectivity connectivity)
    {
        items = new ObservableCollection<string>();
        text = "";
        _connectivity = connectivity;
    }

    [ObservableProperty]
    private string text;

    [ObservableProperty]
    private ObservableCollection<string> items;

    [RelayCommand]
    private async Task Add()
    {
        if (_connectivity.NetworkAccess != NetworkAccess.Internet)
        {
            await Shell.Current.DisplayAlert("verificação de internet", "sem internet", "Ok");
            return;
        }

        if (string.IsNullOrEmpty(Text)) return;
        Items.Add(Text);
        Text = string.Empty;
    }

    [RelayCommand]
    private void Remove(string s)
    {
        if (Items.Contains(s))
        {
            Items.Remove(s);
        }
    }

    [RelayCommand]
    private async Task Tap(string s)
    {
        //enviar parametros simples para a pagina destino
        await Shell.Current.GoToAsync($"{nameof(DetailPage)}?Text={s}");

        //enviar objetos
        //await Shell.Current.GoToAsync($"{nameof(DetailPage)}", new Dictionary<string, object>
        //{
        //    {nameof(DetailPage), s}
        //});
    }
}