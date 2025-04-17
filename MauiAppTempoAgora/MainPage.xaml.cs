using MauiAppTempoAgora.Models;
using MauiAppTempoAgora.Services;
using System.Diagnostics;

namespace MauiAppTempoAgora
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {

        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txt_cidade.Text))
                {
                    Tempo? t = await DataService.GetPrevisao(txt_cidade.Text);

                    if (t != null)
                    {
                        string dados_previsao = "";

                        dados_previsao = $"Latidude: {t.lat} \n" +
                                         $"Longitude: {t.lon} \n" +
                                         $"Nascer do Sol: {t.sunrise} \n" +
                                         $"Por do Sol: {t.sunset} \n" +
                                         $"Temp Máx: {t.temp_max} \n" +
                                         $"Temp Min: {t.temp_min} \n";
                        lbl_res.Text = dados_previsao;

                        string mapa = $"https://embed.windy.com/embed.html?" +
                                      $"type=map&location=coordinates&metricRain=mm&" +
                                      $"metricTemp=ºC&metricWind=km/h&zoom=4&overlay=wind&" +
                                      $"product=ecmwf&level=surface" +
                                      $"&lat={t.lat.ToString().Replace(",", ".")}" +
                                      $"&lon={t.lon.ToString().Replace(",", ".")}";

                        wv_mapa.Source = mapa;
                        Debug.WriteLine(mapa);
                    } else
                    {
                        lbl_res.Text = "Sem dados de Previsão";
                    }
                } else
                {
                    lbl_res.Text = "Preencha a cidade.";
                }
            } catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "OK"); 
            }
        }
    }
}
