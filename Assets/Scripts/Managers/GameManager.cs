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
		[SerializeField] public PlayerBalanceManager playerBalanceManager;
		[SerializeField] public HelpManager helpManager;

		[SerializeField] public PlayerHand playerHand;
		public GameRules pokerHands;

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
			
		}
		
		//-//////////////////////////////////////////////////////////////////////
		/// 
		void Update()
		{
		}

		public void StartGame() {
			playerBalanceManager.EditBalance(-1 * playerBalanceManager.currentBetSize);

			deckManager.ShuffleDeck();
			playerHand.ResetHand();
			playerHand.NewHand();
		}

		public void EndGame(Hand highestHand) { 
			uiManager.DisplayResults(highestHand);
			playerBalanceManager.EditBalance(playerBalanceManager.currentBetSize * highestHand.payout);
		}
	}
}