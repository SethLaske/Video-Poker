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
		
		public PlayerBalanceManager playerBalanceManager;
		public HelpManager helpManager;
		public EffectManager effectManager;
		public AudioManager audioManager;
		public CurrencyManager currencyManager;

		[Header("Controllers")]
		public PlayerHandController playerHandController;
        
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
            if (playerBalanceManager == null) Debug.LogError("PlayerBalanceManager not assigned to GameManager");
            if (helpManager == null) Debug.LogError("HelpManager not assigned to GameManager");
            if (effectManager == null) Debug.LogError("EffectManager not assigned to GameManager");
            if (audioManager == null) Debug.LogError("AudioManager not assigned to GameManager");
            if (playerHandController == null) Debug.LogError("PlayerHandController not assigned to GameManager");
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
            effectManager.Tick(delta);
            helpManager.Tick(delta);
            effectManager.Tick(delta);
			currencyManager.Tick(delta);

            playerHandController.Tick(delta);		//Handled after audioManager

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

			playerHandController.ResetHand();
			playerHandController.NewHand();

			return true;
		}

		public void FirstHandDone() {
			uiManager.HoldPhaseEnabled();
		}

		public bool DrawNewCards() {
            playerHandController.DrawNewCards();

			return true;
        }

		public void EndGame() { 

            Hand highestHand = gameRules.GetHandRank(playerHandController.GetCurrentCardArray());

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