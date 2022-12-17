using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sources.Behaviour
{
    internal class MenuesConfigBehaviour : MonoBehaviour
    {
        public void LoadScene(string name)
        {
            SceneManager.LoadScene(name);
        }
    }
}
