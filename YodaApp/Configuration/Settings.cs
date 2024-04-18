using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YodaApp.Configuration
{
    public class ConstantSettings : ISettings
    {
        public string AzureOpenAiEndPoint { get => "https://loadsheddingaism.openai.azure.com"; } // Replace with your actual OpenAI endpoint
        public string AzureOpenAiKey { get => "615a1c5c8afb43fd9494ea6ba6117587"; } // Replace with your actual OpenAI API key
    }
}
