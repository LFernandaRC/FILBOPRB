using EventosCorferias.Models;

using Mopups.Services;
using System.Windows.Input;

namespace EventosCorferias.Views.PopUp;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class AlertPopup
{
    private readonly ClaseBase claseBase;

    public static readonly BindableProperty PrimaryCommandProperty = BindableProperty.Create(nameof(PrimaryCommand), typeof(ICommand), typeof(AlertPopup));

    public static readonly BindableProperty SecondaryCommandProperty = BindableProperty.Create(nameof(SecondaryCommand), typeof(ICommand), typeof(AlertPopup));

    public static readonly BindableProperty PrimaryButtonTextProperty = BindableProperty.Create(nameof(PrimaryButtonText), typeof(string), typeof(AlertPopup));

    public static readonly BindableProperty SecondaryButtonTextProperty = BindableProperty.Create(nameof(SecondaryButtonText), typeof(string), typeof(AlertPopup));

    public static readonly BindableProperty MessageTextProperty = BindableProperty.Create(nameof(Message), typeof(string), typeof(AlertPopup));

    public static readonly BindableProperty TitlePopupTextProperty = BindableProperty.Create(nameof(TitlePopup), typeof(string), typeof(AlertPopup));

    public static readonly BindableProperty YesorNotProperty = BindableProperty.Create(nameof(YesorNot), typeof(bool), typeof(AlertPopup));

    private TaskCompletionSource<bool> taskCompletionSource;
    public Task PopupClosedTask { get { return taskCompletionSource.Task; } }


    public string PrimaryButtonText
    {
        get => (string)GetValue(PrimaryButtonTextProperty);
        set => SetValue(PrimaryButtonTextProperty, value);
    }
    public string SecondaryButtonText
    {
        get => (string)GetValue(SecondaryButtonTextProperty);
        set => SetValue(SecondaryButtonTextProperty, value);
    }

    public string Message
    {
        get => (string)GetValue(MessageTextProperty);
        set => SetValue(MessageTextProperty, value);
    }

    public string TitlePopup
    {
        get => (string)GetValue(TitlePopupTextProperty);
        set => SetValue(TitlePopupTextProperty, value);
    }

    public bool YesorNot
    {
        get => (bool)GetValue(YesorNotProperty);
        set => SetValue(YesorNotProperty, value);
    }

    public ICommand PrimaryCommand
    {
        get => (ICommand)GetValue(PrimaryCommandProperty);
        set => SetValue(PrimaryCommandProperty, value);
    }
    public ICommand SecondaryCommand
    {
        get => (ICommand)GetValue(SecondaryCommandProperty);
        set => SetValue(SecondaryCommandProperty, value);
    }

    public AlertPopup()
    {
        claseBase = new ClaseBase();
        try
        {
            InitializeComponent();
            taskCompletionSource = new TaskCompletionSource<bool>(); // 🔹 Inicialización necesaria
        }
        catch (Exception ex)
        {
            claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "AlertPopup", "AlertPopup", "Abre popup");
        }
    }

    protected override void OnAppearing()
    {
        try
        {
            base.OnAppearing();
            taskCompletionSource = new TaskCompletionSource<bool>();
        }
        catch (Exception ex)
        {
            claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "AlertPopup", "OnAppearing", "n/a");
        }
    }

    protected override void OnDisappearing()
    {
        try
        {
            base.OnDisappearing();
            taskCompletionSource.SetResult(true);
        }
        catch (Exception ex)
        {
            claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "AlertPopup", "OnDisappearing", "n/a");
        }
    }

    private async void Button_OnClicked(object sender, EventArgs e)
    {
        try
        {
            await MopupService.Instance.PopAsync();
        }
        catch (Exception ex)
        {
            claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "AlertPopup", "Button_OnClicked", "Cierra popup");
        }
    }

}