namespace WpfCctvMonitorApp.Common {
    public class GeoBound {

        public double MinLat { get; }   // MinY
        public double MaxLat { get; }   // MaxY
        public double MinLng { get; }   // MinX
        public double MaxLng { get; }   // MaxX

        public GeoBound(double minLat, double maxLat, double minLng, double maxLng)
        {
            MinLat = minLat;
            MaxLat = maxLat;
            MinLng = minLng;
            MaxLng = maxLng;
        }

        // TODO : 
        public static string BuildGetQueryString(string type)
        {
            return "";
        }



    }
}
