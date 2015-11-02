using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class MainMenuController : MonoBehaviour
    {
        public Canvas MainMenuHolder;
        public Button NewGame;
        public Button LoadButton;
        public Button Instructions;
        public Button Settings;
        public Button Credits;
        public Button Exit;
		public GameObject loginMenu;


        // Use this for initialization
        void Start ()
        {
            MainMenuHolder = GetComponent<Canvas>();
            NewGame = GetComponent<Button>();
            LoadButton = GetComponent<Button>();
            Instructions = GetComponent<Button>();
            Settings = GetComponent<Button>();
            Credits = GetComponent<Button>();
            Exit = GetComponent<Button>();

        }
	
        // Update is called once per frame
       void Update()
		{
			

		}

		

        void Awake()
        {
           

        }

        

        public void Pressed_NewGame()
        {
            Application.LoadLevel(9);
        }

        public void Load_Game()
        {
            Application.LoadLevel(7);
        }

        public void Pressed_Instructions()
        {
            Application.LoadLevel(2);
        }

        public void Pressed_Settings()
        {
            Application.LoadLevel(3);
        }

        public void Pressed_Credits()
        {
            Application.LoadLevel(4);
        }

        public void Pressed_Exit()
        {
            Application.Quit();
        }
    }
}
