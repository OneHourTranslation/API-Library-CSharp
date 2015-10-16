# API-Library-CSharp
One Hour Translation™ provides translation, proofreading and transcription services worldwide. The following API library allows customers to submit and monitor jobs automatically and remotely.

`More at website <https://www.onehourtranslation.com/translation/about-us>`_ 

Starters' Guide
---------------
First of all, you must to obtain private and public keys:

Register as a customer on One Hour Translation
Request your API Keys
or use sandbox for playing with API:

Register as a customer on One Hour Translation Sandbox
Request your snadbox API Keys
Almost each method of OhtApi class return namedtuple (with type oht_response) with such structure:

status:

code - request status code

msg - request status message, "ok" for OK

results - fields list depend on kind of query

errors - list of errors

For more details see doc comments for each method.

The API Library must be configured before calling any API method:

var _api = new Ohtapi(PublicKey, SecretKey, UseSandbox);

Where to go from here

Use doc comments in OhtApi class
See FormExamples.cs for code example
Visit official website API Developers Guide
