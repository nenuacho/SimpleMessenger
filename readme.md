# SimpleMessenger

Simple and fast sub/pub messenger.

## Unity Installation

In the Unity editor: 

```bash
Window -> Package Manager -> + -> Add package from git URL -> https://privatevoid@bitbucket.org/privatevoid/simplemessenger.git
```

## Usage

Message example
```csharp
public class ChatMessage
{
    public string Author { get;set; }
    public string Text { get;set; }
}
```

Publish
```csharp
Dispatcher.Default.Publish(new ChatMessage() {Author = "Username", Text = "Hello"});
```
Subscribe
```csharp
Dispatcher.Default.Subscribe<ChatMessage>(OnChatMessage);

private void OnChatMessage(ChatMessage message)
{
    Debug.Log($"{message.Author} says {message.Text}";);
}
```

## Instead of singletone, you can use it with any IOC FW
Zenject example:
```csharp
Container.Bind<IDispatcher>().To<Dispatcher>().AsSingle();
```
```csharp
[Inject] IDispatcher _dispatcher;
```
```csharp
_dispatcher.Subscribe<ChatMessage>(OnChatMessage);

## License
[MIT](https://choosealicense.com/licenses/mit/)