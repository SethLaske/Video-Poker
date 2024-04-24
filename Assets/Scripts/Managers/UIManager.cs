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
		private Button betButton = null;

        [SerializeField]
        private Button drawButton = null;

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
            
			betButton.interactable = true;
            drawButton.interactable = false;
		}

		//-//////////////////////////////////////////////////////////////////////
		///
		/// Event that triggers when bet button is pressed
		/// 
		private void OnBetButtonPressed()
		{
			GameManager.Instance.StartGame();
			betButton.interactable = false;
			drawButton.interactable = true;
		}

		private void OnDrawButtonPressed() { 
			GameManager.Instance.playerHand.DrawNewCards();
            betButton.interactable = true;
            drawButton.interactable = false;
        }
	}
}