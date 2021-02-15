using TechTalk.SpecFlow;
using System.Net;
using RestSharp;
using Newtonsoft.Json;
using System.Collections.Generic;
using NUnit.Framework;
using DemoAPI.Framework.Helper;
using DemoAPI.Framework.StatusCode;
using DemoAPI.Framework.API.EndPoint;
using DemoAPI.Framework.API_JsonObjects;

namespace API_Demo.Steps
{
    [Binding]
    public class API_Steps
    {
        private readonly ApiContent apiResponse;
        private readonly RestHelper restHelper = new RestHelper();
        private RestRequest apiRequest = new RestRequest();
        private readonly RestClient apiEndPoint = new RestClient();
        List<DemoAPI_Response> demoGetResponse;

        API_Steps(ApiContent _apiResponse)
        {
            this.apiResponse = _apiResponse;
        }

        [Given(@"that a Get Request for user '(.*)' is created")]
        public void GivenThatAGetRequestForUserIsCreated(string userId)
        {
            apiEndPoint.BaseUrl = new System.Uri(Demo_EndPoints.BasedURL());
            string userCall = Demo_EndPoints.GetEndPoint(userId);

            apiRequest = restHelper.SetupRequestMethod(userCall, Method.GET);
            apiResponse.Response = restHelper.GetResponse(apiRequest, apiEndPoint);

            System.Console.WriteLine("---- " + apiResponse.Response.Content);
            //Assert.AreEqual(_apiResponse.Response.StatusCode, HttpStatusCode.OK);
           
        }

        [When(@"the Get Request is made")]
        public void WhenTheGetRequestIsMade()
        {
            apiResponse.Response = restHelper.GetResponse(apiRequest, apiEndPoint);
            
            if (apiResponse.Response.StatusCode == HttpStatusCode.OK)
            {
                demoGetResponse = JsonConvert.DeserializeObject<List<DemoAPI_Response>>(apiResponse.Response.Content);
                System.Console.WriteLine("---- " + demoGetResponse);
            }
        }

        /// <summary>
        /// This method is to ensure, we are recieving the correct status
        /// </summary>
        /// <param name="statusCode"></param>
        [Then(@"the return status code is '(.*)'")]
        public void ThenTheReturnStatusCodeIs(string statusCode)
        {
            switch(statusCode)
            {
                case "OK":
                    RestHelper.AssertStatusCode(apiResponse.Response, HttpStatusCode.OK);
                    break;
                case "Not Found":
                    RestHelper.AssertStatusCode(apiResponse.Response, HttpStatusCode.NotFound);
                    break;
                default:
                    throw new System.ArgumentException("The Status Code entered is invalid");
            }
        }

        [Then(@"assert that return body with id set to '(.*)'")]
        public void ThenAssertThatReturnBodyWithIdSetTo(int userId)
        {
            //Assert.IsTrue(demoGetResponse.Foeach("id"));

           // foreach(DemoAPI_Response user in demoGetResponse)
           // { 
               // Assert.IsTrue(user. != userId, "[ERROR]: No 'id' was present in the response");
               // Assert.IsTrue(user.id == userId, "[ERROR]: Expected Id was not found");
            //}
        }


    }
}

