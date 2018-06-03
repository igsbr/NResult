This is an implementation of the Result pattern in C#. 

# Cheat sheets 

## Basic usage

```
using NResult;
using static NResult.Helpers;

// ...

    private Result<int, string> IsPositive(int i)
    {
        if (i > 0)
        {
            return OK(i);
        }
        else
        {
            return Fail("wrong");
        }
    }
```

Special version ```Result<T>``` with an ```Exception``` as a default for the right part:
```
using NResult;
using static NResult.Helpers;

// ...

    private Result<int> ValidateCount(int count)
    {
        if (count >= 0)
        {
            return OK(count);
        }
        else
        {
            return Error(new ValidationError());
        }
    }
```

Sometimes can be useful a more classic approach without using the ```"using static"``` directive:
```
var i1 = Result.OK(42);
var i2 = Result.OK(42).AsOK();

var f1 = Result.Fail("wrong"); 
var f2 = "wrong".AsFail(); 

var e1 = Result.Error(new ValidationError());
var e2 = (new ValidationError()).AsError(); 
```

```
var res = ValidateCount(42); // see above

var count = one.Match(
        ok: v => v,
        err: e => throw e
	);

Assert.Equal(42, count);
```

```
var (isOK, Value, Err) = ValidateCount(42); // Result<int, Exception>

Assert.True(isOK);
Assert.Equal(42, Value);
Assert.Equal(42, Value);
Assert.IsAssignableFrom<ValidationError>(Err);
```
Also you can use query expression syntax (something like "do" notaition in Haskell):
```
    var foo = 42;
    var bar = 17;

    var fooBar = from i in ValidateCount(foo)
                 let k = i * 2
                 from j in ValidateCount(bar)
                 select k + j;

    Assert.True(fooBar.IsOK);
    Assert.Equal(101, fooBar.Value);
```
```
    var foo = 42;
    var bar = -17; // negative

    fooBar = from i in ValidateCount(foo)
             let k = i * 2
             from j in ValidateCount(bar)
             select k + j;

    Assert.False(fooBar.IsOK);
    Assert.IsAssignableFrom<ValidationError>(fooBar.Err);
```