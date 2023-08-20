using Standard.AI.OpenAI.Models.Services.Foundations.ChatCompletions;

namespace IeltsApp.Interface
{
    public interface IOpenAIProxy
    {
        Task<ChatCompletionMessage[]> SendChatMessage(string message);
    }
}