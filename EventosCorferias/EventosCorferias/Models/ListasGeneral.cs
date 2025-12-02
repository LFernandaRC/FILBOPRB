using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EventosCorferias.Models
{
    public class ListasGeneral : INotifyPropertyChanged
    {
        private string? Id_;
        private string? Descripcion_;
        private string? Observacion_;
        private string? Path_;
        private string? TipoIcono_;
        private Color? ColorTexto_;
        private ImageSource? imagen_;

        public Color? ColorTexto
        {
            get { return ColorTexto_; }
            set { ColorTexto_ = value; OnPropertyChanged(nameof(ColorTexto)); }
        }

        public string? Path
        {
            get { return Path_; }
            set { Path_ = value; OnPropertyChanged(nameof(Path)); }
        }

        public string? Id
        {
            get { return Id_; }
            set { Id_ = value; OnPropertyChanged(nameof(Id)); }
        }

        public string? Descripcion
        {
            get { return Descripcion_; }
            set { Descripcion_ = value; OnPropertyChanged(nameof(Descripcion)); }
        }

        public string? Observacion
        {
            get { return Observacion_; }
            set { Observacion_ = value; OnPropertyChanged(nameof(Observacion)); }
        }

        public string? TipoIcono
        {
            get { return TipoIcono_; }
            set { TipoIcono_ = value; OnPropertyChanged(nameof(TipoIcono)); }
        }

        public ImageSource? ImagenIcon
        {
            get { return imagen_; }
            set { imagen_ = value; OnPropertyChanged(nameof(ImagenIcon)); }
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}