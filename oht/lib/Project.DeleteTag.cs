using System;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace oht.lib
{
	public interface IDeleteTagProvider
	{
		string Get(string url, WebProxy proxy, string publicKey, string secretKey, int projectId, int tagId);
	}
	public class DeleteTagProvider : IDeleteTagProvider
	{
		public string Get(string url, WebProxy proxy, string publicKey, string secretKey, int projectId, int tagId)
		{
			using (var client = new WebClient())
			{
				if (proxy != null)
					client.Proxy = proxy;
				client.Encoding = Encoding.UTF8;
				var web = url + String.Format("/project/" + projectId + "/tag/" + tagId + "?public_key={0}&secret_key={1}", publicKey, secretKey);

				return client.UploadString(web, "DELETE", "");
			}
		}
	}
	partial class Ohtapi
	{
		public IDeleteTagProvider DeleteTagProvider;
		/// <summary>
		/// Delete a specific tag in project
		/// </summary>
		/// <param name="projectId"></param>
		/// <param name="tagId">tag id, can be found with API call GetProjectTags</param>
		/// <returns></returns>
		public DeleteTagResult DeleteTag(int projectId, int tagId)
		{
			var r = new DeleteTagResult();
			try
			{
				if (DeleteTagProvider == null)
					DeleteTagProvider = new DeleteTagProvider();
				var json = DeleteTagProvider.Get(Url, _proxy, KeyPublic, KeySecret, projectId, tagId);
				r = JsonConvert.DeserializeObject<DeleteTagResult>(json);


			}
			catch (Exception err)
			{
				r.Status.Code = -1;
				r.Status.Msg = err.Message;
			}
			return r;
		}


	}


	public struct DeleteTagResult
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
