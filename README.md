# API-Library-CSharp
One Hour Translation™ provides translation, proofreading and transcription services worldwide. The following API library allows customers to submit and monitor jobs automatically and remotely.

[More at website](https://www.onehourtranslation.com/translation/about-us>)

#### Dependencies
C# Framework 4.0 >

## Starters' Guide ##
---------------
First of all, you must to obtain private and public keys:
#### Authentication 
Register as a customer on [One Hour Translation](http://www.onehourtranslation.com/auth/register).
Request your API Keys [here](http://www.onehourtranslation.com/profile/apiKeys).

#### Configuration ####
The API Library must be configured before calling any API method:

  #var _api = new Ohtapi(PublicKey, SecretKey, UseSandbox);#

#### Running Methods ####

 var r = _api.GetResource(<resource_uuid>);
 var file = r.Result.FileName;

Almost each method of OhtApi class return namedtuple (with type oht_response) with such structure:

**status:**

*code* - request status code

*msg* - request status message, "ok" for OK

**results** - fields list depend on kind of query

**errors** - list of errors

For more details see doc comments for each method.


Where to go from here

Use doc comments in OhtApi class
See FormExamples.cs for code example
[Visit official website API Developers Guide] Visit official website `API Developers Guide (https://www.onehourtranslation.com/translation/api-documentation-v2/general-instructions)
