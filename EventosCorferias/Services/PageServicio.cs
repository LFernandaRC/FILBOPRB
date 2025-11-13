using EventosCorferias.Interfaces;
using EventosCorferias.Views.PopUp;
using Mopups.Services;

namespace EventosCorferias.Services
{
    public class PageServicio : IPageServicio
    {
        public async Task DisplayAlert(string title, string message, string ok)
        {
            try
            {
                if (MopupService.Instance.PopupStack.Count > 0)
                {
                    await MopupService.Instance.PopAllAsync();
                }
                if (ok.ToLower() == "ok")
                {
                    ok = "Aceptar";
                }
                await MopupService.Instance.PushAsync(new AlertPopup
                {
                    TitlePopup = title,
                    Message = message,
                    PrimaryButtonText = ok,
                    YesorNot = false,
                    PrimaryCommand = new Command(async () =>
                    {
                        await MopupService.Instance.PopAsync();
                    })
                });
            }
            catch (Exception) { }
        }

        public async Task<bool> DisplayAlert(string title, string message, string ok, string cancel)
        {
            bool result = false;
            try
            {
                if (MopupService.Instance.PopupStack.Count > 0)
                {
                    await MopupService.Instance.PopAllAsync();
                }
                if (ok.ToLower() == "ok")
                {
                    ok = "Aceptar";
                }

                var a = new AlertPopup
                {
                    TitlePopup = title,
                    Message = message,
                    PrimaryButtonText = ok,
                    SecondaryButtonText = cancel,
                    YesorNot = true,
                    PrimaryCommand = new Command(async () =>
                    {
                        result = true;
                        await MopupService.Instance.PopAllAsync();
                    }),
                    SecondaryCommand = new Command(async () =>
                    {
                        result = false;
                        await MopupService.Instance.PopAsync();
                    })
                };
                await MopupService.Instance.PushAsync(a);
                await a.PopupClosedTask;
            }
            catch { }
            //return await MainPage.DisplayAlert(title, message, ok, cancel);
            return result;
        }

        public async Task<string> DisplayOpcion(string title, string message, string ok, string cancel)
        {
            string result = "0";
            try
            {
                if (MopupService.Instance.PopupStack.Count > 0)
                {
                    await MopupService.Instance.PopAllAsync();
                }
                if (ok.ToLower() == "ok")
                {
                    ok = "Aceptar";
                }

                var a = new AlertPopup
                {
                    TitlePopup = title,
                    Message = message,
                    PrimaryButtonText = ok,
                    SecondaryButtonText = cancel,
                    YesorNot = true,
                    PrimaryCommand = new Command(async () =>
                    {
                        result = "1";
                        await MopupService.Instance.PopAllAsync();
                    }),
                    SecondaryCommand = new Command(async () =>
                    {
                        result = "2";
                        await MopupService.Instance.PopAllAsync();
                    })
                };
                await MopupService.Instance.PushAsync(a);
                await a.PopupClosedTask;
            }
            catch (Exception) { }

            return result;
        }

        public async Task<bool> DisplayAlertDos(string title, string message, string ok)
        {

            bool result = false;
            try
            {
                if (MopupService.Instance.PopupStack.Count > 0)
                {
                    await MopupService.Instance.PopAllAsync();
                }
                var a = new AlertPopup
                {
                    TitlePopup = title,
                    Message = message,
                    PrimaryButtonText = ok,
                    YesorNot = false,
                    PrimaryCommand = new Command(async () =>
                    {
                        result = true;
                        await MopupService.Instance.PopAllAsync();
                    }),
                };
                await MopupService.Instance.PushAsync(a);
                await a.PopupClosedTask;
            }
            catch (Exception) { }
            return result;
        }

        public async Task PushModalAsync(Page page)
        {
            await MainPage.Navigation.PushModalAsync(page, false);
        }

        private Page MainPage
        {
            get { return Application.Current.MainPage; }
        }
    }
}
