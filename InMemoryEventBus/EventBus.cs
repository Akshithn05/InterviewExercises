namespace InterviewExercises.InMemoryEventBus;

public sealed class EventBus
{
    //Dictionary to hold all the handlers
    private Dictionary<Type, List<Delegate>> _handlers = [];
    public void Subscribe<TEvent>(Action<TEvent> handler)
    {
        //If this is the first subscriber, create and store a new list
        if (!_handlers.TryGetValue(typeof(TEvent), out var handlers))
        {
            _handlers[typeof(TEvent)] = handlers = [];
        }

        //adds subscription to the list
        handlers.Add(handler);
    }

    public void Publish<TEvent>(TEvent @event)
    {
        //no handlers available for this, so do nothing
        if (!_handlers.TryGetValue(typeof(TEvent), out var handlers))
        {
            return;
        }

        //iterates over all handlers
        foreach (var handler in handlers.ToArray())
        {
            //casts it since it is a delegate by default
            ((Action<TEvent>)handler)(@event);
        }
    }
}
