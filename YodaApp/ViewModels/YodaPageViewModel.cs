using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Maui.Core.Platform;
using CommunityToolkit.Mvvm.Input;
using YodaApp.Models;
using YodaApp.Services.Interfaces;

namespace YodaApp.ViewModels
{
    public partial class YodaPageViewModel : BaseViewModel
    {
        private readonly IAiAssistant _assistant;

        private ObservableCollection<ChatMessage> _chatHistory;
        public ObservableCollection<ChatMessage> ChatHistory
        {
            get => _chatHistory;
            set
            {
                _chatHistory = value;
                OnPropertyChanged();
            }
        }

        private string _currentQuestion;
        public string CurrentQuestion
        {
            get => _currentQuestion;
            set
            {
                _currentQuestion = value;
                OnPropertyChanged();
            }
        }

        public ICommand GetRandomStarWarsFactCommand { get; }

        public YodaPageViewModel(IAiAssistant assistant)
        {
            _assistant = assistant;

            GetRandomStarWarsFactCommand = new Command(async () => ExecuteGetRandomStarWarsFact());

            ChatHistory = new ObservableCollection<ChatMessage>();
            ChatHistory.Add(new ChatMessage { MessageType = Enums.ChatMessageTypeEnum.Inbound, MessageBody = "Help you today, how can I? Hello, say you, hmm?" });
        }
        private async Task<string> GetResponseFromAI()
        {
            try
            {
                // Request a response from the AI assistant service
                string response = _assistant.GetRandomStarWarsFact();

                return response;
            }
            catch (Exception ex)
            {
                // Handle exceptions
                return "Error: Unable to get response from AI";
            }
        }

        private async Task ExecuteGetRandomStarWarsFact()
        {
            try
            {
                // Get the response from the AI
                string response = await GetResponseFromAI();

                // Add the received response to the chat history
                ChatHistory.Add(new ChatMessage { MessageType = Enums.ChatMessageTypeEnum.Inbound, MessageBody = response });
            }
            catch (Exception ex)
            {
                // Handle exceptions
            }
        }
    }
}
