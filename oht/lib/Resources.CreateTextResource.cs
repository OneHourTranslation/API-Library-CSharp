using System;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace oht.lib
{
	public interface ICreateTextResourceProvider
	{
		string Get(string url, WebProxy proxy, string publicKey, string secretKey, string text);
	}
	public class CreateTextResourceProvider : ICreateTextResourceProvider
	{
		public string Get(string url, WebProxy proxy, string publicKey, string secretKey, string text)
		{
			using (var client = new WebClient())
			{
				if (proxy != null)
					client.Proxy = proxy;
				client.Encoding = Encoding.UTF8;
				var web = url + String.Format("/resources/text?public_key={0}&secret_key={1}", publicKey, secretKey);

				var values = new System.Collections.Specialized.NameValueCollection { { "text", text } };
				return Encoding.Default.GetString(client.UploadValues(web, "POST", values));
			}
		}
	}
	partial class Ohtapi
	{
		public ICreateTextResourceProvider CreateTextResourceProvider;
		/// <summary>
		/// Create new text resource file.
		/// </summary>
		/// <param name="text">text (text)</param>
		/// <returns></returns>
		public CreateTextResourceResult CreateTextResource(string text)
		{
			var r = new CreateTextResourceResult();
			try
			{
				if (CreateTextResourceProvider == null)
					CreateTextResourceProvider = new CreateTextResourceProvider();
				var json = CreateTextResourceProvider.Get(Url, _proxy, KeyPublic, KeySecret, text);
				r = JsonConvert.DeserializeObject<CreateTextResourceResult>(json);


			}
			catch (Exception err)
			{
				r.Status.Code = -1;
				r.Status.Msg = err.Message;
			}
			return r;
		}


	}


	public struct CreateTextResourceResult
	{
		[JsonProperty(PropertyName = "status")]
		public StatusType Status;
		[JsonProperty(PropertyName = "results")]
		public string[] Results;
		[JsonProperty(PropertyName = "errors")]
		public string[] Errors;

		public override string ToString()
		{
			return Status.Code == 0 ? Status.Msg : Status.Code + " " + Status.Msg;
		}
	}

}
