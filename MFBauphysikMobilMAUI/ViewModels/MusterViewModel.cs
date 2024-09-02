using MFBauphysikMobilMAUI.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace MFBauphysikMobil.ViewModels
{
	public class MusterViewModel : INotifyPropertyChanged
	{
		readonly IList<MainModel> source;
		MainModel selectedAufbau;
		int selectionCount = 1;

		public ObservableCollection<MainModel> Aufbau { get; private set; }
		public IList<MainModel> EmptyAufbau { get; private set; }

		public MainModel SelectedAufbau
		{
			get
			{
				return selectedAufbau;
			}
			set
			{
				if (selectedAufbau != value)
				{
					selectedAufbau = value;
				}
			}
		}

		ObservableCollection<object> selectedAufbaus;
		public ObservableCollection<object> SelectedAufbaus
		{
			get
			{
				return selectedAufbaus;
			}
			set
			{
				if (selectedAufbaus != value)
				{
					selectedAufbaus = value;
				}
			}
		}


		public string SelectedAufbauMessage { get; private set; }

		public ICommand AufbauSelectionChangedCommand => new Command(AufbauSelectionChanged);
		public MusterViewModel()
		{
			source = new List<MainModel>();
			CreateAufbauCollection();

			selectedAufbau = Aufbau.Skip(3).FirstOrDefault();
			AufbauSelectionChanged();

			SelectedAufbaus = new ObservableCollection<object>()
		{
			Aufbau[1], Aufbau[3], Aufbau[4]
		};
		}

        void CreateAufbauCollection()
        {
            source.Add(new MainModel
            {
                MusterName = "Beton_G200_EPS_PYE",

            });
            source.Add(new MainModel
            {
                MusterName = "Sparrendach",

            });
            source.Add(new MainModel
            {
                MusterName = "Ständerwand",
            });

            //Beton KSD EPS035 PVC
            source.Add(new MainModel
            {
                MusterName = "Beton_KSD_EPS035_PVC",

            });

            //OSB KSD EPS035 PVC
            source.Add(new MainModel
            {
                MusterName = "OSB_KSD_EPS035_PVC",

            });

            //Beton
            source.Add(new MainModel
            {
                MusterName = "Beton_KSD_EPS_EPDM",

            });
            source.Add(new MainModel
            {
                MusterName = "Beton_KSD_EPS_Evalon",

            });
            source.Add(new MainModel
            {
                MusterName = "Beton_KSD_EPS_FPO",

            });
            source.Add(new MainModel
            {
                MusterName = "Beton_KSD_EPS_PVC",

            });
            source.Add(new MainModel
            {
                MusterName = "Beton_KSD_EPS_PYE_einlagig",

            });
            source.Add(new MainModel
            {
                MusterName = "Beton_KSD_EPS_PYE_zweilagig",

            });
            source.Add(new MainModel
            {
                MusterName = "Beton_KSD_MIFA_EPDM",

            });
            source.Add(new MainModel
            {
                MusterName = "Beton_KSD_MIFA_Evalon",

            });
            source.Add(new MainModel
            {
                MusterName = "Beton_KSD_MIFA_FPO",

            });
            source.Add(new MainModel
            {
                MusterName = "Beton_KSD_MIFA_PVC",

            });
            source.Add(new MainModel
            {
                MusterName = "Beton_KSD_MIFA_PYE_einlagig",

            });
            source.Add(new MainModel
            {
                MusterName = "Beton_KSD_MIFA_PYE_zweilagig",

            });
            source.Add(new MainModel
            {
                MusterName = "Beton_KSD_PUR_EPDM",

            });
            source.Add(new MainModel
            {
                MusterName = "Beton_KSD_PUR_Evalon",

            });
            source.Add(new MainModel
            {
                MusterName = "Beton_KSD_PUR_FPO",

            });
            source.Add(new MainModel
            {
                MusterName = "Beton_KSD_PUR_PVC",

            });
            source.Add(new MainModel
            {
                MusterName = "Beton_KSD_PUR_PYE_einlagig",

            });
            source.Add(new MainModel
            {
                MusterName = "Beton_KSD_PUR_PYE_zweilagig",

            });
            //OSB
            source.Add(new MainModel
            {
                MusterName = "OSB_KSD_EPS_EPDM",

            });
            source.Add(new MainModel
            {
                MusterName = "OSB_KSD_EPS_Evalon",

            });
            source.Add(new MainModel
            {
                MusterName = "OSB_KSD_EPS_FPO",

            });
            source.Add(new MainModel
            {
                MusterName = "OSB_KSD_EPS_PVC",

            });
            source.Add(new MainModel
            {
                MusterName = "OSB_KSD_EPS_PYE_einlagig",

            });
            source.Add(new MainModel
            {
                MusterName = "OSB_KSD_EPS_PYE_zweilagig",

            });
            source.Add(new MainModel
            {
                MusterName = "OSB_KSD_MIFA_EPDM",

            });
            source.Add(new MainModel
            {
                MusterName = "OSB_KSD_MIFA_Evalon",

            });
            source.Add(new MainModel
            {
                MusterName = "OSB_KSD_MIFA_FPO",

            });
            source.Add(new MainModel
            {
                MusterName = "OSB_KSD_MIFA_PVC",

            });
            source.Add(new MainModel
            {
                MusterName = "OSB_KSD_MIFA_PYE_einlagig",

            });
            source.Add(new MainModel
            {
                MusterName = "OSB_KSD_MIFA_PYE_zweilagig",

            });
            source.Add(new MainModel
            {
                MusterName = "OSB_KSD_PUR_EPDM",

            });
            source.Add(new MainModel
            {
                MusterName = "OSB_KSD_PUR_Evalon",

            });
            source.Add(new MainModel
            {
                MusterName = "OSB_KSD_PUR_FPO",

            });
            source.Add(new MainModel
            {
                MusterName = "OSB_KSD_PUR_PVC",

            });
            source.Add(new MainModel
            {
                MusterName = "OSB_KSD_PUR_PYE_einlagig",

            });
            source.Add(new MainModel
            {
                MusterName = "OSB_KSD_PUR_PYE_zweilagig",

            });

            //Trapezblech KSD
            source.Add(new MainModel
            {
                MusterName = "Trapezblech_KSD_EPS_EPDM",

            });
            source.Add(new MainModel
            {
                MusterName = "Trapezblech_KSD_EPS_Evalon",

            });
            source.Add(new MainModel
            {
                MusterName = "Trapezblech_KSD_EPS_FPO",

            });
            source.Add(new MainModel
            {
                MusterName = "Trapezblech_KSD_EPS_PVC",

            });
            source.Add(new MainModel
            {
                MusterName = "Trapezblech_KSD_EPS_PYE_einlagig",

            });
            source.Add(new MainModel
            {
                MusterName = "Trapezblech_KSD_EPS_PYE_zweilagig",

            });
            source.Add(new MainModel
            {
                MusterName = "Trapezblech_KSD_MIFA_EPDM",

            });
            source.Add(new MainModel
            {
                MusterName = "Trapezblech_KSD_MIFA_Evalon",

            });
            source.Add(new MainModel
            {
                MusterName = "Trapezblech_KSD_MIFA_FPO",

            });
            source.Add(new MainModel
            {
                MusterName = "Trapezblech_KSD_MIFA_PVC",

            });
            source.Add(new MainModel
            {
                MusterName = "Trapezblech_KSD_MIFA_PYE_einlagig",

            });
            source.Add(new MainModel
            {
                MusterName = "Trapezblech_KSD_MIFA_PYE_zweilagig",

            });
            source.Add(new MainModel
            {
                MusterName = "Trapezblech_KSD_PUR_EPDM",

            });
            source.Add(new MainModel
            {
                MusterName = "Trapezblech_KSD_PUR_Evalon",

            });
            source.Add(new MainModel
            {
                MusterName = "Trapezblech_KSD_PUR_FPO",

            });
            source.Add(new MainModel
            {
                MusterName = "Trapezblech_KSD_PUR_PVC",

            });
            source.Add(new MainModel
            {
                MusterName = "Trapezblech_KSD_PUR_PYE_einlagig",

            });
            source.Add(new MainModel
            {
                MusterName = "Trapezblech_KSD_PUR_PYE_zweilagig",

            });
            //Trapezblech PE
            source.Add(new MainModel
            {
                MusterName = "Trapezblech_PE_MIFA_EPDM",

            });
            source.Add(new MainModel
            {
                MusterName = "Trapezblech_PE_MIFA_Evalon",

            });
            source.Add(new MainModel
            {
                MusterName = "Trapezblech_PE_MIFA_FPO",

            });
            source.Add(new MainModel
            {
                MusterName = "Trapezblech_PE_MIFA_PVC",

            });
            source.Add(new MainModel
            {
                MusterName = "Trapezblech_PE_MIFA_PYE_einlagig",

            });
            source.Add(new MainModel
            {
                MusterName = "Trapezblech_PE_MIFA_PYE_zweilagig",

            });

            //Umkehrdach
            source.Add(new MainModel
            {
                MusterName = "Umkehrdach",

            });
            //WDVS
            source.Add(new MainModel
            {
                MusterName = "WDVS_EPS-032",

            });
            source.Add(new MainModel
            {
                MusterName = "WDVS_EPS-035",

            });

            source.Add(new MainModel
            {
                MusterName = "KSD_EPS035_PVC",

            });

            Aufbau = new ObservableCollection<MainModel>(source);
        }

        void AufbauSelectionChanged()
		{
			SelectedAufbauMessage = $"Selection {selectionCount}:{SelectedAufbau.MusterName}";
			OnPropertyChanged("SelectedAufbauMessage");
			selectionCount++;
		}

		#region INotifyPropertyChanged
		public event PropertyChangedEventHandler PropertyChanged;

		void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
		#endregion
	}
}
