using System.Globalization;
using CsvHelper;
using StockMarket.StockMsgsProcessor.Services.Interfaces;
using StockMarket.StockMsgsProcessorService.Models;

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

        public async Task<StockValue> GetStockValueByCode(string stock_code)
        {
            var csvValues = await GetCsvStockValuesByStockCode(stock_code);

            using (var reader = new StringReader(csvValues))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Read();
                    csv.ReadHeader();

                    csv.Read();

                    try
                    {
                        var stockValue = csv.GetRecord<StockValue>();
                        return stockValue;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Invalid Stock Code.");
                        return null;
                    }
                }
            }
            return null;
        }
    }
    
}
