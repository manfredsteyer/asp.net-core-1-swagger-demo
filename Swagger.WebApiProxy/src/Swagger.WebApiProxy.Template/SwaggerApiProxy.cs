


using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;

// ReSharper disable All

namespace SwaggerServer{
/// <summary>
/// Web Proxy for Flug
/// </summary>
public class FlugWebProxy : Swagger.WebApiProxy.Template.BaseProxy
{
public FlugWebProxy(Uri baseUrl) : base(baseUrl)
{}
					// helper function for building uris. 
					private string AppendQuery(string currentUrl, string paramName, string value)
					{
						if (currentUrl.Contains("?"))
							currentUrl += string.Format("&{0}={1}", paramName, Uri.EscapeUriString(value));
						else
							currentUrl += string.Format("?{0}={1}", paramName, Uri.EscapeUriString(value));
						return currentUrl;
					}
				/// <summary>
/// 
/// </summary>
public async Task<List<Flug>> ApiFlugGet ()
{
var url = "api/Flug";

using (var client = BuildHttpClient())
{
var response = await client.GetAsync(url).ConfigureAwait(false);
await EnsureSuccessStatusCodeAsync(response);
return await response.Content.ReadAsAsync<List<Flug>>().ConfigureAwait(false);
}
}
/// <summary>
/// 
/// </summary>
/// <param name="flug"></param>
public async Task ApiFlugPost (Flug flug = null)
{
var url = "api/Flug";

using (var client = BuildHttpClient())
{
var response = await client.PostAsJsonAsync(url, flug).ConfigureAwait(false);
await EnsureSuccessStatusCodeAsync(response);
}
}
/// <summary>
/// 
/// </summary>
/// <param name="von"></param>
/// <param name="nach"></param>
public async Task<List<Flug>> ApiFlugByRouteGet (string von = null, string nach = null)
{
var url = "api/Flug/byRoute";

using (var client = BuildHttpClient())
{
var response = await client.GetAsync(url).ConfigureAwait(false);
await EnsureSuccessStatusCodeAsync(response);
return await response.Content.ReadAsAsync<List<Flug>>().ConfigureAwait(false);
}
}
/// <summary>
/// 
/// </summary>
/// <param name="id"></param>
public async Task<Flug> ApiFlugByIdGet (int id)
{
var url = "api/Flug/{id}"
	.Replace("{id}", id.ToString());

using (var client = BuildHttpClient())
{
var response = await client.GetAsync(url).ConfigureAwait(false);
await EnsureSuccessStatusCodeAsync(response);
return await response.Content.ReadAsAsync<Flug>().ConfigureAwait(false);
}
}
}
public class Flug 
{
public int id { get; set; }
public string ablugOrt { get; set; }
public string zielOrt { get; set; }
public DateTime datum { get; set; }
public string flugNr { get; set; }
public List<Buchung> buchungen { get; set; }
}
public class Buchung 
{
public int buchungId { get; set; }
public DateTime datum { get; set; }
public int flugId { get; set; }
public Flug flug { get; set; }
public int passagierId { get; set; }
}
}
        

    