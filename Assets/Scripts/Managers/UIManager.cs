using UnityEngine;
using UnityEngine.UI;

namespace VideoPoker
{
	//-//////////////////////////////////////////////////////////////////////
	///
	/// Manages UI including button events and updates to text fields
	/// 
	public class UIManager : MonoBehaviour
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

		[SerializeField]
		private Button decreaseBetButton = null;

        [SerializeField] private string introGameMessage;
        [SerializeField] private string defaultGameMessage;
		[SerializeField] private float betIncrements;
        //-//////////////////////////////////////////////////////////////////////
        /// 
        void Awake()
		{
		}

		//-//////////////////////////////////////////////////////////////////////
		/// 
		void Start()
		{
			betButton.onClick.AddListener(OnBetButtonPressed);
			drawButton.onClick.AddListener(OnDrawButtonPressed);
			helpButton.onClick.AddListener(OnHelpButtonPressed);
			increaseBetButton.onClick.AddListener(OnIncreaseBetButtonPressed);
			decreaseBetButton.onClick.AddListener(OnDecreaseBetButtonPressed);
            
			betButton.interactable = true;
            drawButton.interactable = false;
			helpButton.interactable = true;
			increaseBetButton.interactable = true;
			decreaseBetButton.interactable = true;

            winningText.text = introGameMessage;
            UpdatePlayerBalance(GameManager.Instance.playerBalanceManager.ChangeBalance(0));
            betText.text = "Bet: $" + GameManager.Instance.playerBalanceManager.ChangeBet(0);
        }

		public void DisplayResults(Hand hand) {
			winningText.text = hand.winningMessage;
			hand.winningEffect?.Invoke();
		}

		public void UpdatePlayerBalance(float newBalance) { 
			currentBalanceText.text = "Balance: $" + newBalance.ToString("F2");
		}

		//-//////////////////////////////////////////////////////////////////////
		///
		/// Event that triggers when bet button is pressed
		/// 
		private void OnBetButtonPressed()
		{
			winningText.text = defaultGameMessage;
			GameManager.Instance.StartGame();

			betButton.interactable = false;
			drawButton.interactable = true;
            increaseBetButton.interactable = false;
            decreaseBetButton.interactable = false;
        }

		private void OnDrawButtonPressed() { 
			GameManager.Instance.playerHand.DrawNewCards();

            betButton.interactable = true;
            drawButton.interactable = false;
            increaseBetButton.interactable = true;
            decreaseBetButton.interactable = true;
        }

		private void OnHelpButtonPressed() { 
			GameManager.Instance.helpManager.TogglePayoutTable();
		}

		private void OnIncreaseBetButtonPressed() {
			betText.text = "Bet: $" + GameManager.Instance.playerBalanceManager.ChangeBet(betIncrements);
		}

		private void OnDecreaseBetButtonPressed() {
            betText.text = "Bet: $" + GameManager.Instance.playerBalanceManager.ChangeBet(-1 * betIncrements);
        }
	}
}