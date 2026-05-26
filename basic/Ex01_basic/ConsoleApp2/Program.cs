// C#은 네임스페이스 내 동작
// Python에 import로 불러올 수 있는 패키지와 동일
namespace ConsoleApp2 {
    // C# OOP. 모든 것은 객체
    internal class Program {
        // 기본 진입점(EntryPoint) 메서드(C#은 함수라고 부르지 않음)
        /// <summary>
        /// Main 메서드
        /// class와 메서드에서만 사용가능
        /// </summary>
        /// <param name="args">콘솔명령 옵션 파라미터</param>
        static void Main(string[] args)
        {
            // 빌트인 클래스 콘솔 내의 WriteLine 메서드로 콘솔에 문자열을 출력
            Console.WriteLine("Hello, C#!");
        }
    }
}
