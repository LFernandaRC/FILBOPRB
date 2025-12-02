using EventosCorferias.Models;
using EventosCorferias.ViewModel.Usuario;

namespace EventosCorferias.Views.Usuario;

public partial class AgendaView : ContentPage
{
    private readonly string IdSuceso;
    private readonly string IdConferencista;
    private readonly string NombreConferencista;
    private readonly string IdContenido_;
    private readonly bool EsFavo;
    private readonly bool OrigenPrincipal_;
    private readonly string AuxIdAgendFiltro_;

    public AgendaView(string idSuceso, string idConferencista, string nombreConferncista, bool esFavo, string IdContenido, bool OrPrinci, string AuxIdAgenda)
    {
        try
        {
            EsFavo = esFavo;
            IdSuceso = idSuceso;
            IdContenido_ = IdContenido;
            IdConferencista = idConferencista;
            NombreConferencista = nombreConferncista;

            OrigenPrincipal_ = OrPrinci;
            AuxIdAgendFiltro_ = AuxIdAgenda;

            InitializeComponent();
        }
        catch (Exception ex)
        {
            ClaseBase claseBase = new ClaseBase();
            claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "AgendaView", "AgendaView", "inicio de vista");
        }
    }

    private void EntryBsq_Completed(object sender, EventArgs e)
    {
        filtroTres.Unfocus();
        pickerUno.Unfocus();
        pickerSeis.Unfocus();
        pickerDos.Unfocus();
        pickerSiete.Unfocus();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        BindingContext = new AgendaVm(IdSuceso, IdConferencista, NombreConferencista, EsFavo, IdContenido_, filtroTres, OrigenPrincipal_, AuxIdAgendFiltro_);
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        PickeCalen.Focus();
    }
}