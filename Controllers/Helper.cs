using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Newtonsoft.Json.Linq;
using remote_poc_webapi.Controllers;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace remote_poc_webapi.Helpers
{
    public class Helper
    {
        public static readonly string apiKey = "AIzaSyBlpCd4M34Hfxc-VRLXwUUZ6-pKFnKZA14";
        public static (double, double) GetRouteDetails(double originLat, double originLng, double destinationLat, double destinationLng,string travelMode)
        {
            string url = "https://routes.googleapis.com/directions/v2:computeRoutes";

            string jsonPayload = $"{{ \"origin\": {{ \"location\": {{ \"latLng\": {{ \"latitude\": {originLat}, \"longitude\": {originLng} }} }} }}, \"destination\": {{ \"location\": {{ \"latLng\": {{ \"latitude\": {destinationLat}, \"longitude\": {destinationLng} }} }} }}, \"travelMode\": \"{travelMode}\" }}  ";

            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("X-Goog-Api-Key", apiKey);
            client.DefaultRequestHeaders.Add("X-Goog-FieldMask", "routes.distanceMeters,routes.duration");

            HttpContent content = new StringContent(jsonPayload, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            response.EnsureSuccessStatusCode();
            string responseBody = response.Content.ReadAsStringAsync().Result;

            JObject jsonResponse = JObject.Parse(responseBody);
            var distanceInMeters = jsonResponse?["routes"]?[0]?["distanceMeters"]?.ToObject<double>();
            var sdurationInSeconds = jsonResponse?["routes"]?[0]?["duration"]?.ToObject<string>().Replace("s","");
            double durationInSeconds = double.Parse(sdurationInSeconds);

            double distanceKm = distanceInMeters.HasValue ? distanceInMeters.Value / 1000.0 : 0.0;
            double durationMinutes = durationInSeconds/ 60.0;

            return (distanceKm, durationMinutes);
        }
        

    }
}