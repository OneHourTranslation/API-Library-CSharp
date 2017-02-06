using System;
using System.Net;
using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace oht.lib
{
    public interface IPostProjectRatingsProvider
    {
		string Get(string url, WebProxy proxy, string publicKey, string secretKey, string projectId, StringType type, string rate, string remarks = "", string publish = "", Dictionary<oht.lib.StringRatingCategoriesType, string> categories = null);
    }
    public class PostProjectRatingsProvider : IPostProjectRatingsProvider
    {
		public string Get(string url, WebProxy proxy, string publicKey, string secretKey, string projectId, StringType type, string rate, string remarks = "", string publish = "", Dictionary<oht.lib.StringRatingCategoriesType, string> categories = null)
        {
            using (var client = new WebClient())
            {
                if (proxy != null)
                    client.Proxy = proxy;
                client.Encoding = Encoding.UTF8;
				var web = url + String.Format("/projects/" + projectId + "/rating?public_key={0}&secret_key={1}", publicKey, secretKey);

				var values = new System.Collections.Specialized.NameValueCollection 
				{ 
					{"type", type.GetStringValue()},
					{"rate", rate},
					{"remarks", remarks},
					{"publish", publish}

				};

				if (categories != null) {
					foreach (oht.lib.StringRatingCategoriesType key in categories.Keys) {
					
						values.Add ("categories[" + key.GetStringValue() + "]", categories[key]);
					}
				}

				return Encoding.Default.GetString(client.UploadValues(web, "POST", values));
            }
        }
    }

    partial class Ohtapi
    {
        public IPostProjectRatingsProvider PostProjectRatingsProvider;
        /// <summary>
        /// Submit the rating for the quality of the translation and service
        /// </summary>
        /// <param name="projectId">Unique id of the newly project created</param>
        /// <param name="type">Customer|Service</param>
        /// <param name="rate">Rating of project (1 - being the lowest; 10 - being the highest)</param>
        /// <param name="remarks">Remark left with the rating</param>
		/// <param name="publish">Allow OneHourTranslation to publish review on Yotpo. Not publishing by default</param>
		/// <param name="categories">[Optional]{Dictionary}Extra Service / translation rating values provided via checkboxes.
		/// 	Use StringRatingCategoriesType enums.
		/// 
		///		Service rating must provide fields (all are boolean 0 | 1 values):
		///			service_was_on_time - Service on time, or took too long
		///			service_support_helpful - The support was helpful
		///			service_good_quality - The service was with good quality?
		///			service_trans_responded - Slowly / Quickly
		///			service_would_recommend - The site would be recommended

		///		Translation rating must provide fields (all are boolean 0 | 1 values):
		///			trans_is_good - Good translation quality
		///			trans_bad_formatting - Bad formatting
		///			trans_misunderstood_source - source misunderstood/misrepresent
		///			trans_spell_tps_grmr_mistakes - Spelling/Typos/Grammer mistakes
		///			trans_text_miss - Missing text / Partly untranslated
		///			trans_not_followed_instrctns - Translator didn't follow instructions
		///			trans_inconsistent - Translation is inconsistent
		///			trans_bad_written - Translation written badly, or too literal

		///			Example of passing categories parameters:
		/// 		New dictionary:
		/// 		var dict = new Dictionary<oht.lib.StringRatingCategoriesType, string>();
		/// 
		///			rating_type - customer:
		///				dict.Add (oht.lib.StringRatingCategoriesType.TransBadFormatting, "1");
		///				dict.Add (oht.lib.StringRatingCategoriesType.TransMisunderstoodSource, "0");
		///			rating_type - service:
		///				dict.Add (oht.lib.StringRatingCategoriesType.ServiceTransResponded, "1");
		///				dict.Add (oht.lib.StringRatingCategoriesType.ServiceGoodQuality, "1");
        /// <returns></returns>
		public PostProjectRatingsResult PostProjectRatings(string projectId, StringType type,string rate, string remarks="", string publish = "", Dictionary<oht.lib.StringRatingCategoriesType, string> categories = null)
        {
            var r = new PostProjectRatingsResult();
            try
            {
                if (PostProjectRatingsProvider == null)
                    PostProjectRatingsProvider = new PostProjectRatingsProvider();
				var json = PostProjectRatingsProvider.Get(Url, _proxy, KeyPublic, KeySecret, projectId, type, rate, remarks, publish, categories);
                r = JsonConvert.DeserializeObject<PostProjectRatingsResult>(json);
            }
            catch (Exception err)
            {
                r.Status.Code = -1;
                r.Status.Msg = err.Message;
            }
            return r;
        }

    }


    public struct PostProjectRatingsResult
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
