using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Azure;
using Azure.AI.OpenAI;
using YodaApp.Configuration;
using YodaApp.Models;
using YodaApp.Services.Interfaces;

namespace YodaApp.Services
{
    public class YodaFactsAi : IAiAssistant
    {
        private readonly ISettings _settings;
        private readonly HttpClient _httpClient;
        private const string AssistantBehaviorDescription = "I am your Yoda AI assistant.";

        public YodaFactsAi(ISettings settings, HttpClient httpClient)
        {
 
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<string> GetRandomStarWarsFact()
        {
            try
            {
                // Make a GET request to your API endpoint
                HttpResponseMessage response = await _httpClient.GetAsync("your-api-endpoint");

                // If the request is successful (status code 200), read and return the fact
                if (response.IsSuccessStatusCode)
                {
                    string fact = await response.Content.ReadAsStringAsync();
                    return fact;
                }
                else
                {
                    // If the request fails, handle the error accordingly
                    return "Failed to retrieve Star Wars fact. Please try again later.";
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur during the request
                return "Error retrieving Star Wars fact. Please try again later.";
            }
        }

        public ChatResponseMessage GetCompletion(IList<ChatMessage> chatInboundHistory, ChatMessage userMessage)
        {
            var chatContext = BuildChatContext(chatInboundHistory, userMessage);
            var client = new OpenAIClient(new Uri(_settings.AzureOpenAiEndPoint), new AzureKeyCredential(_settings.AzureOpenAiKey));
            string deploymentName = "gpt35turbo16";

            var chatCompletionsOptions = new ChatCompletionsOptions()
            {
                AzureExtensionsOptions = new AzureChatExtensionsOptions()
                {
                    Extensions =
                    {
                        new AzureSearchChatExtensionConfiguration()
                        {
                            SearchEndpoint = new Uri("https://loadsheddingaism.openai.azure.com"), // Replace with your search endpoint
                            Authentication = new OnYourDataApiKeyAuthenticationOptions("615a1c5c8afb43fd9494ea6ba6117587"), // Replace with your search API key
                            IndexName = "index",
                        }
                    }
                },
                DeploymentName = deploymentName
            };

            foreach (var message in chatContext)
                chatCompletionsOptions.Messages.Add(message);

            var response = client.GetChatCompletions(chatCompletionsOptions);
            return response.Value.Choices[0].Message;
        }

        private IList<ChatRequestMessage> BuildChatContext(IList<ChatMessage> chatInboundHistory, ChatMessage userMessage)
        {
            var chatContext = new List<ChatRequestMessage>();

            chatContext.Add(new ChatRequestSystemMessage(AssistantBehaviorDescription));

            foreach (var chatMessage in chatInboundHistory)
                chatContext.Add(new ChatRequestAssistantMessage(chatMessage.MessageBody));

            chatContext.Add(new ChatRequestUserMessage(userMessage.MessageBody));

            return chatContext;
        }

        string IAiAssistant.GetRandomStarWarsFact()
        {
            throw new NotImplementedException();
        }
    }
}
