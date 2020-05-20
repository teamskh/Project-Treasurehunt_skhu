using UnityEngine;
using System;
using UnityEngine.Serialization;

namespace ARLocation
{
    public enum AltitudeMode {
        GroundRelative,
        DeviceRelative,
        Absolute,
        Ignore
    };

    /// <summary>
    /// Represents a geographical location.
    /// </summary>
    [Serializable]
    public class Location
    {
        [FormerlySerializedAs("latitude")] [Tooltip("The latitude, in degrees.")]
        public double Latitude;

        [FormerlySerializedAs("longitude")] [Tooltip("The longitude, in degrees.")]
        public double Longitude;

        [FormerlySerializedAs("altitude")] [Tooltip("The altitude, in meters.")]
        public double Altitude;

        [FormerlySerializedAs("altitudeMode")]
        [Space(4)]

        [Tooltip("The altitude mode. 'Absolute' means absolute altitude, relative to the sea level. 'DeviceRelative' meas it is " +
            "relative to the device's initial position. 'GroundRelative' means relative to the nearest detected plane, and 'Ignore' means the " +
            "altitude is ignored (equivalent to setting it to zero).")]
        public AltitudeMode AltitudeMode = AltitudeMode.GroundRelative;

        [FormerlySerializedAs("label")] [Tooltip("An optional label for the location.")]
        public string Label = "";

        public bool IgnoreAltitude => AltitudeMode == AltitudeMode.Ignore;

        /// <summary>
        /// Gets the horizontal vector.
        /// </summary>
        /// <value>The horizontal vector.</value>
        public DVector2 HorizontalVector => new DVector2(Latitude, Longitude);

        public Location(double latitude = 0.0, double longitude = 0.0, double altitude = 0.0)
        {
            Latitude = latitude;
            Longitude = longitude;
            Altitude = altitude;
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>The clone.</returns>
        public Location Clone()
        {
            return new Location()
            {
                Label = Label,
                Latitude = Latitude,
                Longitude = Longitude,
                Altitude = Altitude,
                AltitudeMode = AltitudeMode
            };
        }

        public override string ToString()
        {
            return "(" + Latitude + ", " + Longitude + ", " + Altitude + ")";
        }

        public DVector3 ToDVector3()
        {
            return new DVector3(Longitude, Altitude, Latitude);
        }

        public Vector3 ToVector3()
        {
            return ToDVector3().toVector3();
        }

        /// <summary>
        /// Calculates the horizontal distance according to the current function
        /// set in the configuration.
        /// </summary>
        /// <returns>The distance, in meters.</returns>
        /// <param name="l1">L1.</param>
        /// <param name="l2">L2.</param>
        public static double HorizontalDistance(Location l1, Location l2)
        {
            var type = ARLocation.Config.DistanceFunction;

            switch (type)
            {
                case ARLocationConfig.ARLocationDistanceFunc.Haversine:
                    return HaversineDistance(l1, l2);
                case ARLocationConfig.ARLocationDistanceFunc.PlaneSpherical:
                    return PlaneSphericalDistance(l1, l2);
                case ARLocationConfig.ARLocationDistanceFunc.PlaneEllipsoidalFcc:
                    return PlaneEllipsoidalFccDistance(l1, l2);
                default:
                    return HaversineDistance(l1, l2);
            }
        }

        /// <summary>
        /// Horizontal distance using spherical projection on a plane.
        /// https://en.wikipedia.org/wiki/Geographical_distance
        /// </summary>
        /// <returns>The distance, in meters.</returns>
        /// <param name="l1"></param>
        /// <param name="l2"></param>
        /// <returns></returns>
        public static double PlaneSphericalDistance(Location l1, Location l2)
        {
            var r = ARLocation.Config.EarthRadiusInKM;
            var rad = Math.PI / 180;
            var dLat = (l2.Latitude - l1.Latitude) * rad;
            var dLon = (l2.Longitude - l1.Longitude) * rad;
            var lat1 = l1.Latitude * rad;
            var lat2 = l2.Latitude * rad;
            var mLat = (lat1 + lat2) / 2.0;
            var mLatC = Math.Cos(mLat);

            var a = dLat * dLat;
            var b = mLatC * mLatC * dLon * dLon;

            return r * Math.Sqrt(a + b) * 1000.0;
        }

        /// <summary>
        /// Horizontal distance using ellipsoidal projection on a plane.
        /// https://en.wikipedia.org/wiki/Geographical_distance
        /// </summary>
        /// <returns>The distance, in meters.</returns>
        /// <param name="l1"></param>
        /// <param name="l2"></param>
        /// <returns></returns>
        public static double PlaneEllipsoidalFccDistance(Location l1, Location l2)
        {
            var rad = Math.PI / 180;
            var lat1 = l1.Latitude * rad;
            var lat2 = l2.Latitude * rad;
            var mLat = (lat1 + lat2) / 2.0;

            var k1 = 111.13209 - 0.56605 * Math.Cos(2 * mLat) + 0.00120 * Math.Cos(4 * mLat);
            var k2 = 111.41513 * Math.Cos(mLat) - 0.09455 * Math.Cos(3 * mLat) + 0.00012 * Math.Cos(5 * mLat);

            var a = k1 * (l2.Latitude - l1.Latitude);
            var b = k2 * (l2.Longitude - l1.Longitude);

            return 1000.0 * Math.Sqrt(a * a + b * b);
        }

        /// <summary>
        /// Horizontal distance, using the Haversine formula.
        /// https://stackoverflow.com/questions/41621957/a-more-efficient-haversine-function
        /// </summary>
        /// <returns>The distance, in meters.</returns>
        /// <param name="l1">L1.</param>
        /// <param name="l2">L2.</param>
        public static double HaversineDistance(Location l1, Location l2)
        {
            var r = ARLocation.Config.EarthRadiusInKM;
            var rad = Math.PI / 180;
            var dLat = (l2.Latitude - l1.Latitude) * rad;
            var dLon = (l2.Longitude - l1.Longitude) * rad;
            var lat1 = l1.Latitude * rad;
            var lat2 = l2.Latitude * rad;

            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) + Math.Sin(dLon / 2) * Math.Sin(dLon / 2) * Math.Cos(lat1) * Math.Cos(lat2);

            return r * 2 * Math.Asin(Math.Sqrt(a)) * 1000;
        }


        /// <summary>
        /// Calculates the full distance between locations, taking altitude into account.
        /// </summary>
        /// <returns>The with altitude.</returns>
        /// <param name="l1">L1.</param>
        /// <param name="l2">L2.</param>
        public static double DistanceWithAltitude(Location l1, Location l2)
        {
            var d = HorizontalDistance(l1, l2);
            var h = Math.Abs(l1.Altitude - l2.Altitude);

            return Math.Sqrt(d * d + h * h);
        }

        /// <summary>
        /// Calculates the horizontal vector pointing from l1 to l2, in meters.
        /// </summary>
        /// <returns>The vector from to.</returns>
        /// <param name="l1">L1.</param>
        /// <param name="l2">L2.</param>
        public static DVector2 HorizontalVectorFromTo(Location l1, Location l2)
        {
            var d = HorizontalDistance(l1, l2);

            var direction = (l2.HorizontalVector - l1.HorizontalVector).normalized;

            return direction * d;
        }

        /// <summary>
        /// Calculates the vector from l1 to l2, in meters, taking altitude into account.
        /// </summary>
        /// <returns>The from to.</returns>
        /// <param name="l1">L1.</param>
        /// <param name="l2">L2.</param>
        /// <param name="ignoreHeight">If true, y = 0 in the output vector.</param>
        public static DVector3 VectorFromTo(Location l1, Location l2, bool ignoreHeight = false)
        {
            var horizontal = HorizontalVectorFromTo(l1, l2);
            var height = l2.Altitude - l1.Altitude;

            return new DVector3(horizontal.y, ignoreHeight ? 0 : height, horizontal.x);
        }

        /// <summary>
        /// Gets the game object world-position for location.
        /// </summary>
        /// <param name="arLocationRoot"></param>
        /// <param name="userPosition"></param>
        /// <param name="userLocation"></param>
        /// <param name="objectLocation"></param>
        /// <param name="heightIsRelative"></param>
        /// <returns></returns>
        public static Vector3 GetGameObjectPositionForLocation(Transform arLocationRoot, Vector3 userPosition, Location userLocation, Location objectLocation, bool heightIsRelative)
        {
            var displacementVector = VectorFromTo(userLocation, objectLocation, objectLocation.IgnoreAltitude || heightIsRelative)
                .toVector3();

            var displacementPosition = arLocationRoot ? arLocationRoot.TransformVector(displacementVector) : displacementVector;

            return userPosition + displacementPosition + new Vector3(0, (heightIsRelative && !objectLocation.IgnoreAltitude) ? ((float)objectLocation.Altitude - userPosition.y) : 0, 0);
        }

        /// <summary>
        /// Gets the game object world-position for location.
        /// </summary>
        /// <returns>The game object position for location.</returns>
        /// <param name="arLocationRoot"></param>
        /// <param name="user">User.</param>
        /// <param name="userLocation">User location.</param>
        /// <param name="objectLocation">Object location.</param>
        /// <param name="heightIsRelative">If set to <c>true</c> height is relative.</param>
        ///
        public static Vector3 GetGameObjectPositionForLocation(Transform arLocationRoot, Transform user, Location userLocation, Location objectLocation, bool heightIsRelative)
        {
            return GetGameObjectPositionForLocation(arLocationRoot, user.position, userLocation, objectLocation,
                heightIsRelative);
        }

        /// <summary>
        /// Places the game object at location.
        /// </summary>
        /// <param name="arLocationRoot"></param>
        /// <param name="transform">The GameObject's transform.</param>
        /// <param name="user">The user's point of view Transform, e.g., camera.</param>
        /// <param name="userLocation">User Location.</param>
        /// <param name="objectLocation">Object Location.</param>
        /// <param name="heightIsRelative"></param>
        public static void PlaceGameObjectAtLocation(Transform arLocationRoot, Transform transform, Transform user, Location userLocation, Location objectLocation, bool heightIsRelative)
        {
            transform.position = GetGameObjectPositionForLocation(arLocationRoot, user, userLocation, objectLocation, heightIsRelative);
        }

        public static bool Equal(Location a, Location b, double eps = 0.0000001)
        {
            return (Math.Abs(a.Latitude - b.Latitude) <= eps) &&
                (Math.Abs(a.Longitude - b.Longitude) <= eps) &&
                (Math.Abs(a.Altitude - b.Altitude) <= eps);
        }
    }
}
