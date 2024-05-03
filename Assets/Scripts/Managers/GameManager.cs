using UnityEngine;

namespace VideoPoker
{
	//-//////////////////////////////////////////////////////////////////////
	/// 
	/// The main game manager
	/// 
	public class GameManager : Branch
	{
		public static GameManager Instance;

		[Header ("Managers")]
		public UIManager uiManager;
		public DeckManager deckManager;
		public PlayerBalanceManager playerBalanceManager;
		public HelpManager helpManager;
		public EffectManager effectManager;
		public AudioManager audioManager;
		public PlayerHandManager playerHandManager;
		public CurrencyManager currencyManager;

        [Header("Game Rules")]
        public GameRules gameRules;

		public bool isGameActive { get; private set; }

		//-//////////////////////////////////////////////////////////////////////
		///
		/// Create Singleton immediately
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
		protected override void Initialize()
		{
			base.Initialize();

			if (uiManager == null) Debug.LogError("UIManager not assigned to GameManager");
            if (deckManager == null) Debug.LogError("DeckManager not assigned to GameManager");
            if (playerBalanceManager == null) Debug.LogError("PlayerBalanceManager not assigned to GameManager");
            if (helpManager == null) Debug.LogError("HelpManager not assigned to GameManager");
            if (effectManager == null) Debug.LogError("EffectManager not assigned to GameManager");
            if (audioManager == null) Debug.LogError("AudioManager not assigned to GameManager");
            if (playerHandManager == null) Debug.LogError("PlayerHandManager not assigned to GameManager");
            if (currencyManager == null) Debug.LogError("CurrencyManager not assigned to GameManager");
            if (gameRules == null) Debug.LogError("GameRules not assigned to GameManager");

            isGameActive = false;
		}

        private void Update()
        {
			Tick(Time.deltaTime);
        }

        //-//////////////////////////////////////////////////////////////////////
        /// 
        public override void Tick(float delta)
		{
			base.Tick(delta);

            audioManager.Tick(delta);
            playerBalanceManager.Tick(delta);
            deckManager.Tick(delta);
            effectManager.Tick(delta);
            helpManager.Tick(delta);
            effectManager.Tick(delta);
			currencyManager.Tick(delta);

            playerHandManager.Tick(delta);		//Handled after audioManager

            uiManager.Tick(delta);		//Handled after playerBalanceManager
		}

        //-//////////////////////////////////////////////////////////////////////
        ///
        /// Functions to control the main gameflow
        /// 
        public bool StartGame() {
			isGameActive=true;

			float newBalance = playerBalanceManager.ChangeBalance(-1 * playerBalanceManager.GetBet());

            if (newBalance < 0)
            {
                audioManager.PlaySound(audioManager.buttonReject);
				isGameActive = false;
				return false;
            }

            deckManager.ShuffleDeck();
			playerHandManager.ResetHand();
			playerHandManager.NewHand();

			return true;
		}

		public void FirstHandDone() {
			uiManager.HoldPhaseEnabled();
		}

		public bool DrawNewCards() {
            playerHandManager.DrawNewCards();

			return true;
        }

		public void EndGame() { 

            Hand highestHand = gameRules.GetHandRank(playerHandManager.GetCurrentCardArray());

            uiManager.DisplayResults(highestHand);

			float playerGain = highestHand.GetPayoutAmount();

            playerBalanceManager.ChangeBalance(playerGain);

			if (highestHand.payoutMultiplier > 0) { 
				effectManager.EndGameEffects(highestHand, playerGain);
				audioManager.PlaySound(audioManager.winSound);
			}

			isGameActive = false;

			uiManager.PreGamePhase();
		}
	}
}