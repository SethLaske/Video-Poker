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
		[SerializeField] public EffectManager effectManager;
		[SerializeField] public AudioManager audioManager;

		[SerializeField] public PlayerHand playerHand;

		public GameRules gameRules;

		public bool isGameActive { get; private set; }
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
			isGameActive = false;
		}
		
		//-//////////////////////////////////////////////////////////////////////
		/// 
		void Update()
		{
			effectManager.Tick(Time.deltaTime);
		}

		public void StartGame() {
			isGameActive=true;

			playerBalanceManager.ChangeBalance(-1 * playerBalanceManager.ChangeBet(0));

			deckManager.ShuffleDeck();
			playerHand.ResetHand();
			playerHand.NewHand();
		}

		public void EndGame() { 
			isGameActive = false;

            Hand highestHand = gameRules.GetHandRank(playerHand.GetCurrentCardArray());

            uiManager.DisplayResults(highestHand);

			float playerGain = playerBalanceManager.ChangeBet(0) * highestHand.payout;

            playerBalanceManager.ChangeBalance(playerGain);

			if (highestHand.payout > 0) { 
				effectManager.EndGameEffects(highestHand, playerGain);
				audioManager.PlaySound(audioManager.winSound);
			}
		}
	}
}