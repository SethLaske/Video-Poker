using UnityEngine;

namespace VideoPoker
{
	//-//////////////////////////////////////////////////////////////////////
	/// 
	/// The main game manager
	/// 
	public class GameManager : Manager
	{
		public static GameManager Instance;

		[SerializeField] public UIManager uiManager;
		[SerializeField] public DeckManager deckManager;
		[SerializeField] public PlayerBalanceManager playerBalanceManager;
		[SerializeField] public HelpManager helpManager;
		[SerializeField] public EffectManager effectManager;
		[SerializeField] public AudioManager audioManager;
		[SerializeField] public PlayerHandManager playerHandManager;


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
		protected override void Initialize()
		{
			base.Initialize();

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

            audioManager.Tick(Time.deltaTime);
            playerBalanceManager.Tick(Time.deltaTime);
            deckManager.Tick(Time.deltaTime);
            effectManager.Tick(Time.deltaTime);
            helpManager.Tick(Time.deltaTime);
            effectManager.Tick(Time.deltaTime);

            playerHandManager.Tick(Time.deltaTime);		//Handled after audioManager

            uiManager.Tick(Time.deltaTime);		//Handled after playerBalanceManager
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

			float playerGain = playerBalanceManager.GetBet() * highestHand.payout;

            playerBalanceManager.ChangeBalance(playerGain);

			if (highestHand.payout > 0) { 
				effectManager.EndGameEffects(highestHand, playerGain);
				audioManager.PlaySound(audioManager.winSound);
			}

			isGameActive = false;

			uiManager.PreGamePhase();
		}
	}
}