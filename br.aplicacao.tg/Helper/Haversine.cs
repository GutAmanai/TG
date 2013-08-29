using br.dominio.tg.ObjetoValor;

namespace System
{
    public class Haversine
    {
        public static double Distance(Posicao pos1, Posicao pos2, DistanceUnit distanceUnit)
        {
            double R = (distanceUnit == DistanceUnit.Milhas) ? 3960 : 6371;
 
            double dLat = toRadian(pos2.Latitude - pos1.Latitude);
            double dLon = toRadian(pos2.Longitude - pos1.Longitude);
            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) + Math.Cos(toRadian(pos1.Latitude)) *Math.Cos(toRadian(pos2.Latitude)) * Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            double c = 2 * Math.Asin(Math.Min(1, Math.Sqrt(a)));
            double d = R * c;
 
            return d;
        }

        private static double toRadian(double val)
        {
            return (Math.PI / 180) * val;
        }
    }

    public enum DistanceUnit
    {
        Milhas,
        Kilometros
    };
}
