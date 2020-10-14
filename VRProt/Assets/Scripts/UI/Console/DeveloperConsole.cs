using TMPro;
using UnityEngine;

namespace UI.Console
{
    public class DeveloperConsole : MonoBehaviour
    {
        #region Singleton
        public static DeveloperConsole Instance
        {
            get
            {
                if (_instance!= null) return _instance;
                _instance = FindObjectOfType<DeveloperConsole>();
                return _instance!= null ? _instance : CreateNewInstance();
            }
        }
        private static DeveloperConsole _instance;
        private static DeveloperConsole CreateNewInstance()
        {
            var prefab = Resources.Load<DeveloperConsole>("Prefabs/Managers/DeveloperConsole");
            _instance = Instantiate(prefab);
            return _instance;
        }
		
        private void Awake()
        {
            if (Instance != this)
            {
                Destroy(gameObject);
            }
        }
        #endregion
    
        [SerializeField] private TextMeshProUGUI textField;

        public void AddLine(string line)
        {
            textField.text += " \n " +line;
            Debug.Log(line);
        }
    }
}