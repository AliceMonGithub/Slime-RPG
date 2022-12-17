using TMPro;
using UnityEngine;

namespace Sources.Factories
{
    internal class PopupFactory
    {
        private readonly TMP_Text _damagePopup;

        public PopupFactory(TMP_Text damagePopup)
        {
            _damagePopup = damagePopup;
        }

        public TMP_Text Create(Vector3 position, Transform parent)
        {
            return Object.Instantiate(_damagePopup, position, Quaternion.identity, parent);
        }
    }
}
