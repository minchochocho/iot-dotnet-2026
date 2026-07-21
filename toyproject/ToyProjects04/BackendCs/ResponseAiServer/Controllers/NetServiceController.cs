using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace ResponseAiServer.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class NetServiceController : ControllerBase {
        private readonly IHttpClientFactory _httpClientFactory;

        public NetServiceController(IHttpClientFactory httpClientFactory) {
            _httpClientFactory = httpClientFactory;
        }

        [HttpPost]
        [Route("/net_service")]
        public async Task<IActionResult> ProxyRequest([FromForm] string? message, [FromForm] IFormFile? file) {
            // 1. 파일 선택 안했으면
            if (file == null || file.Length == 0) {
                return BadRequest(new { message = "파일을 선택하세요." });
            }

            // 2. Program.cs에 등록한 PythonAI 서버 이름으로 클라이언트 생성
            var client = _httpClientFactory.CreateClient("PythonAiServer");

            // 3. Python RestAPI로 전달할 데이터 할당
            using var content = new MultipartFormDataContent();

            // 3-1. Request Body 중 message 키할당
            content.Add(new StringContent(message ?? string.Empty), "message");

            // 3-2. Request Body 중 file 키할당
            using var stream = file.OpenReadStream();
            var fileContent = new StreamContent(stream);
            var contentType = string.IsNullOrWhiteSpace(file.ContentType)
                ? "application/octet-stream"
                : file.ContentType;
            fileContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
            content.Add(fileContent, "file", file.FileName);

            // 4. Python API로 Post 요청
            HttpResponseMessage response;
            try {
                response = await client.PostAsync("/detect", content);
            }
            catch (HttpRequestException ex) {
                // Python 서버(8080)가 꺼져 있으면 여기서 발생 → 기존엔 500 Internal Server Error
                return StatusCode(503, new {
                    message = "파이썬 AI 서버(http://127.0.0.1:8080)에 연결할 수 없습니다. main05.py를 실행하세요.",
                    detail = ex.Message
                });
            }

            if (!response.IsSuccessStatusCode) {
                return StatusCode((int)response.StatusCode, "파이썬 AI 서비스 호출 실패!");
            }

            // 5. 돌아온 결과를 읽어서 json으로 출력
            var result = await response.Content.ReadAsStringAsync();

            return Content(result, "application/json");
        }

    }
}
