using projectlufi.Models;
using projectlufi.ViewModels.UyeBilgileriVM;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace projectlufi.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class KayitSayfasi : ContentPage
    {
        public KayitSayfasi()
        {
            InitializeComponent();
           
        }
        public void Kaydet(object sender,System.EventArgs e)
        {
            var dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "UserDatabase.db");
            var db = new SQLiteConnection(dbpath);
            db.CreateTable<UyeBilgileri>();

            var item = new UyeBilgileri()
            {
                UyeAd = AdEditor.Text,
                UyeEmail = EmailEntry.Text,
                UyeSifre = SifreEditor.Text,
                UyeSoyad = SoyadEditor.Text
            };
            db.Insert(item);
            Device.BeginInvokeOnMainThread(async () =>
            {
                var result = await this.DisplayAlert("Tebrikler!", "Kayit Isleminiz Tamamlandi", "Tamam", "Iptal");
                if (result)
                    await Navigation.PushAsync(new GirisSayfasi());
            });
        }
    }
}