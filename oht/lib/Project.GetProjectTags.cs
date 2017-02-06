using System;
using System.Linq;
using System.Net;
using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace oht.lib
{
	public interface IGetProjectTagsProvider
	{
		string Get(string url, WebProxy proxy, string publicKey, string secretKey, string projectId);
	}
	public class GetProjectTagsProvider : IGetProjectTagsProvider
	{
		public string Get(string url, WebProxy proxy, string publicKey, string secretKey, string projectId)
		{
			using (var client = new WebClient())
			{
				if (proxy != null)
					client.Proxy = proxy;
				client.Encoding = Encoding.UTF8;
				var web = url + String.Format("/project/" + projectId + "/tag?public_key={0}&secret_key={1}", publicKey, secretKey);
				return client.DownloadString(web);
			}
		}
	}
	partial class Ohtapi
	{
		public IGetProjectTagsProvider GetProjectTagsProvider;
		/// <summary>
		/// View project tags
		/// </summary>
		/// <param name="projectId"></param>
		/// <returns></returns>
		public GetProjectTagsResult GetProjectTags(string  projectId)
		{
			var r = new GetProjectTagsResult();
			try
			{
				if (GetProjectTagsProvider == null)
					GetProjectTagsProvider = new GetProjectTagsProvider();
				var json = GetProjectTagsProvider.Get(Url, _proxy, KeyPublic, KeySecret, projectId);
				r = JsonConvert.DeserializeObject<GetProjectTagsResult>(json);
			}
			catch (Exception err)
			{
				r.Status.Code = -1;
				r.Status.Msg = err.Message;
			}
			return r;
		}
	}


	public struct GetProjectTagsResult
	{
		[JsonProperty(PropertyName = "status")]
		public StatusType Status;
		/// <summary>
		/// Dictionary of tag id and tag string.
		/// </summary>
		[JsonProperty(PropertyName = "results")]
		public Dictionary<int, string> Results;
		[JsonProperty(PropertyName = "errors")]
		public string[] Errors;

		public override string ToString()
		{
			return Status.Code == 0 ? Status.Msg : Status.Code + " " + Status.Msg;
		}
	}
}

