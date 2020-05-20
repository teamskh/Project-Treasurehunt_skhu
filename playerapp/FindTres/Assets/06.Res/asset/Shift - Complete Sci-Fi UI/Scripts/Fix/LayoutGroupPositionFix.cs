using UnityEngine;

namespace Michsky.UI.Shift
{
    public class LayoutGroupPositionFix : MonoBehaviour
    {
        public void FixPos()
        {
            gameObject.SetActive(false);
            gameObject.SetActive(true);
        }
    }
}