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
