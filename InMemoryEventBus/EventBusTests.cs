namespace InterviewExercises.InMemoryEventBus;

public class EventBusTests
{
    [Fact]
    public void Publish_InvokesTheHandlerRegisteredForTheEventType()
    {
        var eventBus = new EventBus();
        var receivedEvents = new List<OrderPlaced>();

        eventBus.Subscribe<OrderPlaced>(receivedEvents.Add);

        var orderPlaced = new OrderPlaced(42);
        eventBus.Publish(orderPlaced);

        var received = Assert.Single(receivedEvents);
        Assert.Same(orderPlaced, received);
    }

    //Added tests
    [Fact]
    // Makes sure that handlers only take events that they have subscribed to.
    public void Publish_OnlyInvokesHandlersForTheMatchingEventType()
    {
        var eventBus = new EventBus();
        var placed = new List<OrderPlaced>();
        var shipped = new List<OrderShipped>();

        eventBus.Subscribe<OrderPlaced>(placed.Add);
        eventBus.Subscribe<OrderShipped>(shipped.Add);

        eventBus.Publish(new OrderPlaced(1));

        Assert.Single(placed);
        Assert.Empty(shipped);
    }

    [Fact]
    // Makes sure that all eligible handlers are invoked.
    public void Publish_InvokesEveryHandlerSubscribedToTheEventType()
    {
        var eventBus = new EventBus();
        var first = new List<OrderPlaced>();
        var second = new List<OrderPlaced>();

        eventBus.Subscribe<OrderPlaced>(first.Add);
        eventBus.Subscribe<OrderPlaced>(second.Add);

        var orderPlaced = new OrderPlaced(7);
        eventBus.Publish(orderPlaced);

        Assert.Same(orderPlaced, Assert.Single(first));
        Assert.Same(orderPlaced, Assert.Single(second));
    }

    [Fact]
    //Makes sure that handlers are invoked in the correct order
    public void Publish_InvokesHandlersInTheOrderTheyWereSubscribed()
    {
        var eventBus = new EventBus();
        var calls = new List<string>();

        eventBus.Subscribe<OrderPlaced>(_ => calls.Add("first"));
        eventBus.Subscribe<OrderPlaced>(_ => calls.Add("second"));
        eventBus.Subscribe<OrderPlaced>(_ => calls.Add("third"));

        eventBus.Publish(new OrderPlaced(1));

        Assert.Equal(["first", "second", "third"], calls);
    }

    [Fact]
    //Makes sure Publish doesn't complain if there are no subscribers
    public void Publish_WithNoSubscribers_DoesNotThrow()
    {
        var eventBus = new EventBus();

        var exception = Record.Exception(() => eventBus.Publish(new OrderPlaced(1)));

        Assert.Null(exception);
    }

    [Fact]
    //Makes sure Publish doesn't throw if the available subscribers are all other types. Similar to last one.
    public void Publish_WithSubscribersOnlyForOtherTypes_DoesNotThrow()
    {
        var eventBus = new EventBus();
        eventBus.Subscribe<OrderShipped>(_ => { });

        var exception = Record.Exception(() => eventBus.Publish(new OrderPlaced(1)));

        Assert.Null(exception);
    }

    private sealed record OrderPlaced(int OrderId);
    //added
    private sealed record OrderShipped(int OrderId);
}
