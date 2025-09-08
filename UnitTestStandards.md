## Unit Tests

All new code must have a complete set of unit tests. 

### Naming convention

> The basic naming of a test comprises of three main parts: `[UnitOfWork_StateUnderTest_ExpectedBehavior]`
>
> A unit of work is a use case in the system that starts with a public method and ends up with one of three
> types of results: a return value/exception, a state change to the system which changes its behaviour, or a 
> call to a third party (when we use mocks)
> A unit of work can be a small as a method, or as large as a class, or even multiple classes. as long is it
> all runs in memory, and is fully under our control.

> Examples:
> - public void Sum_NegativeNumberAs1stParam_ExceptionThrown()
> - public void Sum_NegativeNumberAs2ndParam_ExceptionThrown()
> - public void Sum_WithSimpleValues_Calculated()

Note: 
- When checking if something is not valid  use `IsNotValid`
   > Example:
   > - FurtherDetailsComponent_AllElementsAreEmpty_IsNotValid

### Test method structure

[Arrange/Act/Assert](http://wiki.c2.com/?ArrangeActAssert)

> Each method should group these functional sections, separated by blank lines: 
> 1. Arrange all necessary preconditions and inputs
> 2. Act on the object or method under test
> 3. Assert that the expected results have occurred

### One Assert per test

Only one assert per test. This is not to say only one `Assert` statement - we should only validate one thing, e.g. 
the state of an entity or the result of a calculation, per test. To put it another way, a test should have 
one *primary* reason for failing.