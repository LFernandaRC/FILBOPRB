using System.Text;
using System.Diagnostics;

namespace EventosCorferias.Services
{
    class LogicaWs
    {
        /*Enlace marca blanca pruebas*/
        //private static readonly string url = "http://190.147.38.91:8891/CorferiasEventosServ/";

        /*Enlace marca blanca Productivo*/
        private static readonly string url = "https://servicioseventos.corferias.co/CorferiasEventosServ/";

        //Imagenes transversales de uso en la app
        public string ImgMenuSuperior_Mtd()
        {
            return "icon.png";
        }

        private HttpClient? ClientGet;
        private HttpClient? ClientPost;


        private readonly string Movile_Consulta_Login = "validausuarioaplicacion";
        private readonly string Movile_Insert_Registro = "insertarusuarioApp";
        private readonly string Movile_Update_Token = "actualizatoken";
        private readonly string Movile_select_Pais = "consultarpaises";
        private readonly string Movile_select_Idioma = "consultaridioma";
        private readonly string Movile_select_CiudadRecinto = "consultaciudadporrecinto";
        private readonly string Movile_select_TipoId = "consultatipoid";
        private readonly string Movile_select_Profesion = "consultaprofesion";
        private readonly string Movile_select_SectoEcono = "consultasectoreconomico";
        private readonly string Movile_select_CiudadByPais = "consultaciudadporpais";
        private readonly string Movile_select_Interes_Usuario = "consultarinteresusuario";
        private readonly string Movile_Delete_Interes_Usuario = "actualizarinteresusuario";
        private readonly string Movile_insert_Interes = "insertarinteresusuario";
        private readonly string Movile_select_Perfil = "consultausuario";
        private readonly string Movile_Splash = "splashpublicado";
        private readonly string Movile_select_RedSocialUsu = "consultaredsocialusuario";
        private readonly string Movile_Delet_RedSocialUsu = "eliminaredsocialusuario";
        private readonly string Movile_Update_Usuario = "actualizausuario";
        private readonly string AvalidarUsuarioExistente = "AvalidarUsuarioExistente";
        private readonly string Movile_Generar_Codigo = "send";
        private readonly string Movile_Cambio_Clave = "/ActualizarContrasena";
        private readonly string Movile_Subir_Imagen = "/uploadFileMovile/";

        private readonly string Movile_select_Suceso = "consultacalendario";
        private readonly string Movile_Select_Red_Suceso = "consultaredes";
        private readonly string Movile_Update_Fav_Suceso = "insertarusuariosuceso";
        private readonly string Movile_delet_Fav_Suceso = "eliminarusuariosuceso";
        private readonly string Movile_Select_Categoria_succeso = "consultacategoria";
        private readonly string Movile_Select_Recinto_Suceso = "consultarecinto";
        private readonly string Movile_Select_meses = "consultameses";

        private readonly string Movile_Select_DetalleSuseco = "consultadetallesuceso";
        private readonly string Movile_Select_DetalleSusecoCarusel = "consultaimagensuceso";
        private readonly string Movile_Select_DetalleSucesoModulos = "consultasucesocarrusel";
        private readonly string Movile_Select_DetalleSucesoRedRecinto = "consultaredesdetallesuceso";
        private readonly string Movile_Select_DetalleSucesoPataLogos = "consultapatalogosuceso";

        private readonly string Movile_Delet_NewsUsuario = "eliminarNewsUsuario";
        private readonly string Movile_Update_NewsUsuario = "insertarNewsUsuario";

        private readonly string Movile_select_Confenrencistas = "consultaconferencista";
        private readonly string Movile_select_ConfenrencistasNombre = "consultaconferencistaconnombre";
        private readonly string Movile_select_Confenrencistas_Fav = "consultafavoritoconferencista";
        private readonly string Movile_Update_Fav_Confenrencista = "insertarfavoritoconferencista";
        private readonly string Movile_delet_Fav_Confenrencista = "eliminarfavoritoconferencista";
        private readonly string Movile_select_conferencistaDetalle = "consultaconferencistadetalle";
        private readonly string Movile_Select_Red_Conferencista = "consultaredesconferencista";

        private readonly string Movile_Select_Expo_Ubicacion = "consultaexpositorubicacion";
        private readonly string Movile_Select_Expositores = "consultacatalgoexpositor";
        private readonly string Movile_Select_ExpoFavoritos = "consultacatalogoexpositorfav";
        private readonly string Movile_Update_Fav_Expo = "insertarusuarioexpositor";
        private readonly string Movile_Delet_Fav_Expo = "eliminarusuarioexpositor";
        private readonly string Movile_Detalle_Expo = "consultadetallexpositor";
        private readonly string Movile_Redes_Expo = "consultaredesexpositor";

        private readonly string Movile_Select_catalogoProductos = "consultacatalogoproducto";

        private readonly string Movile_Select_servicios_Recinto = "consultarecintopantalla";
        private readonly string Movile_Select_servicios_Lista = "consultaserviciogeneral";
        private readonly string Movile_Select_servicios_Detalle = "consultadetalleservicio";

        private readonly string Movile_Select_ContenidosFavorito = "consultafavoritoscontenidos";
        private readonly string Movile_Select_ContenidosSuceso = "consultacontenidosuceso";
        private readonly string Movile_Select_ContenidosHome = "consultacontenidoPP";
        private readonly string Movile_Select_ContenidosComunidad = "consultacontenidocomunidad";
        private readonly string Movile_Update_Fav_Contenidos = "agregarfavoritocontenido";
        private readonly string Movile_delet_Fav_Contenidos = "eliminarfavoritocontenido";
        private readonly string Movile_select_ContenidosDetalle = "consultadetallecontenidos";

        private readonly string Movile_select_Comunidades = "consultarcomunidad";
        private readonly string Movile_select_Comunidades_Favoritos = "consultacomunidadfavorito";
        private readonly string Movile_select_Comunidades_Lista = "generamultilistacomunidadcontenido";
        private readonly string Movile_select_Comunidades_Lista_Favoritos = "filtrocomunidadfavorito";
        private readonly string Movile_Update_Fav_Comunidades = "insertarusuariocomunidad";
        private readonly string Movile_delet_Fav_Comunidades = "eliminausuariocomunidad";

        private readonly string Movile_Select_Mascara_Agenda = "consultamascarasagendasuceso";
        private readonly string Movile_Select_Opcion_Categoria = "consultaopcionescategoria";
        private readonly string Movile_Select_Opcion_Lugar = "consultaopcioneslugar";
        private readonly string Movile_Select_Opcion_Conferencista = "consultaopcionesconferencista";
        private readonly string Movile_Update_Fav_Agenda = "insertafavoritoagenda";
        private readonly string Movile_Delet_Fav_Agenda = "eliminafavoritoagenda";

        private readonly string Movile_select_Cupones = "consultacuponera";
        private readonly string Movile_select_filtroCategoria = "filtrocategoriaproducto";
        private readonly string Movile_Update_Fav_Cupones = "insertarusuariocuponera";
        private readonly string Movile_delet_Fav_Cupones = "eliminarusuariocuponera";

        private readonly string Movile_Select_Terminos_Politicas = "consultaterminosaplicacion";

        private readonly string Movile_Select_Insert_Evaluacion = "evaluacionsatisfaccion";

        private readonly string Movile_Select_PreRegistroConsultaGeneral = "consutlapreregistroappb1";
        private readonly string Movile_Select_ValidacionPreregistro = "validarpreregistro";
        private readonly string Movile_Insert_CodigoPreRegistro = "insertarpreregistroappb2";
        private readonly string Movile_Select_Insert_PreRegistro = "respuestasnegocio";

        private readonly string Movile_Select_Lista_Recintos = "consultacomollegarpopupb1";
        private readonly string Movile_Select_Recintos_C = "consultacomollegarb2";


        private readonly string Movile_Select_General_Mapa = "consultageneralmapa";
        private readonly string Movile_Select_Mapa = "consultasitiosmapab1";
        private readonly string Movile_Select_Servicios_Mapa = "consultasitiosmapaserviciosb2";
        private readonly string Movile_Select_Detalle_Mapa = "consultadetallesitiob1";
        private readonly string Movile_Select_Nivel_Sitio = "consultanivelsitio";
        private readonly string Movile_Select_Mapa_Expositor = "consultaexpositoressucesorecintob1";
        private readonly string Movile_Select_Sitio_Expositor = "consultaexpositoressitiob2";
        private readonly string Movile_Select_Retorno_Mapa = "retornoexpositormapab1";
        private readonly string Movile_Select_PinExpositor = "consultaexpositorsitioubicacionapp";

        private readonly string Movile_SelectGeneral_Networking = "interaccionnetworking";

        private readonly string Movile_Select_Lista_TipoContenido_Contactanos = "listatipocontenido";
        private readonly string Movile_Insert_Contactanos = "insertarcontactanosb2";

        private readonly string Movile_Select_Notificaciones = "consultanotificaciones";
        private readonly string Movile_Update_Notificaciones = "actualizanotifiaciones";
        private readonly string Movile_Select_NotificacionesComunidades = "consultanotificacioncomunidad";

        private readonly string Movile_Select_consultaCfiel = "consultaCfiel";
        private readonly string Movile_Insert_Cfiel = "insertacfiel";
        private readonly string Movile_Update_Cfiel = "actualizacfiel";
        private readonly string Movile_Select_Genero = "consultagenero";
        private readonly string Movile_Select_EstadoCivil = "consultaestadocivil";
        private readonly string Movile_Select_NivelAcademico = "consultanivelprofesional";
        private readonly string Movile_Select_Estrato = "consultaestrato";
        private readonly string Movile_Select_Ocupacion = "consultaocupacion";
        private readonly string Movile_Select_Reconocimientos = "consultacfielreconocimiento";
        private readonly string Movile_Select_Facturas = "consultafacturacfiel";
        private readonly string Movile_Select_SucesosCfiel = "consultacfielactivos";
        private readonly string Movile_Select_FiltroSuceso = "consultarsucesocfiel";
        private readonly string Movile_Insert_FacturaCfiel = "insertarfacturascfiel";
        private readonly string Movile_Select_cfieldatosservicio = "rest/cfieldatosservicio";

        private readonly string Movile_Select_BusquedaApp = "busquedaaplicacion";

        private readonly string Movile_Select_Comprar = "consultaboleteriasucesoapp";
        private readonly string Movile_Select_MisBoletas = "consultaconfirmacionboleteriapp";
        private readonly string Movile_Select_Boletas = "consultaboleteriacategoriaapp";
        private readonly string Movile_Select_CategoriaBoletas = "consultalistaactividadesbolapp";
        private readonly string Movile_Select_ConsultarTransaccion = "consultatransaccionboleteriapp";
        private readonly string Movile_Insert_Boleteria = "insertarboleteriacategoriapp";
        private readonly string Movile_validar_boleta = "rest/validaboleteria";
        private readonly string Movile_validar_CodigoPromocional = "consultarcodigopromocionapp";
        private readonly string Movile_delete_boleta = "actualizaestadoboletapp";
        private readonly string Movile_Select_EventoPriniapl = "consAEvento";
        private readonly string Movile_Insert_Logs = "crealog";
        private readonly string Movile_Select_MensajeAux = "consultarayudaconsola";
        private readonly string Movile_Select_preregistro = "rest/preregistroservicio";
        private readonly string Movile_Insert_habeasData = "insertarhabeasdatapp";
        private readonly string Movile_Insert_CfielReconocimientos = "rest/cfielreconocimiento";
        private readonly string Movile_Insert_CfielMisFacturas = "rest/cfielfacturaservicio";
        private readonly string Movile_select_consultachatwh = "consultachatwh";
        private readonly string Insert_Usuario_ios = "modificausuarioid";


        private readonly string Moviel_select_filtroAgenda = "consAMascaraFiltroAgenda";
        private readonly string Moviel_select_botonesMenuInferior = "consABotonesApp";
        private readonly string Moviel_select_CorferiasEventosServ = "consAsitiosEventos";
        private readonly string Moviel_select_consAMascaraFiltroExp = "consAMascaraFiltroExp";
        private readonly string Moviel_select_consultaagendasuceso = "consultaagendasuceso";

        private readonly string Moviel_select_consAExpositorSitioLugar = "consAExpositorSitioLugar";

        private readonly string Movile_Login = "consultaAdministraLogin";

        private readonly string Movile_consultarelacionferiasuceso = "consultarelacionferiasuceso";

        private readonly string Movile_actualizarelacionusuario = "actualizarelacionusuario";

        private readonly string Movile_consultaAdministraIconos = "consultaAdministraIconos";

        /*--------------------------------------------------------------------------------------------------------------------------------------------------*/

        public string Movile_actualizarelacionusuario_Mtd(string Bandera)
        {
            return url + "/" + Movile_actualizarelacionusuario + "/" + Bandera;
        }

        public string Movile_consultaAdministraIconos_Mtd(string Bandera, string IdModulo, string IdSuceso)
        {
            return url + "/" + Movile_consultaAdministraIconos + "/" + Bandera + "/" + IdModulo + "/" + IdSuceso;
        }

        public string Movile_consultarelacionferiasuceso_Mtd(string Bandera, string IdSuceso)
        {
            return url + "/" + Movile_consultarelacionferiasuceso + "/" + Bandera + "/" + IdSuceso;
        }


        //{bandera}/{IdExpositor}/{IdSucesoRecinto}/{IdSitio}
        public string Moviel_select_consAExpositorSitioLugar_Mtd(string bandera, string IdExpositor, string IdSucesoRecinto, string IdSitio)
        {
            return url + "/" + Moviel_select_consAExpositorSitioLugar + "/" + bandera + "/" + IdExpositor + "/" + IdSucesoRecinto + "/" + IdSitio;
        }

        public string Moviel_select_consultaagendasuceso_Mtd(string IdCiudad, string bandera, string correo, string IdSuceso, string Idioma, string idConf, string IdContenido)
        {
            return url + "/" + Moviel_select_consultaagendasuceso + "/" + IdCiudad + "/" + bandera + "/" + correo + "/" + IdSuceso + "/" + Idioma + "/" + idConf + "/" + IdContenido;
        }

        public string Moviel_select_consAMascaraFiltroExp_Mtd(string idLabel, string IdSuceso)
        {
            return url + "/" + Moviel_select_consAMascaraFiltroExp + "/" + idLabel + "/" + IdSuceso;
        }

        //PARAMETROS=/{bandera}/{IdRecintoSuceso}/{IdOrigen}/{IdModulo}/{Idioma}
        public string Moviel_select_CorferiasEventosServ_Mtd(string Bandera, string IdRecintoSuceso, string IdOrigen, string IdModulo, string Idioma)
        {
            return url + "/" + Moviel_select_CorferiasEventosServ + "/" + Bandera + "/" + IdRecintoSuceso + "/" + IdOrigen + "/" + IdModulo + "/" + Idioma;
        }


        public string Moviel_select_botonesMenuInferior_Mtd(string Bandera, string IdSuceso)
        {
            return url + "/" + Moviel_select_botonesMenuInferior + "/" + Bandera + "/" + IdSuceso;
        }

        public string Moviel_select_filtroAgenda_Mtd(string IdLabel, string IdSuceso)
        {
            return url + "/" + Moviel_select_filtroAgenda + "/" + IdLabel + "/" + IdSuceso;
        }


        /* Consulra el evento actual que va a mostrar la app*/ //{bandera}/{Correo}/{Idioma}
        public string Movile_Select_EventoPriniapl_Mtd(string bandera, string correo, string Idioma, string IdApp)
        {
            return url + "/" + Movile_Select_EventoPriniapl + "/" + bandera + "/" + correo + "/" + Idioma + "/" + IdApp;
        }
        /*--------------------------------------------------------------------------------------------------------------------------------------------------*/

        /* insertar usuaro ios nuevo desde acutalizar perfil*/
        public string Insert_Usuario_ios_Mtd(string bandera, string correo)
        {
            return url + "/" + Insert_Usuario_ios + "/" + bandera + "/" + correo;
        }

        /*--------------------------------------------------------------------------------------------------------------------------------------------------*/
        /* Inicio Metodos para Login */
        public string Movile_Consulta_Login_Mtd(string correo)
        {
            return url + "/" + Movile_Consulta_Login + "/" + correo;
        }

        public string Movile_Login_Mtd(string Bandera, string IdLogin, string IdSuceso)
        {
            return url + "/" + Movile_Login + "/" + Bandera + "/" + IdLogin + "/" + IdSuceso;
        }


        /*--------------------------------------------------------------------------------------------------------------------------------------------------*/
        /* Inicio Metodos Para registro */
        public string Movile_Insert_Registro_Mtd(string banderaEmpresa)
        {
            return url + "/" + Movile_Insert_Registro + "/" +
                    banderaEmpresa;
        }

        public string Movile_Update_Token_Mtd(string correo, string token, string IDaPP)
        {
            return url + "/" + Movile_Update_Token + "/" + correo + "/" + token + "/" + IDaPP;
        }

        public string Movile_select_Pais_Mtd()
        {
            return url + "/" + Movile_select_Pais;
        }

        public string Movile_select_Idioma_Mtd()
        {
            return url + "/" + Movile_select_Idioma;
        }

        public string Movile_select_CiudadRecinto_Mtd()
        {
            return url + "/" + Movile_select_CiudadRecinto;
        }

        public string Movile_select_TipoId_Mtd()
        {
            return url + "/" + Movile_select_TipoId;
        }

        public string Movile_select_Profesion_Mtd()
        {
            return url + "/" + Movile_select_Profesion;
        }

        public string Movile_select_SectoEcono_Mtd()
        {
            return url + "/" + Movile_select_SectoEcono;
        }

        public string Movile_select_CiudadByPais_Mtd(string IdPais)
        {
            return url + "/" + Movile_select_CiudadByPais + "/" + IdPais;
        }


        /*--------------------------------------------------------------------------------------------------------------------------------------------------*/
        /* Incio metodo para Interes*/

        public string Movile_select_Interes_Usuario_Mtd(string bandera, string correo, string idioma, string IdInteres, string Estado, string idusuario)
        {
            return url + "/" + Movile_select_Interes_Usuario + "/" + bandera + "/" + correo + "/" + idioma + "/" + IdInteres + "/" + Estado + "/" + idusuario;
        }

        public string MMovile_insert_Interes_Mtd(string bandera, string correo, string interes, string idioma, string Estado, string idusuario)
        {
            return url + "/" + Movile_insert_Interes
                + "/" + bandera + "/" + correo + "/" + interes + "/" + idioma + "/" + Estado + "/" + idusuario;
        }

        public string MMovile_Delete_Interes_Usuario_Mtd(string bandera, string correo, string interes, string idioma, string idusuario)
        {
            return url + "/" + Movile_Delete_Interes_Usuario
                + "/" + bandera + "/" + correo + "/" + interes + "/" + idioma + "/" + idusuario;
        }
        /*--------------------------------------------------------------------------------------------------------------------------------------------------*/
        /* Incio metodo para Splash*/
        public string Movile_Splash_Mtd(string idFeria)
        {
            return url + "/" + Movile_Splash + "/" + idFeria;
        }
        /*--------------------------------------------------------------------------------------------------------------------------------------------------*/

        public string Movile_Generar_Codigo_Mtd()
        {
            return url + "/" + Movile_Generar_Codigo;
        }


        /*--------------------------------------------------------------------------------------------------------------------------------------------------*/
        /*Metodos para editar el perfil */

        public string Movile_select_RedSocialUsu_Mtd(string correo)
        {
            return url + "/" + Movile_select_RedSocialUsu + "/" + correo;
        }

        public string Movile_Delet_RedSocialUsu_Mtd(string correo, string bandera, string idRed)
        {
            return url + "/" + Movile_Delet_RedSocialUsu + "/" + correo + "/" + bandera + "/" + idRed;
        }

        public string Movile_Update_Usuario_Mtd(string correo)
        {
            return url + "/" + Movile_Update_Usuario + "/" + correo;
        }

        public string Movile_select_Perfil_Mtd(string correo)
        {
            return url + "/" + Movile_select_Perfil + "/" + correo;
        }

        public string Movile_Cambio_Clave_Mtd(string correo)
        {
            return url + "/" + Movile_Cambio_Clave + "/" + correo;
        }


        /*---------------------------------------------------------------------------------------------------------------------------------------*/

        public string Movile_AvalidarUsuarioExistente_Mtd(string correo, string redsocial)
        {
            return url + "/" + AvalidarUsuarioExistente + "/" + correo + "/" + redsocial;
        }

        /*---------------------------------------------------------------------------------------------------------------------------------------*/
        /*Incio metodos para sucesos */
        public string Movile_select_Suceso_Mtd(string buscar, string Idciudad, string bandera, string idSuceso, string idModulo, string mes, string idRecinto, string idCategoria, string correo, string idioma, string ano)
        {
            return url + "/" + Movile_select_Suceso + "/" + buscar + "/" + Idciudad + "/" + bandera + "/" + idSuceso + "/" + idModulo + "/" + mes + "/" + idRecinto + "/" + idCategoria + "/" + correo + "/" + idioma + "/" + ano;
        }

        public string Movile_Select_Red_Suceso_Mtd(string idSuceso)
        {
            return url + "/" + Movile_Select_Red_Suceso + "/" + idSuceso;
        }

        public string Movile_Update_Fav_Suceso_Mtd(string idSuceso, string Idfav, string correo)
        {
            return url + "/" + Movile_Update_Fav_Suceso + "/" + Idfav + "/" + correo + "/" + idSuceso;
        }

        public string Movile_delet_Fav_Suceso_Mtd(string idSuceso, string Idfav, string correo)
        {
            return url + "/" + Movile_delet_Fav_Suceso + "/" + Idfav + "/" + idSuceso + "/" + correo;
        }

        public string Movile_Select_Categoria_succeso_Mtd(string idioma)
        {
            return url + "/" + Movile_Select_Categoria_succeso + "/" + idioma;
        }

        public string Movile_Select_meses_Mtd()
        {
            return url + "/" + Movile_Select_meses;
        }

        public string Movile_Select_Recinto_Suceso_Mtd(string idCiudad, string idioma)
        {
            return url + "/" + Movile_Select_Recinto_Suceso + "/" + idCiudad + "/" + idioma;
        }

        /*---------------------------------------------------------------------------------------------------------------------------------------*/
        /*Inicio metodos para detalle suceso */


        public string Movile_Select_DetalleSuseco_Mtd(string idSuceso, string idioma, string correo)
        {
            return url + "/" + Movile_Select_DetalleSuseco + "/1/" + idSuceso + "/" + idioma + "/" + correo;
        }

        public string Movile_Select_DetalleSusecoCarusel_Mtd(string idSuceso)
        {
            return url + "/" + Movile_Select_DetalleSusecoCarusel + "/" + idSuceso;
        }

        public string Movile_Select_DetalleSucesoModulosl_Mtd(string idioma, string correo, string idSuceso)
        {
            return url + "/" + Movile_Select_DetalleSucesoModulos + "/" + idioma + "/" + correo + "/" + idSuceso;
        }

        public string Movile_Select_DetalleSucesoRedRecinto_Mtd(string idRecinto)
        {
            return url + "/" + Movile_Select_DetalleSucesoRedRecinto + "/" + idRecinto;
        }

        public string Movile_Delet_NewsUsuario_Mtd(string correo, string idSuceso)
        {
            return url + "/" + Movile_Delet_NewsUsuario + "/6/" + correo + "/" + idSuceso;
        }

        public string Movile_Update_NewsUsuario_Mtd(string correo, string idSuceso)
        {
            return url + "/" + Movile_Update_NewsUsuario + "/5/" + correo + "/" + idSuceso;
        }

        public string Movile_Select_DetalleSucesoPataLogos_Mtd(string idioma, string correo, string idSuceso)
        {
            return url + "/" + Movile_Select_DetalleSucesoPataLogos + "/" + idioma + "/" + correo + "/" + idSuceso;
        }

        /*---------------------------------------------------------------------------------------------------------------------------------------*/
        /*Inico metodos para conferencista */

        public string Movile_select_Confenrencistas_Mtd(string correo, string idioma, string idSuceso)
        {
            return url + "/" + Movile_select_Confenrencistas + "/" + correo + "/" + idioma + "/" + idSuceso;
        }

        public string Movile_select_ConfenrencistasNombre_Mtd(string correo, string idioma, string idSuceso)
        {
            return url + "/" + Movile_select_ConfenrencistasNombre + "/" + correo + "/" + idioma + "/" + idSuceso;
        }


        public string Movile_select_Confenrencistas_Fav_Mtd(string NombreConf, string IdCiudad, string bandera, string IdConf, string correo, string IdSuceso, string Idioma)
        {
            return url + "/" + Movile_select_Confenrencistas_Fav + "/" + NombreConf + "/" + IdCiudad + "/" + bandera + "/" + IdConf + "/" + correo + "/" + IdSuceso + "/" + Idioma;
        }

        public string Movile_Update_Fav_Confenrencista_Mtd(string correo, string idConferencista)
        {
            return url + "/" + Movile_Update_Fav_Confenrencista + "/" + correo + "/" + idConferencista;
        }

        public string Movile_delet_Fav_Confenrencista_Mtd(string correo, string idConferencista)
        {
            return url + "/" + Movile_delet_Fav_Confenrencista + "/" + correo + "/" + idConferencista;
        }
        public string Movile_select_conferencistaDetalle_Mtd(string correo, string idioma, string idSuceso, string idConferencista)
        {
            return url + "/" + Movile_select_conferencistaDetalle + "/" + correo + "/" + idioma + "/" + idSuceso + "/" + idConferencista;
        }

        public string Movile_Select_Red_Conferencista_Mtd(string idConferencista)
        {
            return url + "/" + Movile_Select_Red_Conferencista + "/" + idConferencista;
        }


        /*---------------------------------------------------------------------------------------------------------------------------------------*/
        /*expositores */

        public string Movile_Detalle_Expo_Mtd(string idioma, string correo, string idExpositor, string idSuceso)
        {
            return url + "/" + Movile_Detalle_Expo + "/" + idioma + "/" + correo + "/" + idExpositor + "/" + idSuceso;
        }
        public string Movile_Redes_Expo_Mtd(string idExpositor)
        {
            return url + "/" + Movile_Redes_Expo + "/" + idExpositor;
        }

        public string Movile_Select_Expo_Ubicacion_Mtd(string idSuceso, string correo, string idioma)
        {
            return url + "/" + Movile_Select_Expo_Ubicacion + "/" + idSuceso + "/" + correo + "/" + idioma;
        }

        public string Movile_Select_Expositores_Mtd(string idioma, string correo, string idSuceso, string nombreExpositor, string idSitio)
        {
            return url + "/" + Movile_Select_Expositores + "/" + idioma + "/" + correo + "/" + idSuceso + "/" + nombreExpositor + "/" + idSitio;
        }

        public string Movile_Select_ExpoFavoritos_Mtd(string Idioma, string correo, string IdExpositor, string IdSuceso, string NombreExpositor, string IdSitio)
        {
            return url + "/" + Movile_Select_ExpoFavoritos + "/" + Idioma + "/" + correo + "/" + IdExpositor + "/" + IdSuceso + "/" + NombreExpositor + "/" + IdSitio;
        }

        public string Movile_Update_Fav_Expo_Mtd(string idioma, string correo, string idExpositor)
        {
            return url + "/" + Movile_Update_Fav_Expo + "/" + idioma + "/" + correo + "/" + idExpositor;
        }

        public string Movile_Delet_Fav_Expo_Mtd(string idioma, string correo, string idExpositor)
        {
            return url + "/" + Movile_Delet_Fav_Expo + "/" + idioma + "/" + correo + "/" + idExpositor;
        }

        /*--------------------------------------------------------------------------------------------------------------------------------------------------*/
        /*Producto */

        public string Movile_Select_catalogoProductos_Mtd(string idioma, string idExpositro, string nombreProducto)
        {
            return url + "/" + Movile_Select_catalogoProductos + "/" + idioma + "/" + idExpositro + "/" + nombreProducto;
        }

        /*--------------------------------------------------------------------------------------------------------------------------------------------------*/
        /*servicios */

        public string Movile_Select_servicios_Recinto_Mtd(string idCiudad, string correo, string idioma)
        {
            return url + "/" + Movile_Select_servicios_Recinto + "/" + idCiudad + "/" + correo + "/" + idioma;
        }

        public string Movile_Select_servicios_Lista_Mtd(string correo, string idRecinto, string idioma, string bandera)
        {
            return url + "/" + Movile_Select_servicios_Lista + "/" + correo + "/" + idRecinto + "/" + idioma + "/" + bandera;
        }

        public string Movile_Select_servicios_Detalle_Mtd(string correo, string idRecinto, string idioma, string idServicio, string bandera)
        {
            return url + "/" + Movile_Select_servicios_Detalle + "/" + correo + "/" + idRecinto + "/" + idioma + "/" + idServicio + "/" + bandera;
        }

        /*--------------------------------------------------------------------------------------------------------------------------------------------------*/
        /*Contenidos */
        public string Movile_Select_ContenidosFavotito_Mtd(string idioma, string tituloContenido, string categoria, string correo, string idSuceso)
        {
            return url + "/" + Movile_Select_ContenidosFavorito + "/" + correo + "/" + idioma + "/" + tituloContenido + "/" + categoria + "/" + idSuceso;
        }

        public string Movile_Select_ContenidosSuceso_Mtd(string Bandera, string idioma, string correo, string tituloContenido, string idSuceso)
        {
            return url + "/" + Movile_Select_ContenidosSuceso + "/" + Bandera + "/" + idioma + "/" + correo + "/" + tituloContenido + "/" + idSuceso;
        }

        public string Movile_Select_ContenidosHome_Mtd(string idioma, string correo, string interses, string idCiudad)
        {
            return url + "/" + Movile_Select_ContenidosHome + "/" + idioma + "/" + correo + "/" + interses + "/" + idCiudad;
        }

        public string Movile_Select_ContenidosComunidad_Mtd(string idioma, string correo, string tituloContenido, string idCategoria)
        {
            return url + "/" + Movile_Select_ContenidosComunidad + "/" + idioma + "/" + correo + "/" + tituloContenido + "/" + idCategoria;
        }

        public string Movile_Update_Fav_Contenidos_Mtd(string correo, string idContendido)
        {
            return url + "/" + Movile_Update_Fav_Contenidos + "/" + correo + "/" + idContendido;
        }

        public string Movile_delet_Fav_Contenidos_Mtd(string correo, string idContendido)
        {
            return url + "/" + Movile_delet_Fav_Contenidos + "/" + correo + "/" + idContendido;
        }
        public string Movile_select_ContenidosDetalle_Mtd(string idioma, string idContendido, string correo)
        {
            return url + "/" + Movile_select_ContenidosDetalle + "/" + idioma + "/" + idContendido + "/" + correo;
        }


        /*--------------------------------------------------------------------------------------------------------------------------------------------------*/
        /*Agenda */

        public string Movile_Select_Mascara_Agenda_Mtd(string idSuceso, string idioma)
        {
            return url + "/" + Movile_Select_Mascara_Agenda + "/" + idSuceso + "/" + idioma;
        }
        public string Movile_Select_Opcion_Categoria_Mtd(string idSuceso, string correo, string idioma)
        {
            return url + "/" + Movile_Select_Opcion_Categoria + "/" + idSuceso + "/" + correo + "/" + idioma;
        }

        public string Movile_consAFiltroFranja_Mtd(string bandera, string idSuceso, string IdAgenda)
        {
            return url + "/" + "consAFiltroFranja" + "/" + bandera + "/" + idSuceso + "/" + IdAgenda;
        }
        public string Movile_Select_Opcion_Lugar_Mtd(string idSuceso, string correo, string idioma)
        {
            return url + "/" + Movile_Select_Opcion_Lugar + "/" + idSuceso + "/" + correo + "/" + idioma;
        }
        public string Movile_Select_Opcion_Conferencista_Mtd(string idSuceso)
        {
            return url + "/" + Movile_Select_Opcion_Conferencista + "/" + idSuceso;
        }

        public string Movile_modAEliminaUsuario(string Bandera)
        {
            return url + "/" + "modAEliminaUsuario" + "/" + Bandera;
        }

        public string Movile_Update_Fav_Agenda_Mtd(string correo, string idAgenda, string IdContenido)
        {
            return url + "/" + Movile_Update_Fav_Agenda + "/" + correo + "/" + idAgenda + "/" + IdContenido;
        }

        public string Movile_Delet_Fav_Agenda_Mtd(string correo, string idAgenda, string IdContenido)
        {
            return url + "/" + Movile_Delet_Fav_Agenda + "/" + correo + "/" + idAgenda + "/" + IdContenido;
        }
        public string Movile_select_ContenidosDetalle_Mtd(string suceso, string agenda, string idioma, string correo)
        {
            return url + "/" + Movile_select_ContenidosDetalle + "/" + suceso + "/" + agenda + "/" + idioma + "/" + correo;
        }

        /*--------------------------------------------------------------------------------------------------------------------------------------------------*/
        /*Comunidades */

        public string Movile_select_Comunidades_Mtd(string correo, string idioma, string idComunidad)
        {
            return url + "/" + Movile_select_Comunidades + "/" + correo + "/" + idioma + "/" + idComunidad;
        }

        public string Movile_select_Comunidades_Lista_Mtd(string idioma, string intereses)
        {
            return url + "/" + Movile_select_Comunidades_Lista + "/" + idioma + "/" + intereses;
        }

        public string Movile_Update_Fav_Comunidades_Mtd(string correo, string idComunidad)
        {
            return url + "/" + Movile_Update_Fav_Comunidades + "/" + correo + "/" + idComunidad;
        }

        public string Movile_delet_Fav_Comunidades_Mtd(string correo, string idComunidad)
        {
            return url + "/" + Movile_delet_Fav_Comunidades + "/" + correo + "/" + idComunidad;
        }

        public string Movile_select_Comunidades_Favoritos_Mtd(string correo, string idioma, string idComunidad)
        {
            return url + "/" + Movile_select_Comunidades_Favoritos + "/" + correo + "/" + idioma + "/" + idComunidad;
        }

        public string Movile_select_Comunidades_Lista_Favoritos_Mtd(string correo, string idioma)
        {
            return url + "/" + Movile_select_Comunidades_Lista_Favoritos + "/" + correo + "/" + idioma;
        }

        /*--------------------------------------------------------------------------------------------------------------------------------------------------*/
        /*Cuponera */

        public string Movile_select_Cupones_Mtd(string Bandera, string Idioma, string Correo, string IdCupon, string IdExpositor, string NombreCategoria, string NombreExpositor, string IdCategoria, string NombreSuceso, string IdSuceso)
        {
            return url + "/" + Movile_select_Cupones + "/" + Bandera + "/" + Idioma + "/" + Correo + "/" + IdCupon + "/" + IdExpositor + "/" + NombreCategoria + "/" + NombreExpositor + "/" + IdCategoria + "/" + NombreSuceso + "/" + IdSuceso;
        }

        public string Movile_select_filtroCategoria_Mtd(string idioma, string idExpositor)
        {
            return url + "/" + Movile_select_filtroCategoria + "/" + idioma + "/" + idExpositor;
        }

        public string Movile_Update_Fav_Cupones_Mtd(string correo, string idCupon)
        {
            return url + "/" + Movile_Update_Fav_Cupones + "/" + correo + "/" + idCupon;
        }

        public string Movile_delet_Fav_Cupones_Mtd(string correo, string idCupon)
        {
            return url + "/" + Movile_delet_Fav_Cupones + "/" + correo + "/" + idCupon;
        }
        /*--------------------------------------------------------------------------------------------------------------------------------------------------*/

        /*Terminos y Politicas */

        public string Movile_Select_Terminos_Politicas_Mtd(string Bandera, string Modulo, string Idioma)
        {
            return url + "/" + Movile_Select_Terminos_Politicas + "/" + Bandera + "/" + Modulo + "/" + Idioma;
        }
        /*--------------------------------------------------------------------------------------------------------------------------------------------------*/

        /*Evaluacion satisfaccion */

        public string Movile_Select_Insert_Evaluacion_Mtd(string bandera, string correo, string idioma, string idevaluacion, string ubicacion)
        {
            return url + "/" + Movile_Select_Insert_Evaluacion + "/" + bandera + "/" + correo + "/" + idioma + "/" + idevaluacion + "/" + ubicacion;
        }

        /*--------------------------------------------------------------------------------------------------------------------------------------------------*/
        /*Pre-Registro */

        public string Movile_Select_PreRegistroConsultaGeneral_Mtd(string correo, string NombreSuceso, string Vigencia, string IdSuceso, string Idioma)
        {
            return url + "/" + Movile_Select_PreRegistroConsultaGeneral + "/" + correo + "/" + NombreSuceso + "/" + Vigencia + "/" + IdSuceso + "/" + Idioma;
        }

        public string Movile_Insert_CodigoPreRegistro_Mtd(string IdSuceso, string correo, string CodigoInv, string TieneInv, string Estado) //
        {
            return url + "/" + Movile_Insert_CodigoPreRegistro + "/" + IdSuceso + "/" + correo + "/" + CodigoInv + "/" + TieneInv + "/" + Estado;
        }

        public string Movile_Select_ValidacionPreregistro_Mtd(string correo, string bandera)
        {
            return url + "/" + Movile_Select_ValidacionPreregistro + "/" + correo + "/" + bandera;
        }

        public string Movile_Select_Insert_PreRegistro_Mtd(string bandera, string correo, string idioma, string idSuceso, string respuesta)
        {
            return url + "/" + Movile_Select_Insert_PreRegistro + "/" + bandera + "/" + correo + "/" + idioma + "/" + idSuceso + "/" + respuesta;
        }

        /*--------------------------------------------------------------------------------------------------------------------------------------------------*/

        /*Como Llegar */

        public string Movile_Select_Lista_Recintos_Mtd()
        {
            return url + "/" + Movile_Select_Lista_Recintos;
        }

        public string Movile_Select_Recintos_C_Mtd(string IdRecinto)
        {
            return url + "/" + Movile_Select_Recintos_C + "/" + IdRecinto;
        }

        /*--------------------------------------------------------------------------------------------------------------------------------------------------*/

        /*Mapa */

        //   PARAMETROS=/{bandera}/{Correo}/{Idioma}/{IdRecintoSuceso}/{IdOrigen}/{IdModulo}
        public string Movile_Select_General_Mapa_Mtd(string bandera, string Correo, string codigoidioma, string id /*recinto o suceso*/, string origen, string IdModulo)
        {
            return url + "/" + Movile_Select_General_Mapa + "/" + bandera + "/" + Correo + "/" + codigoidioma + "/" + id + "/" + origen + "/" + IdModulo;
        }

        public string Movile_Select_Mapa_Mtd(string id /*recinto o suceso*/, string origen, string idioma)
        {
            return url + "/" + Movile_Select_Mapa + "/" + id + "/" + origen + "/" + idioma;
        }
        public string Movile_Select_Servicios_Mapa_Mtd(string id /*recinto o suceso*/, string origen, string idioma)
        {
            return url + "/" + Movile_Select_Servicios_Mapa + "/" + id + "/" + origen + "/" + idioma;
        }
        ///{bandera}/{IdRecintoSuceso}/{IdOrigen}/{Idioma}
        public string Movile_Select_consAServiciosEvento_Mtd(string bandera, string IdRecintoSuceso, string IdOrigen, string idioma, string idModulo)
        {
            return url + "/" + "consAServiciosEvento" + "/" + bandera + "/" + IdRecintoSuceso + "/" + IdOrigen + "/" + idioma + "/" + idModulo;
        }


        public string Movile_Select_Detalle_Mapa_Mtd(string idsitio, string idioma)
        {
            return url + "/" + Movile_Select_Detalle_Mapa + "/" + idsitio + "/" + idioma;
        }
        public string Movile_Select_Nivel_Sitioa_Mtd(string idsitio, string idioma)
        {
            return url + "/" + Movile_Select_Nivel_Sitio + "/1/" + idsitio + "/" + idioma;
        }

        public string Movile_Select_Mapa_Expositor_Mtd(string codigoidioma, string correo, string nombreexpositor, string idsucesorecinto)
        {
            return url + "/" + Movile_Select_Mapa_Expositor + "/" + codigoidioma + "/" + correo + "/" + nombreexpositor + "/" + idsucesorecinto;
        }

        public string Movile_Select_Sitio_Expositor_Mtd(string codigoidioma, string correo, string nombreexpositor, string idsitio)
        {
            return url + "/" + Movile_Select_Sitio_Expositor + "/" + codigoidioma + "/" + correo + "/" + nombreexpositor + "/" + idsitio;
        }

        public string Movile_Select_Retorno_Mapa_Mtd(string codigoidioma, string correo, string idsucesorecinto, string idexpositor)
        {
            return url + "/" + Movile_Select_Retorno_Mapa + "/" + codigoidioma + "/" + correo + "/" + idsucesorecinto + "/" + idexpositor;
        }

        ///consultaexpositorsitioubicacionapp/es/yeyadot@gmail.com/13/0/4/1/0
        public string Movile_Select_PinExpositor_Mtd(string codigoidioma, string correo, string idExpositor, string jumm, string idsuceso, string bandera, string jummm)
        {
            return url + "/" + Movile_Select_PinExpositor + "/" + codigoidioma + "/" + correo + "/" + idExpositor + "/" + jumm + "/" + idsuceso + "/" + bandera + "/" + jummm;
        }

        /*--------------------------------------------------------------------------------------------------------------------------------------------------*/
        /*Networking */

        public string Movile_SelectGeneral_Networking_Mtd(string bandera, string correo, string idioma, string idsuceso, string empresa, string rueda, string fecha)
        {
            return url + "/" + Movile_SelectGeneral_Networking + "/" + bandera + "/" + correo + "/" + idioma + "/" + idsuceso + "/" + empresa + "/" + rueda + "/" + fecha;
        }

        /*--------------------------------------------------------------------------------------------------------------------------------------------------*/
        /*Contactanos */

        public string Movile_Select_Lista_TipoContenido_Contactanos_Mtd()
        {
            return url + "/" + Movile_Select_Lista_TipoContenido_Contactanos;
        }

        public string Movile_Insert_Contactanos_Mtd(string correo)
        {
            return url + "/" + Movile_Insert_Contactanos + "/" + correo;
        }

        /*--------------------------------------------------------------------------------------------------------------------------------------------------*/

        /*Notificaciones */

        public string Movile_Select_Notificaciones_Mtd(string correo, string idioma, string idSuceso)
        {
            return url + "/" + Movile_Select_Notificaciones + "/" + correo + "/" + idioma + "/" + idSuceso;
        }

        public string Movile_Update_Notificaciones_Mtd(string bandera, string correo, string Idioma, string Estado, string IdNotificacion)
        {
            return url + "/" + Movile_Update_Notificaciones + "/" + bandera + "/" + correo + "/" + Idioma + "/" + IdNotificacion + "/" + Estado;
        }

        public string Movile_Select_NotificacionesComunidades_Mtd(string bandera, string correo, string Idioma, string IdComunidad)
        {
            return url + "/" + Movile_Select_NotificacionesComunidades + "/" + bandera + "/" + correo + "/" + Idioma + "/" + IdComunidad;
        }


        /*--------------------------------------------------------------------------------------------------------------------------------------------------*/

        /*CFiel */

        public string Movile_Select_consultaCfiel_Mtd(string correo)
        {
            return url + "/" + Movile_Select_consultaCfiel + "/" + correo;
        }

        public string Movile_Insert_Cfiel_Mtd(string correo, string idGenero, string idEstadoC, string idNivelProfesiona, string idPais, string idCiudad, string barrio, string Localildad, string idEstrato, string idOcupacion)
        {
            return url + "/" + Movile_Insert_Cfiel + "/" + correo + "/" + idGenero + "/" + idEstadoC + "/" + idNivelProfesiona + "/" + idPais + "/" + idCiudad + "/" + barrio + "/" + Localildad + "/" + idEstrato + "/" + idOcupacion;
        }

        public string Movile_Update_Cfiel_Mtd(string correo, string idGenero, string idEstadoC, string idNivelProfesiona, string idPais, string idCiudad, string barrio, string Localildad, string idEstrato, string idOcupacion)
        {
            return url + "/" + Movile_Update_Cfiel + "/" + correo + "/" + idGenero + "/" + idEstadoC + "/" + idNivelProfesiona + "/" + idPais + "/" + idCiudad + "/" + barrio + "/" + Localildad + "/" + idEstrato + "/" + idOcupacion;
        }

        public string Movile_Select_Genero_Mtd()
        {
            return url + "/" + Movile_Select_Genero;
        }

        public string Movile_Select_EstadoCivil_Mtd()
        {
            return url + "/" + Movile_Select_EstadoCivil;
        }

        public string Movile_Select_NivelAcademico_Mtd()
        {
            return url + "/" + Movile_Select_NivelAcademico;
        }

        public string Movile_Select_Estrato_Mtd()
        {
            return url + "/" + Movile_Select_Estrato;
        }

        public string Movile_Select_Ocupacion_Mtd()
        {
            return url + "/" + Movile_Select_Ocupacion;
        }

        public string Movile_Select_Reconocimientos_Mtd(string correo, string idioma)
        {
            return url + "/" + Movile_Select_Reconocimientos + "/" + correo + "/" + idioma;
        }

        public string Movile_Select_Facturas_Mtd(string correo, string idioma, string idSuceso, string Fecha, string ano, string Expositor)
        {
            return url + "/" + Movile_Select_Facturas + "/" + correo + "/" + idioma + "/" + idSuceso + "/" + Fecha + "/" + ano + "/" + Expositor;
        }

        public string Movile_Select_SucesosCfiel_Mtd(string correo, string idioma, string idSuceso)
        {
            return url + "/" + Movile_Select_SucesosCfiel + "/" + correo + "/" + idioma + "/" + idSuceso;
        }

        public string Movile_Select_FiltroSuceso_Mtd(string Bandera, string Vigencia, string Idioma)
        {
            return url + "/" + Movile_Select_FiltroSuceso + "/" + Bandera + "/" + Vigencia + "/" + Idioma;
        }

        public string Movile_Insert_FacturaCfiel_Mtd(string correo, string Idioma, string IdSuceso, string IdExpositor)
        {
            return url + "/" + Movile_Insert_FacturaCfiel + "/" + correo + "/" + Idioma + "/" + IdSuceso + "/" + IdExpositor;
        }

        public string Movile_Insert_CfielReconocimientos_Mtd(string tipoDocumento, string NoDocumeto)
        {
            if (tipoDocumento.Equals("1") || tipoDocumento.Equals("2") || tipoDocumento.Equals("3") || tipoDocumento.Equals("4") || tipoDocumento.Equals("5") ||
               tipoDocumento.Equals("6") || tipoDocumento.Equals("7") || tipoDocumento.Equals("8") || tipoDocumento.Equals("9"))
            {
                tipoDocumento = "0" + tipoDocumento;
            }

            return url + "/" + Movile_Insert_CfielReconocimientos + "/" + tipoDocumento + "/" + NoDocumeto;
        }

        public string Movile_Insert_CfielMisFacturas_Mtd(string tipoDocumento, string NoDocumeto)
        {
            if (tipoDocumento.Equals("1") || tipoDocumento.Equals("2") || tipoDocumento.Equals("3") || tipoDocumento.Equals("4") || tipoDocumento.Equals("5") ||
               tipoDocumento.Equals("6") || tipoDocumento.Equals("7") || tipoDocumento.Equals("8") || tipoDocumento.Equals("9"))
            {
                tipoDocumento = "0" + tipoDocumento;
            }

            return url + "/" + Movile_Insert_CfielMisFacturas + "/" + tipoDocumento + "/" + NoDocumeto;
        }


        //  
        /*--------------------------------------------------------------------------------------------------------------------------------------------------*/

        /*Busqueda */
        public string Movile_Select_BusquedaApp_Mtd(string Bandera, string Modulo, string Idioma, string Vigencia, string texto, string Ciudad, string idSuceso)
        {
            return url + "/" + Movile_Select_BusquedaApp + "/" + Bandera + "/" + Modulo + "/" + Idioma + "/" + Vigencia + "/" + texto + "/" + Ciudad + "/" + idSuceso;
        }

        /*--------------------------------------------------------------------------------------------------------------------------------------------------*/

        /*Taquilla Boleteria */
        public string Movile_Select_Comprar_Mtd(string NombreSuceso, string IdCiudad, string bandera, string IdSuceso, string Idcategoria, string Fecha, string Correo, string Idioma)
        {
            return url + "/" + Movile_Select_Comprar + "/" + NombreSuceso + "/" + IdCiudad + "/" + bandera + "/" + IdSuceso + "/" + Idcategoria + "/" + Fecha + "/" + Correo + "/" + Idioma;
        }

        public string Movile_Select_MisBoletasr_Mtd(string bandera, string Correo, string Idioma, string idtransaccion)
        {
            return url + "/" + Movile_Select_MisBoletas + "/" + bandera + "/" + Correo + "/" + Idioma + "/" + idtransaccion;
        }

        public string Movile_Select_Boletas_Mtd(string bandera, string IdSuceso, string Idcategoria, string IdActividad, string Correo, string Idioma, string cantidad, string Titular, string Telefono, string Identificacion,
            string IdIdentificacion, string IdBoleteria, string CodigoProm, string Vigencia, string Precio, string AplicaImpuesto, string DetalleImpuesto)
        {
            return url + "/" + Movile_Select_Boletas + "/" + bandera + "/" + IdSuceso + "/" + Idcategoria + "/" + IdActividad + "/" + Correo + "/" + Idioma + "/" + cantidad + "/" + Titular + "/" + Telefono + "/" + Identificacion
                + "/" + IdIdentificacion + "/" + IdBoleteria + "/" + CodigoProm + "/" + Vigencia + "/" + Precio + "/" + AplicaImpuesto + "/" + DetalleImpuesto;
        }
        public string Movile_Select_CategoriaBoletas_Mtd(string IdSuceso, string Idioma)
        {
            return url + "/" + Movile_Select_CategoriaBoletas + "/" + IdSuceso + "/" + Idioma;
        }


        public string Movile_Select_ConsultarTransaccion_Mtd(string bandera, string correo, string idioma, string idtransaccion, string idSuceso)
        {
            return url + "/" + Movile_Select_ConsultarTransaccion + "/" + bandera + "/" + correo + "/" + idioma + "/" + idtransaccion + "/" + idSuceso;
        }

        public string Movile_Insert_Boleteria_Mtd(string bandera, string Idcategoria, string IdActividad, string Correo, string Idioma, string IdtransaccionB)
        {
            return url + "/" + Movile_Insert_Boleteria + "/" + bandera + "/" + Idcategoria + "/" + IdActividad + "/" + Correo + "/" + Idioma + "/" + IdtransaccionB;
        }
        public string Movile_validar_boleta_Mtd(string idCategoria, string idSucesoServicio)
        {
            return url + "/" + Movile_validar_boleta + "/" + idCategoria + "/" + idSucesoServicio;
        }

        public string Movile_validar_CodigoPromocional_Mtd(string IdSuceso, string CodigoPromocional, string IdCategoria)
        {
            return url + "/" + Movile_validar_CodigoPromocional + "/" + IdSuceso + "/" + CodigoPromocional + "/" + IdCategoria;
        }

        public string Movile_delete_boleta_Mtd(string Bandera, string idTransaccionBoleta)
        {
            return url + "/" + Movile_delete_boleta + "/" + Bandera + "/" + idTransaccionBoleta;
        }

        /*--------------------------------------------------------------------------------------------------------------------------------------------------*/
        //Insertar logs en BD

        public string Movile_Insert_Logs_Mtd()
        {
            return url + "/" + Movile_Insert_Logs;
        }

        /*--------------------------------------------------------------------------------------------------------------------------------------------------*/
        //Actualiza info cfil BD con corferias

        public string Movile_Select_cfieldatosservicio_Mtd(string idTipoDocumento, string numeroDocumento)
        {
            if (idTipoDocumento.Equals("1") || idTipoDocumento.Equals("2") || idTipoDocumento.Equals("3") || idTipoDocumento.Equals("4") || idTipoDocumento.Equals("5") ||
                idTipoDocumento.Equals("6") || idTipoDocumento.Equals("7") || idTipoDocumento.Equals("8") || idTipoDocumento.Equals("9"))
            {
                idTipoDocumento = "0" + idTipoDocumento;
            }
            return url + "/" + Movile_Select_cfieldatosservicio + "/" + idTipoDocumento + "/" + numeroDocumento;
        }

        public string Movile_Select_MensajeAux_Mtd(string bandera, string modulo, string campo)
        {
            return url + "/" + Movile_Select_MensajeAux + "/" + bandera + "/" + modulo + "/" + campo;
        }

        public string Movile_Select_preregistro_Mtd(string tipoDocumento, string numeroDocumento, string idSuceso, string correo)
        {
            if (tipoDocumento.Equals("1") || tipoDocumento.Equals("2") || tipoDocumento.Equals("3") || tipoDocumento.Equals("4") || tipoDocumento.Equals("5") ||
                tipoDocumento.Equals("6") || tipoDocumento.Equals("7") || tipoDocumento.Equals("8") || tipoDocumento.Equals("9"))
            {
                tipoDocumento = "0" + tipoDocumento;
            }

            return url + "/" + Movile_Select_preregistro + "/" + tipoDocumento + "/" + numeroDocumento + "/" + idSuceso + "/" + correo;
        }

        public string Movile_Insert_habeasData_Mtd(string Bandera, string Correo)
        {
            return url + "/" + Movile_Insert_habeasData + "/" + Bandera + "/" + Correo;
        }

        public string Movile_select_consultachatwh_Mtd(string Bandera, string Idioma)
        {
            return url + "/" + Movile_select_consultachatwh + "/" + Bandera + "/" + Idioma;
        }

        /*--------------------------------------------------------------------------------------------------------------------------------------------------*/

        // Se hace la coneccion con el ws pos POST
        public async Task<string?> ConectionPost(string json, string urlo)
        {
            try
            {
                ClientPost = new HttpClient();

                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage? peticion = null;
                peticion = await ClientPost.PostAsync(urlo, content);


                if (peticion != null)
                {
                    string jsonProcedimiento = peticion.Content.ReadAsStringAsync().Result;
                    return jsonProcedimiento;
                }

                return default;
            }
            catch (Exception e)
            {
                Debug.WriteLine("\tERROR {0}", e.Message);
                return default;
            }
        }

        // Se hace la coneccion con el ws por GET
        public async Task<string?> ConectionGet(string urlo)
        {
            try
            {
                ClientGet = new HttpClient();

                var peticion = await ClientGet.GetAsync(urlo); // Se realiza la peticion

                if (peticion != null)
                {
                    string jsonProcedimiento = peticion.Content.ReadAsStringAsync().Result;
                    return jsonProcedimiento;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("\tERROR {0}", e.Message);
            }
            return default;
        }

        public async Task<string> UploadDataAsync(byte[] imagen) //recibimos la imagen como parametro en el metodo
        {
            try
            {
                //create new HttpClient and MultipartFormDataContent and add our file, and StudentId
                HttpClient client = new HttpClient();
                MultipartFormDataContent content = new MultipartFormDataContent();
                ByteArrayContent baContent = new ByteArrayContent(imagen);
                DateTime localDate = DateTime.Now;
                content.Add(baContent, "file", "img" + localDate.ToString("yyyy-MM-dd-HH-s") + ".jpg");

                //upload MultipartFormDataContent content async and store response in response var
                var response = await client.PostAsync(url + Movile_Subir_Imagen, content);
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    //read response result as a string async into json var
                    return response.Content.ReadAsStringAsync().Result;
                }
                else
                {
                    return "no se hizo";
                }

            }
            catch (Exception e)
            {
                //debug
                Debug.WriteLine("Exception Caught: " + e.ToString());
                return "no se hizo";
            }
        }

        // Se hace la coneccion con el ws pos DELETE
        public async Task<string?> ConectionDelete(string urlo)
        {
            try
            {
                ClientPost = new HttpClient();

                HttpResponseMessage? peticion = null;
                peticion = await ClientPost.DeleteAsync(urlo);


                if (peticion != null)
                {
                    string jsonProcedimiento = peticion.Content.ReadAsStringAsync().Result;
                    return jsonProcedimiento;
                }

                return default;
            }
            catch (Exception e)
            {
                Debug.WriteLine("\tERROR {0}", e.Message);
                return default;
            }
        }
    }
}
