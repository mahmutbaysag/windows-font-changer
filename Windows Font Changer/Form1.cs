using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Diagnostics;
using System.Globalization;

namespace Windows_Font_Changer
{
    public partial class Form1 : Form
    {
        private bool _dragging = false;
        private Point _offset;
        private Point _start_point = new Point(0, 0);

        public Form1()
        {
            InitializeComponent();
        }

        RegistryKey register;

        public static bool checkMachineType()
        {
            RegistryKey winLogonKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\FontSubstitutes", true);
            return (winLogonKey.GetValueNames().Contains("Segoe UI"));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CultureInfo ci = CultureInfo.InstalledUICulture;
            
            switch(CultureInfo.CurrentCulture.Name)
            {
                case "tr-TR":
                    Localization.Culture = new CultureInfo("tr-TR");
                    break;
                default:
                    Localization.Culture = new CultureInfo("en-US");
                    break;
            }

            button1.Text = Localization.degistir;
            button2.Text = Localization.sifirla;

            InstalledFontCollection installedFontCollection = new InstalledFontCollection();
            FontFamily[] fontlar = installedFontCollection.Families;

            foreach(FontFamily i in fontlar)
            {
                comboBox1.Items.Add(i.Name);
            }

            register = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\FontSubstitutes", true);
            if (!checkMachineType())
            {
                comboBox1.Text = "Segoe UI";
            }
            else
            {
                comboBox1.Text = register.GetValue("Segoe UI").ToString();
            }

        }

        void fontDegistir(string fontName)
        { 
            
            register = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts",true);
            register.SetValue("Segoe UI (TrueType)","");
            register.SetValue("Segoe UI Bold (TrueType)", "");
            register.SetValue("Segoe UI Bold Italic(TrueType)", "");
            register.SetValue("Segoe UI Italic (TrueType)", "");
            register.SetValue("Segoe UI Semibold (TrueType)", "");
            register.SetValue("Segoe UI Symbol (TrueType)", "");
            
            register = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\FontSubstitutes",true);
            register.SetValue("Segoe UI", fontName);
        }  
        
        void fontSifirla()
        {
            
            register = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts",true);
            register.SetValue("Segoe UI (TrueType)", "segoeui.ttf");
            register.SetValue("Segoe UI Black (TrueType)", "seguibl.ttf");
            register.SetValue("Segoe UI Black Italic (TrueType)", "seguibli.ttf");
            register.SetValue("Segoe UI Bold (TrueType)", "segoeuib.ttf");
            register.SetValue("Segoe UI Bold Italic (TrueType)", "segoeuiz.ttf");
            register.SetValue("Segoe UI Emoji (TrueType)", "seguiemj.ttf");
            register.SetValue("Segoe UI Historic (TrueType)", "seguihis.ttf");
            register.SetValue("Segoe UI Italic (TrueType)", "segoeuii.ttf");
            register.SetValue("Segoe UI Light (TrueType)", "segoeuil.ttf");
            register.SetValue("Segoe UI Light Italic (TrueType)", "seguili.ttf");
            register.SetValue("Segoe UI Semibold (TrueType)", "seguisb.ttf");
            register.SetValue("Segoe UI Semibold Italic (TrueType)", "seguisbi.ttf");
            register.SetValue("Segoe UI Semilight (TrueType)", "segoeuisl.ttf");
            register.SetValue("Segoe UI Semilight Italic (TrueType)", "seguisli.ttf");
            register.SetValue("Segoe UI Symbol (TrueType)", "seguisym.ttf");
            register.SetValue("Segoe MDL2 Assets (TrueType)", "segmdl2.ttf");
            register.SetValue("Segoe Print (TrueType)", "segoepr.ttf");
            register.SetValue("Segoe Print Bold (TrueType)", "segoeprb.ttf");
            register.SetValue("Segoe Script (TrueType)", "segoesc.ttf");
            register.SetValue("Segoe Script Bold (TrueType)", "segoescb.ttf");

            register = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\FontSubstitutes",true);
            if(register.GetValue("Segoe UI")!=null)
            {
                register.DeleteValue("Segoe UI");
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            fontDegistir(comboBox1.SelectedItem.ToString());
            MessageBox.Show(Localization.yazitipiDegistirMesaj,Localization.mesajBaslik);
            restartCheck();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            fontSifirla();
            MessageBox.Show(Localization.yazitipiSifirlaMesaj, Localization.mesajBaslik);
            restartCheck();
        }

        void restartCheck()
        {
            if(MessageBox.Show(Localization.yenidenBaslatMesaj, Localization.yenidenBaslatBaslik, MessageBoxButtons.YesNo, MessageBoxIcon.Information)==DialogResult.Yes)
            {
                Process.Start("shutdown.exe","-r -t 0");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            richTextBox1.Font = new Font(comboBox1.Text,15);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1_TextChanged(sender,e);
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            comboBox1_TextChanged(sender,e);
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            _dragging = true;  // _dragging is your variable flag
            _start_point = new Point(e.X, e.Y);
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            _dragging = false;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (_dragging)
            {
                Point p = PointToScreen(e.Location);
                Location = new Point(p.X - this._start_point.X, p.Y - this._start_point.Y);
            }
        }
    }
}


