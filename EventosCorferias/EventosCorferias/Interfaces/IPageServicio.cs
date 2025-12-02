namespace EventosCorferias.Interfaces
{
    public interface IPageServicio
    {
        Task PushModalAsync(Page page);
        Task<bool> DisplayAlert(string title, string message, string ok, string cancel);
        Task<string> DisplayOpcion(string title, string message, string ok, string cancel);
        Task DisplayAlert(string title, string message, string ok);
        Task<bool> DisplayAlertDos(string title, string message, string ok);
    }
}
