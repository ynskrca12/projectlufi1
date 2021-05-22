using projectlufi.Models;
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
    public partial class GirisSayfasi : ContentPage
    {
        public GirisSayfasi()
        {
            InitializeComponent();
        }
        async void Handle_Click(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new KayitSayfasi());
        }
        void  GirisYap(object sender, EventArgs e)
        {
            var dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "UserDatabase.db");
            var db = new SQLiteConnection(dbpath);
            var myquery = db.Table<UyeBilgileri>().Where(u => u.UyeEmail.Equals(EmailEntry.Text) && u.UyeSifre.Equals(SifreEntry.Text)).FirstOrDefault();
            if (myquery != null)
            {
                App.Current.MainPage = new AppShell();
            }
            else
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var result = await this.DisplayAlert("Hata!", "Gecersiz Kullanici", "Tamam", "Iptal");
                    if (result)
                        await Navigation.PushAsync(new GirisSayfasi());
                    else
                    {
                        await Navigation.PushAsync(new GirisSayfasi());
                    }
                });
            }
        }
    }
}