using UnityEngine;

namespace VideoPoker
{
	//-//////////////////////////////////////////////////////////////////////
	/// 
	/// The main game manager
	/// 
	public class GameManager : MonoBehaviour
	{
		public static GameManager Instance;

		[SerializeField] public UIManager uiManager;
		[SerializeField] public DeckManager deckManager;

		[SerializeField] public PlayerHand playerHand;

		//-//////////////////////////////////////////////////////////////////////
		/// 
		void Awake()
		{
			if (Instance == null)
			{
				Instance = this;
			}
			else {
				Destroy(this);
			}
		}

		//-//////////////////////////////////////////////////////////////////////
		/// 
		void Start()
		{
			playerHand.ResetHand();
		}
		
		//-//////////////////////////////////////////////////////////////////////
		/// 
		void Update()
		{
		}

		public void StartGame() {
			deckManager.ShuffleDeck();
			playerHand.ResetHand();
			playerHand.NewHand();
		}
	}
}