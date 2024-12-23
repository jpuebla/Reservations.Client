using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using Reservations.Backend.Models;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace Reservations.Client.Service
{

    public class ReservationService : IReservationService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        public ReservationService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<List<Shift>> GetShifts()
        {
            var url = _configuration.GetSection("ApiSettings:ApiBaseUrl").Value + "/Shift";

            List<Shift> returnResponse = new List<Shift>();

            using (HttpClient httpClient = _httpClientFactory.CreateClient())
            {
                var response = await httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string contentStr = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    returnResponse = JsonSerializer.Deserialize<List<Shift>>(contentStr, options);
                }
            }
            return returnResponse;
        }
        public async Task<List<RestaurantTable>> GetTables()
        {
            var url = _configuration.GetSection("ApiSettings:ApiBaseUrl").Value + "/Table";

            List<RestaurantTable> returnResponse = new List<RestaurantTable>();

            using (HttpClient httpClient = _httpClientFactory.CreateClient())
            {
                var response = await httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string contentStr = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    returnResponse = JsonSerializer.Deserialize<List<RestaurantTable>>(contentStr, options);
                }
            }
            return returnResponse;
        }
        public async Task<List<Reservation>> GetReservations(DateOnly date, int? shiftId, int? tableId)
        {
            var url = _configuration.GetSection("ApiSettings:ApiBaseUrl").Value + "/Reservation/" + date.ToString("yyyy-MM-dd");

            if (shiftId.HasValue)
            {
                url = url + "/" + shiftId.Value;

                if (tableId.HasValue)
                {
                    url = url + "/" + tableId.Value;
                }
            }

            List<Reservation> returnResponse = new List<Reservation>();

            using (HttpClient httpClient = _httpClientFactory.CreateClient())
            {
                var response = await httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string contentStr = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    returnResponse = JsonSerializer.Deserialize<List<Reservation>>(contentStr, options);
                }
            }
            return returnResponse;
        }
    }
}