using CommunityToolkit.Mvvm.ComponentModel;
using NewTCEApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace NewTCEApp.ViewModels
{
	[QueryProperty("Event", "Event")]

	
	public partial class EventDetailsViewModel : BaseViewModel
	{
		public EventDetailsViewModel()
		{
		}

		

		//[ObservableProperty]
		//Event Event;

	}
}
