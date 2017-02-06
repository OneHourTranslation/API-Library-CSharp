using System;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace oht.lib
{
	public interface IAddTagProvider
	{
		string Get(string url, WebProxy proxy, string publicKey, string secretKey, string projectId, string tag);
	}
	public class AddTagProvider : IAddTagProvider
	{
		public string Get(string url, WebProxy proxy, string publicKey, string secretKey, string projectId, string tag)
		{
			using (var client = new WebClient())
			{
				if (proxy != null)
					client.Proxy = proxy;
				client.Encoding = Encoding.UTF8;
				var web = url + String.Format("/project/" + projectId + "/tag?public_key={0}&secret_key={1}", publicKey, secretKey);

				var values = new System.Collections.Specialized.NameValueCollection { { "tag_name", tag } };
				return Encoding.Default.GetString(client.UploadValues(web, "POST", values));
			}
		}
	}
	partial class Ohtapi
	{
		public IAddTagProvider AddTagProvider;
		/// <summary>
		/// Add new tag to a project
		/// </summary>
		/// <param name="projectId"></param>
		/// <param name="tag">tag string</param>
		/// <returns></returns>
		public AddTagResult AddTag(string projectId, string tag)
		{
			var r = new AddTagResult();
			try
			{
				if (AddTagProvider == null)
					AddTagProvider = new AddTagProvider();
				var json = AddTagProvider.Get(Url, _proxy, KeyPublic, KeySecret, projectId, tag);
				r = JsonConvert.DeserializeObject<AddTagResult>(json);


			}
			catch (Exception err)
			{
				r.Status.Code = -1;
				r.Status.Msg = err.Message;
			}
			return r;
		}


	}


	public struct AddTagResult
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

