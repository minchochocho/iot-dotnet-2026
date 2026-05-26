namespace Prac05Exception {
    internal class Program {
        static void Main(string[] args)
        {
            int x = 100, y = 0;
            float result;

            try
            {
                result = x / y;
            }
            catch (ArithmeticException ex)
            {
                Console.WriteLine("수학 예외발생 : " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("예외 발생 : " + ex.Message);
                throw;
            }
            finally
            {
                Console.WriteLine("프로그램 종료");
            }
        }
    }
}
