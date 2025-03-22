using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class Crosshair : MonoBehaviour
    {
        public enum CrosshairSprites
        {
            Default,
            Interactable
        };

        [SerializeField] private Sprite _defaultCrosshair;
        [SerializeField] private Sprite _interactableCrosshair;

        public void ChangeIcon(CrosshairSprites sprite)
        {
            switch (sprite)
            {
                case CrosshairSprites.Default:
                    SetSprite(_defaultCrosshair);
                    break;
                case CrosshairSprites.Interactable:
                    SetSprite(_interactableCrosshair);
                    break;
            }
        }

        private void SetSprite(Sprite sprite)
        {
            this.GetComponent<Image>().sprite = sprite;
        }
    }
}