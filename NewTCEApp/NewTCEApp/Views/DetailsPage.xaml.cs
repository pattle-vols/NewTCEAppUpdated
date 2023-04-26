using NewTCEApp.Models;
using NewTCEApp.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NewTCEApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DetailsPage : ContentPage
	{
		static detail _detail = new detail();
		public static void InitPage(string title, string url, string location)
		{
			_detail.Title = title;
			_detail.Url = url;
			_detail.Location = location;
		}
		public DetailsPage()
		{
			InitializeComponent();
			BindingContext = _detail;
		}
	}
}