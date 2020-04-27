// Filename:  HttpServer.cs        
// Author:    Benjamin N. Summerton <define-private-public>        
// License:   Unlicense (http://unlicense.org/)

using System;
using System.IO;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OnlineControl.EntityPosDataModel;
using System.Collections.Generic;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;
using System.Diagnostics;
using System.Web.Script.Serialization;
using TaleWorlds.CampaignSystem;
using System.Web;

namespace OnlineControl
{
	class HttpServer
	{

		private string ModuleFolder = "OnlineControl";
		private string url = "http://localhost:8000/";
		string indexName = "/interface.htm";

		private HttpListener listener;
		private int pageViews = 0;
		private int requestCount = 0;
		private string pageData;


		private async Task DoResponse(byte[] data, HttpListenerResponse resp)
		{
			resp.ContentType = "text/html";
			resp.ContentEncoding = Encoding.UTF8;
			resp.ContentLength64 = data.LongLength;
			await resp.OutputStream.WriteAsync(data, 0, data.Length);
			resp.Close();
		}

		private bool CheckAuth(string password)
		{
			string truePassword = ReadFile("/password.txt");
			Debug.Print("login attempt with " + password);
			Debug.Print("true password is " + truePassword);
			return truePassword == password;
		}

		private async Task DoJsonResponse(string json, HttpListenerResponse resp)
		{
			byte[] data = Encoding.UTF8.GetBytes(json);
			await DoResponse(data, resp);
		}

		//https://gist.github.com/leggetter/769688
		private string GetDocumentContents(HttpListenerRequest Request)
		{
			string documentContents;
			using (Stream receiveStream = Request.InputStream)
			{
				using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
				{
					documentContents = readStream.ReadToEnd();
				}
			}
			return documentContents;
		}

		private async Task HandleIncomingConnections()
		{
			EntityController lordController = new EntityController();
			bool runServer = true;

			// While a user hasn't visited the `shutdown` url, keep on handling requests
			while (runServer)
			{


				bool hasResponded = false;
				// Will wait here until we hear from a connection
				HttpListenerContext ctx = await listener.GetContextAsync();

				// Peel out the requests and response objects
				HttpListenerRequest req = ctx.Request;
				HttpListenerResponse resp = ctx.Response;

				var passwordResponse = req.Cookies[0].Value;

				if (CheckAuth(passwordResponse)) //TODO: Authentication
				{
					// Print out some info about the request
					/*Console.WriteLine("Request #: {0}", ++requestCount);
					Console.WriteLine(req.Url.ToString());
					Console.WriteLine(req.HttpMethod);
					
					Console.WriteLine(req.UserHostName);
					Console.WriteLine(req.UserAgent);
					Console.WriteLine();*/

					Debug.Print("New request:");
					Debug.Print(req.Url.AbsolutePath);
					Debug.Print(req.HttpMethod);

					if ((req.HttpMethod == "GET") && (req.Url.AbsolutePath == "/shutdown"))
					{
						runServer = false;
					}

					if ((req.HttpMethod == "POST") && (req.Url.AbsolutePath == "/stagechanges"))
					{
						var successString = "";
						Debug.Print("Stage changes request");
						try
						{
							string documentContents = GetDocumentContents(req);
							List<EntityActionRequest> userRequest = JsonConvert.DeserializeObject<List<EntityActionRequest>>(documentContents);
							lordController.ProcessRequest(userRequest);
							Debug.Print("Deserialize success");
							successString = "OK";
						}
						catch
						{
							Debug.Print("Couldn't deserialize request");
							Debug.Print(GetDocumentContents(req));
							successString = "Bad JSON";
						}
						hasResponded = true;
						await DoResponse(Encoding.UTF8.GetBytes(successString), resp);
					}


					if (req.HttpMethod == "GET")
					{
						hasResponded = true;
						switch (req.Url.AbsolutePath) //TODO: so much reused code. Can we use delegates?
						{
							case "/lords":
								await DoJsonResponse(JsonConvert.SerializeObject(new Dictionary<string, LordPartyData>(new EntityDataScraper().GenerateLordData())), resp);
								break;
							case "/mobileparties":
								await DoJsonResponse(JsonConvert.SerializeObject(new Dictionary<string, MobilePartyData>(new EntityDataScraper().GenerateMobilePartyData())), resp);
								break;
							case "/settlements":
								await DoJsonResponse(JsonConvert.SerializeObject(new Dictionary<string, SettlementData>(new EntityDataScraper().GenerateSettlementData())), resp);
								break;
							case "/kingdoms":
								await DoJsonResponse(JsonConvert.SerializeObject(new Dictionary<string, KingdomData>(new EntityDataScraper().GenerateKingdomData())), resp);
								break;
							case "/clans":
								await DoJsonResponse(JsonConvert.SerializeObject(new Dictionary<string, ClanData>(new EntityDataScraper().GenerateClanData())), resp);
								break;
							case "/test":
								await DoJsonResponse(JsonConvert.SerializeObject(Hero.All), resp);
								break;
							default:
								hasResponded = false;
								break;
						}
					}



					// Make sure we don't increment the page views counter if `favicon.ico` is requested
					if (req.Url.AbsolutePath != "/favicon.ico")
					{

						//lordController.gotRequest();
						pageViews += 1;
					}


					// Write the response info
					if (!hasResponded)
					{
						// Write out to the response stream (asynchronously), then close it
						string disableSubmit = !runServer ? "disabled" : "";
						string filePath = GetFilePath(req.RawUrl);
						string readFile = ReadFile(req.RawUrl);

						await DoResponse(Encoding.UTF8.GetBytes(ReadFile(req.RawUrl)), resp);
					}
				}
				else
				{
					await DoResponse(Encoding.UTF8.GetBytes(ReadFile("/login.htm")), resp);
				}
			}
		}
		private string GetFilePath(string file)
		{
			string newFileName = file;
			if (file.Equals("/") || file.Equals("") || file.Equals(" "))
			{
				newFileName = indexName;
			}

			return "../../Modules/" + ModuleFolder + "/web" + newFileName;
		}
		private string ReadFile(string file)
		{
			try
			{
				return System.IO.File.ReadAllText(GetFilePath(file));
			}
			catch
			{
				return System.IO.File.ReadAllText(GetFilePath(indexName));
			}
		}
		public void StartListener()
		{
			//pageData = System.IO.File.ReadAllText("../../Modules/" + ModuleFolder + "" + file);
			// Create a Http server and start listening for incoming connections
			listener = new HttpListener();
			listener.Prefixes.Add(url);
			listener.Start();
			Console.WriteLine("Listening for connections on {0}", url);

			// Handle requests
			Task listenTask = HandleIncomingConnections();
			listenTask.GetAwaiter().GetResult();

			// Close the listener
			StopListener();
		}

		public void StopListener()
		{
			listener.Close();
		}

		private class ContentResult
		{
			public string Content { get; set; }
			public string ContentType { get; set; }
		}
	}
}