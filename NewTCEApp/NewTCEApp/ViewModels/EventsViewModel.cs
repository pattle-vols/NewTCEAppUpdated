using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
//html pack
using HtmlAgilityPack;
using System.Net.Http;
using System.Threading.Tasks;
using System.Diagnostics;

using Xamarin.Forms;
using System.Linq;
using NewTCEApp.Models;
using NewTCEApp.ViewModels;
using NewTCEApp.Views;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace NewTCEApp.ViewModels
{
	public partial class EventsViewModel : BaseViewModel
	{
		public ObservableCollection<Event> Events { get; }
		public Command GetEventsCommand { get; }
		//public Command GoToDetailsCommand { get; }

		public EventsViewModel()
		{
			Title = "Upcoming TCE Events";
			Events = new ObservableCollection<Event>();

			GetEventsCommand = new Command(async () => await GetEventsAsync());

		}

		[RelayCommand]
		async Task GoToDetailsAsync(Event @event)
		{
			if (@event is null)
				return;
			DetailsPage.InitPage(@event.Title, @event.Url,@event.Location);
			await Shell.Current.GoToAsync($"{nameof(DetailsPage)}?info={@event.Url}");
		}
		
		HttpClient httpClient;
		HttpClient Client => httpClient ?? (httpClient = new HttpClient());

		//Text Cleaning Function
		static string RemoveWhiteSpaceInString(string pattern)
		{
			string temp = pattern.Replace('\t', ' ');
			string tempstr = temp.Replace('\n', ' ');


			//int count = 0;
			StringBuilder SB = new StringBuilder();
			bool PrevChar = false;
			foreach (char item in tempstr)
			{
				bool NextChar = item == ' ';
				if (!NextChar || !PrevChar)
				{
					SB.Append(item);
					PrevChar = NextChar;
				}
			}
			return SB.ToString();
		}

		//Retrive data from school
		public async Task<List<Event>> GetEvents()
		{
			//get web data
			List<Event> data = new List<Event>();

			//choose your website
			var url = "https://calendar.utk.edu/engineering";

			//get the html page source 
			var httpClient = new HttpClient();
			var html = await httpClient.GetStringAsync(url);

			//store the html of the page in a variable
			var doc = new HtmlDocument();
			doc.LoadHtml(html);


			//split doc into sub headings and collect info
			var titles = doc.DocumentNode.Descendants("h3").ToList();
			var summaries = doc.DocumentNode.Descendants("h4").ToList();
			var locations2 = doc.DocumentNode.Descendants("div").Where(d => d.GetAttributeValue("class", "").Contains("location"));
			var locations = locations2.ToList();
			var times = doc.DocumentNode.Descendants("abbr").ToList();

			// extracting all links
			List<string> urlsd = new List<string>();
			foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//a[@href]"))
			{
				HtmlAttribute att = link.Attributes["href"];

				if (att.Value.Contains("a"))
				{
					// showing output
					if (att.Value.Contains("utk.edu/event/"))
						//Console.WriteLine(att.Value);
						urlsd.Add(att.Value);
				}
			}
			List<string> urls = urlsd.Distinct().ToList();

			//image urls
			var images2 = doc.DocumentNode.Descendants("img")
							.Select(d => d.GetAttributeValue("src", null))
							.Where(s => !System.String.IsNullOrEmpty(s));
			var images = images2.Distinct().ToList();

			//clean info
			titles.RemoveAt(0);
			summaries.RemoveAt(0);
			summaries.RemoveAt(summaries.Count - 1);
			urls.RemoveAt(urls.Count - 1);


			try
			{
				for (var i = 0; i < titles.Count; i++)
				{
					Event tmp = new Event();
					
					//tmp.Title = titles[i].InnerText;
					tmp.Title = RemoveWhiteSpaceInString(titles[i].InnerText);
					//tmp.Description = summaries[i].InnerText;
					tmp.Description = RemoveWhiteSpaceInString(summaries[i].InnerText);
					//tmp.Location = locations[i].InnerText;
					tmp.Location = RemoveWhiteSpaceInString(locations[i].InnerText);
					//tmp.Time = times[i].InnerText;
					tmp.Time = RemoveWhiteSpaceInString(times[i].InnerText);
					tmp.Url = urls[i];
					tmp.Image = images[i];
					
					//Debug.WriteLine(tmp.Title);
					data.Add(tmp);
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"Error Collecting Info: {ex.Message}");
				await Application.Current.MainPage.DisplayAlert("Error!", ex.Message, "Close");
			}


			return data;
		}
		async Task GetEventsAsync()
		{
			if (IsBusy)
				return;

			try
			{
				IsBusy = true;

				var eventList = await GetEvents();
				Events.Clear();
				foreach (var e in eventList)
					Events.Add(e);
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"Unable to get retrieve information: {ex.Message}");
				await Application.Current.MainPage.DisplayAlert("Error!", ex.Message, "OK");
			}
			finally
			{
				IsBusy = false;
			}
		}
	}
}
