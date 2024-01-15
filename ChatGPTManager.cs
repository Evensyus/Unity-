using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenAI;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class ChatGPTManager : MonoBehaviour
{
    public OnResponseEvent OnResponse;
    public AudioSource m_AudioSource;
    private Text AskText;
    private string newText;

    [System.Serializable]
    public class OnResponseEvent : UnityEvent<string> { }

    private OpenAIApi openAI = new OpenAIApi("sk-I8YB5W4XgJYsH4UR2f6c6f117874449fB5B135C16dAa76A9"); //org-jtztGqMHt22yCLXFZyRzR7At
    private List<ChatMessage> messages = new List<ChatMessage>();

    public async void AskChatGPT()//string newText
    {
        AskText = GameObject.Find("Lunarcom/Canvas/OutputText").GetComponent<Text>();
        newText = AskText.text;
        ChatMessage newMessage = new ChatMessage();
        newMessage.Content = newText;
        newMessage.Role = "user";

        messages.Add(newMessage);

        CreateChatCompletionRequest request = new CreateChatCompletionRequest();

        request.Messages = messages;
        request.Model = "gpt-3.5-turbo";

        var response = await openAI.CreateChatCompletion(request);

        if (response.Choices != null && response.Choices.Count > 0)
        {
            var chatResponse = response.Choices[0].Message;
            messages.Add(chatResponse);

            Debug.Log(chatResponse.Content);
            AzureTTSNormal.Instance.StartTTS(chatResponse.Content, m_AudioSource);

            OnResponse.Invoke(chatResponse.Content);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
