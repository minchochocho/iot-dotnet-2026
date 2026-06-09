using MySqlConnector;
using System.Data;

namespace WpfCafeKiosk.Common {
    class DatabaseHelper {
        // MySQL 연결문자열 key=value;
        private string connStr = "Server=localhost;" +  // 운영아이피로 바꾸세요
                                 "Port=3306;" +         // 운영포트로 변경할 것
                                 "Database=cafekiosk;" +
                                 "User ID=root;" +      // 운영DB 사용자로 변경
                                 "Password=my123456;" + // 패스워드 변경할 것
                                 "Charset=utf8mb4;";
        public DataTable Select(string sql)
        {
            using MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();  // DB 오픈

            using MySqlCommand cmd = new MySqlCommand(sql, conn);
            using MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

            DataTable dt = new DataTable();
            adapter.Fill(dt);

            return dt;
        }
    }
}
