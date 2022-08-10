using StockMarket.StockMsgsProcessor.Services.Interfaces;

namespace StockMarket.StockMsgsProcessor.Services
{
    public class StooqWebDataService : IStooqService
    {
        private readonly HttpClient _httpClient;

        public StooqWebDataService(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(configuration.GetSection("StooqApiUrl").Value);

        }

        private async Task<string> GetCsvStockValuesByStockCode(string stock_code)
        {
            string uriResult = $"?s={stock_code}&f=sd2t2ohlcv&h&e=csv";
            return await _httpClient.GetStringAsync(uriResult);
        }

        public async Task<string> GetStockValueByCode(string stock_code) {
            var csvValues = await GetCsvStockValuesByStockCode(stock_code);

            var csvLines = csvValues.Split("\r\n");
            var closeValueIndex = csvLines[0].Split(",").ToList().IndexOf("Close");

            var stockValues = csvLines[1].Split(",");
            if (stockValues[1] == "N/D") {
                throw new Exception("Unknown Stock Code");
            }
            return csvLines[1].Split(",")[closeValueIndex];
        }
    }
}
