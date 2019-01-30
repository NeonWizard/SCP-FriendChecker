using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json.Linq;

namespace FriendChecker
{
	public class SteamAPI
	{
		private readonly FriendChecker plugin;

		public SteamAPI(FriendChecker plugin) => this.plugin = plugin;

		public string[] GetFriends(string steamID)
		{
			using (WebClient wc = new WebClient())
			{
				try
				{
					JObject json = JObject.Parse(
						wc.DownloadString(
							"https://api.steampowered.com/ISteamUser/GetFriendList/v1/?key=2E13692A3A02D5BCE96337CB82FF1F54&steamid=" + steamID + "&relationship=friend"
						)
					);

					return json["friendslist"]["friends"].Select(friend => friend["steamid"].ToString()).ToArray();
				}
				catch {
					return new string[] { };
				}
			}
		}
	}
}
