using MFBauphysikMobilMAUI.Models;
using MFBauphysikMobilMAUI.ViewModels;
using System;
using SQLitePCL;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm;
using Microsoft.Maui.Controls.Xaml;
using MFBauphysikMobilMAUI.Interface;
using System.Threading;
using System.Runtime.Serialization;
using System.IO;
using System.Net.Mail;
using System.ComponentModel.Design;
using System.Xml;
using System.Linq.Expressions;
using MFBauphysikMobilMAUI.Info;
using MFBauphysikMobilMAUI.Helpers;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace MFBauphysikMobilMAUI.NewProject
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProjektMenu : ContentPage
        
    {
        public MainModel main_model { get;set; }
        private double _size_title;
        public double SizeTitle
        {
            get { return _size_title; }
            set
            {
                if (_size_title == value)
                    return;
                _size_title = value;
                OnPropertyChanged(nameof(SizeTitle));
            }
        }

        public ProjektMenu(MainModel project)
        {
            InitializeComponent();
            main_model = new MainModel
            {
                Selected = project.Selected,
                ID = project.ID,
                MusterName = project.MusterName,
                ProjectName = project.ProjectName,
                Date = project.Date,
                BV_Ersatz = project.BV_Ersatz,               
            };
            bv_label.FontSize = Setting.Size_Default;
            info_label.FontSize = Setting.Size_Default;
            klima_label.FontSize = Setting.Size_Default;
            SizeTitle = Setting.Size_Title;
        }

        private void Back_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private async void Mail_Clicked(object sender, EventArgs e)
        {
            var fn = main_model.ProjectName.ToString() + ".xbph";
            var file = Path.Combine(Microsoft.Maui.Storage.FileSystem.CacheDirectory, fn);
            var message = new EmailMessage
            {
                Subject = main_model.ProjectName.ToString() + ".xbph",
                Body = "Projekt: " + main_model.ProjectName.ToString(),
            };
            message.Attachments.Add(new EmailAttachment(file));
            await Email.ComposeAsync(message);

        

            using (XmlWriter writer = XmlWriter.Create("test.xml"))
            {
                writer.WriteStartElement("DOC");
                writer.WriteElementString("BV", main_model.ProjectName.ToString());
                writer.WriteElementString("BVErsatz", main_model.BV_Ersatz.ToString());
                writer.WriteElementString("Mustername", main_model.MusterName.ToString());
                writer.WriteElementString("Bauteil", main_model.Bauteil_Basis.ToString());
                writer.WriteElementString("Bauteil", main_model.Bauteil_Sparren.ToString());
                writer.WriteElementString("Bauteil", main_model.Bauteil_Gefach.ToString());
                writer.WriteElementString("Bauteil", main_model.Bauteil_Ständer.ToString());
                writer.WriteElementString("Befestiger", main_model.Befestiger_Basis.ToString());
                writer.WriteElementString("Befestiger", main_model.Befestiger_Sparren.ToString());
                writer.WriteElementString("Befestiger", main_model.Befestiger_Gefach.ToString());
                writer.WriteElementString("Befestiger", main_model.Befestiger_Ständer.ToString());
                writer.WriteElementString("Date", main_model.Date.ToString());
                writer.WriteEndElement();

            }
        }
        private async void Klimadaten_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new KlimadatenPage());
        }      

        private async void BV_Clicked(object sender, EventArgs e)
        {
            var bv = main_model as MainModel;
            await Navigation.PushAsync(new BV(bv)
            {
                BindingContext = bv as MainModel
            });
        }

        private void Info_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new InfoPage());
        }     

    }

    public class EmailTest
    {
        public async Task SendEmail (string sub, string body, List<string> recipients)
        {
           // try
            //{
                var message = new EmailMessage
                {
                    Subject = sub,
                    Body = body,
                    To = recipients,
                };
                await Email.ComposeAsync(message);
            //}
            /*catch (FeatureNotSupportedException fbsEx)
            {

            }
            catch (Exception ex)
            {

            }*/
        }
    }
}