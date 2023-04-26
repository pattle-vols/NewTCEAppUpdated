using NewTCEApp.Models;
using NewTCEApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace NewTCEApp.ViewModels
{
	public class detail
	{
		public string Title { get; set; }
		public string Url { get; set; }
		public string Location { get; set; }
	}
	public class BaseViewModel : INotifyPropertyChanged
    {
		

		bool isBusy;
		string title;

		public ObservableCollection<Event> Events { get; }

		public bool IsBusy
		{
			get => isBusy;
			set
			{
				if (isBusy == value)
					return;
				isBusy = value;
				OnPropertyChanged();
				// Also raise the IsNotBusy property changed
				OnPropertyChanged(nameof(IsNotBusy));
			}
		}

		public bool IsNotBusy => !IsBusy;
		public string Title
		{
			get => title;
			set
			{
				if (title == value)
					return;
				title = value;
				OnPropertyChanged();
			}
		}
		public BaseViewModel()
		{

		}

		public void OnPropertyChanged([CallerMemberName] string name = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

		public event PropertyChangedEventHandler PropertyChanged;
	}
}
