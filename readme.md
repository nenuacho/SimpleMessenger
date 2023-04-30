# SimpleMessenger

Simple and fast pub/sub messenger. Works with unity, but does not depend on it

## Unity Installation

In the Unity editor: 

```bash
Window -> Package Manager -> + -> Add package from git URL -> https://github.com/nenuacho/SimpleMessenger.git
```

## Usage
You can use class or struct as message:
```csharp
public struct ChatMessage
{
    public string Author;
    public string Text;
}
```


Publish
```csharp
Dispatcher.Default.Pub(new ChatMessage() {Author = "Username", Text = "Hello"});
```
Subscribe
```csharp
Dispatcher.Default.Sub<ChatMessage>(OnChatMessage);
```
```csharp
private void OnChatMessage(ChatMessage message)
{
    Debug.Log($"{message.Author} says {message.Text}");
}
```
You can filter messages with precondition function:
```csharp
Dispatcher.Default.Sub<ChatMessage>(OnChatMessage, m => !_blackList.Contains(m.Author));
```

Unsubscribe
```csharp
Dispatcher.Default.Unsub<ChatMessage>(OnChatMessage);
```

Remove all subscribes
```csharp
Dispatcher.Default.UnsubAll();
```

## Instead of singletone, you can use it with any IOC FW or Service Locator
Zenject example:
```csharp
Container.Bind<IDispatcher>().To<Dispatcher>().AsSingle();
```
```csharp
[Inject] IDispatcher _dispatcher;
```
```csharp
_dispatcher.Sub<ChatMessage>(OnChatMessage);
```

## License
[MIT](https://choosealicense.com/licenses/mit/)
