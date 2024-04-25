using UnityEngine;
using UnityEngine.UI;

namespace VideoPoker
{
	//-//////////////////////////////////////////////////////////////////////
	///
	/// Manages UI including button events and updates to text fields
	/// 
	public class UIManager : Manager
	{
		[SerializeField]
		private Text currentBalanceText = null;

		[SerializeField]
		private Text winningText = null;

        [SerializeField]
        private Text betText = null;


        [SerializeField]
		private Button betButton = null;

        [SerializeField]
        private Button drawButton = null;

		[SerializeField]
		private Button helpButton = null;

		[SerializeField]
		private Button increaseBetButton = null;
		private Text increaseBetText;

		[SerializeField]
		private Button decreaseBetButton = null;
		private Text decreaseBetText;

        [SerializeField]
        private Button toggleCurrencyButton = null;

        [SerializeField] private string introGameMessage;
        [SerializeField] private string defaultGameMessage;
		[SerializeField] private string insufficientFundsMessage;
		
        

		//-//////////////////////////////////////////////////////////////////////
		/// 
		protected override void Initialize()
		{
            base.Initialize();

            betButton.onClick.AddListener(OnBetButtonPressed);
			drawButton.onClick.AddListener(OnDrawButtonPressed);
			helpButton.onClick.AddListener(OnHelpButtonPressed);
			increaseBetButton.onClick.AddListener(OnIncreaseBetButtonPressed);
			decreaseBetButton.onClick.AddListener(OnDecreaseBetButtonPressed);
			toggleCurrencyButton.onClick.AddListener(OnToggleCurencyButtonPressed);
            
			betButton.interactable = true;
            drawButton.interactable = false;
			helpButton.interactable = true;
			increaseBetButton.interactable = true;
			decreaseBetButton.interactable = true;

            winningText.text = introGameMessage;

            UpdatePlayerBalance();
            UpdatePlayerBet();

			increaseBetText = increaseBetButton.GetComponentInChildren<Text>();
            decreaseBetText = decreaseBetButton.GetComponentInChildren<Text>();
			UpdateBetIncrementTexts();
        }

        //-//////////////////////////////////////////////////////////////////////
        ///
        /// Updating UI text
        /// 

        public void DisplayResults(Hand hand) {
			winningText.text = hand.winningMessage;
			hand.winningEffect?.Invoke();
		}

		public void UpdatePlayerBalance() {
			float balance = GameManager.Instance.playerBalanceManager.GetBalance();
			currentBalanceText.text = "Balance: " + GameManager.Instance.currencyManager.GetCurrencyString(balance);
		}

        public void UpdatePlayerBet()
        {
            float bet = GameManager.Instance.playerBalanceManager.GetBet();
            betText.text = "Bet: " + GameManager.Instance.currencyManager.GetCurrencyString(bet);
        }

        public void UpdateBetIncrementTexts()
        {
			float increment = GameManager.Instance.gameRules.betIncrement;
            increaseBetText.text = "+ " + GameManager.Instance.currencyManager.GetCurrencyString(increment);
            decreaseBetText.text = "- " + GameManager.Instance.currencyManager.GetCurrencyString(increment);
        }

        //-//////////////////////////////////////////////////////////////////////
        ///
        /// Event that triggers when buttons are pressed
        /// 
        private void OnBetButtonPressed()
		{
			if (GameManager.Instance.StartGame())
			{
				winningText.text = defaultGameMessage;


				betButton.interactable = false;

                increaseBetButton.interactable = false;
                decreaseBetButton.interactable = false;

                GameManager.Instance.audioManager.PlaySound(GameManager.Instance.audioManager.buttonPress);
			}
			else { 
				winningText.text = insufficientFundsMessage;
			}

        }

		private void OnDrawButtonPressed() {
			if (GameManager.Instance.DrawNewCards()) { 
				drawButton.interactable = false;

				GameManager.Instance.audioManager.PlaySound(GameManager.Instance.audioManager.buttonPress);
			}

        }

		private void OnHelpButtonPressed() { 
			GameManager.Instance.helpManager.TogglePayoutTable();

            GameManager.Instance.audioManager.PlaySound(GameManager.Instance.audioManager.buttonPress);
        }

		private void OnIncreaseBetButtonPressed() {
            GameManager.Instance.audioManager.PlaySound(GameManager.Instance.audioManager.buttonPress);

			GameManager.Instance.playerBalanceManager.ChangeBet(1 * GameManager.Instance.gameRules.betIncrement);

			UpdatePlayerBet();
        }

		private void OnDecreaseBetButtonPressed() {
            GameManager.Instance.audioManager.PlaySound(GameManager.Instance.audioManager.buttonPress);

            GameManager.Instance.playerBalanceManager.ChangeBet(-1 * GameManager.Instance.gameRules.betIncrement);

            UpdatePlayerBet();
        }

		private void OnToggleCurencyButtonPressed() {
			GameManager.Instance.currencyManager.ToggleCurrency();

			UpdatePlayerBalance();
			UpdatePlayerBet();
			UpdateBetIncrementTexts();
		}

		//-//////////////////////////////////////////////////////////////////////
		///
		/// Sequencing functions
		/// 

		public void PreGamePhase() {
            betButton.interactable = true;
            increaseBetButton.interactable = true;
            decreaseBetButton.interactable = true;
        }
		public void HoldPhaseEnabled() {
            drawButton.interactable = true;
            
        }
    }
}