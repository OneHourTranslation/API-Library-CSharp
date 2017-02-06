
using System;
using System.Reflection;

namespace oht.lib
{
    public enum StringCurrency
    {
        [StringValue("")] None,
        [StringValue("USD")] USD,
        [StringValue("EUR")] EUR,
		[StringValue("GBP")] GBP
    }

    public enum StringAvailability
    {
        [StringValue("")] None,
        [StringValue("high")] High,
        [StringValue("medium")] Medium,
        [StringValue("low")] Low
    }

    public enum StringProjectType
    {
        [StringValue("")] None,
        [StringValue("Translation")] Translation,
        [StringValue("Expert Translation")] ExpertTranslation,
        [StringValue("Proofreading")] Proofreading,
        [StringValue("Transcription")] Transcription,
		[StringValue("Translation+Proofreading")] TranslationProofreading
    }

	public enum StringRatingCategoriesType
	{
		[StringValue("service_was_on_time")] ServiceWasOnTime,
		[StringValue("service_support_helpful")] ServiceSupportHelpful,
		[StringValue("service_good_quality")] ServiceGoodQuality,
		[StringValue("service_trans_responded")] ServiceTransResponded,
		[StringValue("service_would_recommend")] ServiceWouldRecommend,
		[StringValue("trans_is_good")] TransIsGood,
		[StringValue("trans_bad_formatting")] TransBadFormatting,
		[StringValue("trans_misunderstood_source")] TransMisunderstoodSource,
		[StringValue("trans_spell_tps_grmr_mistakes")] TransSpellTpsGrmrMistakes,
		[StringValue("trans_text_miss")] TransTextMiss,
		[StringValue("trans_not_followed_instrctns")] TransNotFollowedInstrctns,
		[StringValue("trans_inconsistent")] TransInconsistent,
		[StringValue("trans_bad_written")] TransBadWritten
	}

	public enum StringExpertiseType
	{
		[StringValue("")] None,
		[StringValue("automotive-aerospace")] Automotive_Aerospace,
		[StringValue("business-finance")] Business_Forex_Finance,
		[StringValue("software-it")] Software_IT,
		[StringValue("legal-certificate")] Legal,
		[StringValue("marketing-consumer-media")] Marketing_Consumer_Media,
		[StringValue("cv")] CV,
		[StringValue("medical")] Medical,
		[StringValue("patents")] Patents,
		[StringValue("scientific-academic")] Scientific_Academic,
		[StringValue("technical-engineering")] Technical_Engineering,
		[StringValue("gaming-video-games")] Gaming_Video_Games,
		[StringValue("ad-words-banners")] Ad_Words_Banners,
		[StringValue("mobile-applications")] Mobile_Applications,
		[StringValue("tourism")] Tourism,
		[StringValue("certificates-translation")] Certificates_Translation
	}

    public enum StringProjectStatusCode
    {
        [StringValue("")]
        None,
        [StringValue("Pending")]
        Pending,
        [StringValue("in_progress")]
        InProgress,
        [StringValue("submitted")]
        Submitted,
        [StringValue("signed")]
        Signed,
        [StringValue("completed")]
        Completed,
        [StringValue("canceled")]
        Canceled
    }

    public enum StringCommenterRole
    {
        [StringValue("")]
        None,
        [StringValue("admin")]
        Admin,
        [StringValue("customer")]
        Customer,
        [StringValue("provider")]
        Provider,
        [StringValue("potential-provider")]
        PotentialProvider
    }

    public enum StringType
    {
        [StringValue("")]
        None,
        [StringValue("customer")]
        Customer,
        [StringValue("service")]
        Service
    }

    public enum StringTypeFile
    {
        [StringValue("")]
        None,
        [StringValue("text")]
        Text,
        [StringValue("file")]
        File
    }

    public enum StringService
    {
        [StringValue("")]
        None,
        [StringValue("translation")]
        Translation,
        [StringValue("proofreading")]
        Proofreading,
        [StringValue("transproof")]
        Transproof,
        [StringValue("transcription")]
        Transcription
    }

    public enum StringProofreading
    {
        [StringValue("")]
        None,
        [StringValue("0")]
        No,
        [StringValue("1")]
        Yes
    }

    public class StringValue : Attribute
    {
        private string _value;

        public StringValue(string value)
        {
            _value = value;
        }

        public string Value
        {
            get { return _value; }
        }
    }
    public static class StringEnum
    {
        public static string GetStringValue<TEnum>(this TEnum value)
        {
            string output = null;
            Type type = value.GetType();

            FieldInfo fi = type.GetField(value.ToString());
            StringValue[] attrs =
               fi.GetCustomAttributes(typeof(StringValue),
                                       false) as StringValue[];
            if (attrs != null && attrs.Length > 0)
            {
                output = attrs[0].Value;
            }

            return output;
        }
    }

}
