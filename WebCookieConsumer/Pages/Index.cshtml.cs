using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Primitives;
using System.Diagnostics;
using System.Net;

namespace WebCookieConsumer.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private static HttpClientHandler _handler;
        private static HttpClient _httpClient;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
            if(_httpClient == null)
            {
                _handler = new HttpClientHandler
                {
                    CookieContainer = new CookieContainer(),
                    UseCookies = true,
                    UseDefaultCredentials = true
                };

                _httpClient = new HttpClient(_handler)
                {
                    BaseAddress = new Uri("https://localhost:44339")
                };
            }
        }

        public async Task OnGetAsync()
        {
            var response = await _httpClient.GetAsync("api/clientinfo");

            var responseCookies = response.Headers.GetValues("Set-Cookie");
            foreach( var cookie in responseCookies)
            {
                Debug.WriteLine($"Cookie from response.Headers: {cookie}");
            }

            var cookieCount = _handler.CookieContainer.Count;
            Debug.WriteLine($"Handler {nameof(_handler.CookieContainer)} found {cookieCount} cookies");

            var countainerCookies = _handler.CookieContainer.GetAllCookies();
            foreach(Cookie cookie in countainerCookies)
            {
                Debug.WriteLine($"{cookie.Name}: {cookie.Value}");
            }
        }
    }
}
