using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace NewTCEApp.Models
{
	//[ObservableObject]
	public partial class Event
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string Time { get; set; }
		public string Location { get; set; }
		public string Image { get; set; }
		public string Url { get; set; }
	}
}
