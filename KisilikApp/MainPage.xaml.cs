using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using KisilikApp.Resources;
using System.Text.RegularExpressions;
using System.Text;
using Microsoft.Xna.Framework.GamerServices;


namespace KisilikApp
{
    public partial class MainPage : PhoneApplicationPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        string IsimAnalizi(string isim)
        {
            if (isim.Trim().Length == 0 || isim.Trim().Length == 1) return "";
            string ozellik = "";
            isim = isim.Trim();
            if (isim[0] == isim[1])
                return "";
            else
            {
                if (isim[0] == ' ') return "";
                if (isim[1] == ' ') return "";
            }
            #region Ozellik
            char[] harf = { 'e','r','t','y','u','ı','o','p','ü','a',
                            's','d','f','g','h','j','k','l','ş','i','z','c',
                            'v','b','n','m','ö','ç'};
            string[] isimozellik = {"üzüntü ve sevinci bir arada yaşayan (ruhsal gel-gitleri olan), ",
                                   "zor karar veren, ","oldukça ketum tavırlı ve duygularını zor açan, ",
                                   "geçmişi unutmayan, güçlü, ","durgun, işlerini ağırdan alan, ","hassas, duygusal, kırılgan, ","gizemli,",
                                   "kendinden emin, kendine güvenen,","durgun, işlerini ağırdan alan, ","algı kabiliyeti yüksek, mantıklı, ","hayalperest, ",
                                   "zorluklara karşı direnen ve hırslı, ","sakin, uysal, güvenilir, ","inatçı, güç arzusu olan, ","sakin, durağan, ",
                                   "kaprisli, kıskanç, ","başarılı, ünvan arzusu olan ve sürekli yükselen, ","sanatsal yönleri olan, kabiliyetli, ",
                                   "hayalperest, ","hassas, duygusal, kırılgan, ","akademik anlamda okumayı ve kendini geliştirmeyi seven, ",
                                   "güzel sanatlara karşı yetenekli, duygusal, ","içine dönük, umursamaz, ","ön sezileri kuvvetli, umut dolu, ",
                                   "sağduyulu, "/*yaratıcı,(bunda kararsızım)*/,"ticarete yatkın, yüksek zekalı, ","gizemli, ","yaşamında zevk, sefaya düşkün, "};
            #endregion
            int[] birkere = new int[harf.Length];
            for (int i = 0; i < isim.Length; i++)
            {
                for (int v = 0; v < harf.Length; v++)
                {
                    if (char.ToLower(isim[i]) == harf[v])
                        if (birkere[v] < 1)
                        {
                            ozellik += isimozellik[v];
                            birkere[v]++;
                        }
                }
            }
            return ozellik.Trim(',').Substring(0, ozellik.Length - 2) + " bir kişilik.";
        }
        public static string StringBuyult(string deger)
        {
            deger = deger.ToLower();
            deger = " " + deger;
            string yenideger = "";
            for (int i = 0; i < deger.Length; i++)
            {
                if (deger[i] != ' ') yenideger += deger[i];
                else
                {
                    yenideger += deger[i];
                    string str = deger.Substring(i + 1, 1).ToUpper();
                    yenideger += str;
                    i++;
                }
            }
            return yenideger.Substring(1);
        }

        private void btn_analiz_Click(object sender, RoutedEventArgs e)
        {
            string analiz = IsimAnalizi(txt_isim.Text);

            if (analiz != "")
            {
                Guide.BeginShowMessageBox(StringBuyult(txt_isim.Text.Trim()), char.ToUpper(analiz[0]) + analiz.Substring(1), new string[] { "Kapat" }, 0, MessageBoxIcon.None, new AsyncCallback(Paylas), null);
            }
        }
        void Paylas(IAsyncResult py)
        {
            Guide.EndShowMessageBox(py);
        }

    }
}