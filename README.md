This is a simple implementation of the Result pattern in C#. 

It’s inspired by the following:

[Don't publish Domain Events, return them!](https://blog.jayway.com/2013/06/20/dont-publish-domain-events-return-them/)
and
[Railway oriented programming](http://fsharpforfunandprofit.com/posts/recipe-part2/)
and by Greg Young: “I’m gonna tell you a little secret - there’s no such thing as a one-way command. They don’t exist.” 
[Greg Young — A Decade of DDD, CQRS, Event Sourcing](https://youtu.be/LDW0QWie21s?t=1675)

## Result<T>

When we use a more straightforward implementation of the pseudo-union class in C# we have to specify the type even if we aren’t interested in returning the value and trying to return an error:

```charp
public Result<EventsCollection> HandleCommand(...)
{
	if(Validate(...))
	{
		return Result<EventsCollection>.Error("Validation error");
	}
    ...
}
```

This implementation uses some trick so we can write the less annoying code: 

```charp
public Result<EventsCollection> HandleCommand(...)
{
	if(Validate(...))
	{
		return Result.Error("Validation error");
	}
    ...
}
```

See the difference: `Result<EventsCollection>.Error("...")` vs `Result.Error("...")`

Also this library allows the monadic programming style by using the LINQ query syntax.
