using UnityEngine;

namespace ARLocation
{

    /// <summary>
    /// This scriptable object holds the global configuration data for the AR + GPS
    /// Location plugin.
    /// </summary>
    [CreateAssetMenu(fileName = "ARLocationConfig", menuName = "AR+GPS/ARLocationConfig")]
    public class ARLocationConfig : ScriptableObject
    {

        public static string Version
        {
            get
            {
                return "v3.4.1";
            }
        }

        public enum ARLocationDistanceFunc
        {
            Haversine,
            PlaneSpherical,
            PlaneEllipsoidalFcc
        };

        [Tooltip("The earth radius, in kilometers, to be used in distance calculations.")]
        public double EarthRadiusInKM = 6372.8;

        [Tooltip("The distance function used to calculate geographical distances.")]
        public ARLocationDistanceFunc DistanceFunction = ARLocationDistanceFunc.Haversine;

        [Tooltip("The initial ground height guess, relative from the device position.")]
        [Range(0, 10)]
        public float InitialGroundHeightGuess = 1.4f;

        [Tooltip("The initial ground height guess, relative from the device position.")]
        [Range(0, 10)]
        public float MinGroundHeight = 0.4f;

        [Tooltip("The initial ground height guess, relative from the device position.")]
        [Range(0, 10)]
        public float MaxGroundHeight = 3.0f;

        [Tooltip("The distance between Vuforia ground plane hit tests. Lower will be more precise but will affect performance.")]
        public float VuforiaGroundHitTestDistance = 4.0f;

        [Tooltip("The smoothing factor for object height adjustments.")]
        [Range(0, 1)]
        public float GroundHeightSmoothingFactor = 0.05f;

        [Tooltip("If true, use Vuforia instead of ARFoundation.")]
        public bool UseVuforia;
    }
}
