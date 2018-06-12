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

Special version ```Result<T>``` with an ```Exception``` as a predefined type for the right part:
```
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

```
var i1 = Result.OK(42);
var f1 = Result.Fail("wrong"); 

var i2 = 42.AsOK();
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
// Assert.IsType<ValidationError>(Err);
```
Number of classic combinators:
```
var foo = new Result<int, string>(42);
var bar = new Result<int, string>(42);
var ops = new Result<int, string>("wrong");

var r1 = foo.Map(i => i * 2.0); // Result<double, string>(84.0)
var r2 = ops.Map(i => i * 2.0); // Result<double, string>("wrong")

var r3 = foo.MapFail(e => e + "!!!"); // Result<int, string>(42)
var r4 = ops.MapFail(e => e + "!!!"); // Result<int, string>("wrong!!!")

var r5 = foo.And(bar); // r5 == bar
var r6 = ops.And(bar); // r6 == ops

var r7 = foo.Or(bar); // r7 == foo
var r8 = ops.Or(bar); // r8 == bar

// ...
// see 'CombinatorsTests.cs' for other examples
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
    var bar = -17; // <- negative

    fooBar = from i in ValidateCount(foo)
             let k = i * 2
             from j in ValidateCount(bar)
             select k + j;

    Assert.False(fooBar.IsOK);
    Assert.IsType<ValidationError>(fooBar.Err);
```