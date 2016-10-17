Documentation
-----------
Document contains common syntax for script side and host for MOSES.

Variable assign
-------------
```
var = var2
var = 123
var = 0x123 //hex
var = 123.123
var = "some string"
var = var2 = 123
var = functionCall(params)
var = new className(params)
var[key] = value //only valid if var is an object.
var.key = value //equivalent to above
```

Unary Operators
-------------
```
var = +variableOrValue
var = -variableOrValue
var = !variableOrValue // flips boolean value.
var = ~variableOrValue // flips bit values. MUST be integer.
```
```
// pre/post increment/decrement
++var
var++
--var
var--
```

Binary Operators
-------------
```
varOrValue1 ** varOrValue1 //performs LHS ^ RHS (power operator)
varOrValue1 * varOrValue2 //multiply
varOrValue1 / varOrValue2 //divide
varOrValue1 % varOrValue2 //Modulo operator. returns LHS Modulo RHS
varOrValue1 + varOrValue2 //add
varOrValue1 - varOrValue2 //subtract
(expression) //priority evaluation
```
```
varOrValue1 . varOrValue2 //concatenate
```
```
varOrValue1 < varOrValue2 //LT
varOrValue1 > varOrValue2 //GT
varOrValue1 <= varOrValue2 //LTE
varOrValue1 >= varOrValue2 //GTE
varOrValue1 != varOrValue2 //NEQ
varOrValue1 == varOrValue2 //EQ
```
```
varOrValue1 & varOrValue2 //Bitwise AND
varOrValue1 | varOrValue2 //Bitwise OR
varOrValue1 ^ varOrValue2 //Bitwise XOR
varOrValue1 << varOrValue2 //Bitwise Leftshift
varOrValue1 >> varOrValue2 //Bitwise Rightshift
```
```
varOrValue1 && varOrValue2 //conditional AND
varOrValue1 || varOrValue2 //conditional OR
```

Conditionals
--------------------
```
if (some expression to be evaluated)
	//some inline code
```
```
if (some expression to be evaluated)
{
	//some code
}
else if (some other condition)
	//some code
else
	//some code
```
```
variable = condition ? ifTrue : ifFalse
```
```
//both are equivalent
variable = condition ?? ifFalse
variable = expression1 ? expression1 : expression2
```

Loops
-------------
```
while (someCondition)
	//inline code
while (someCondition
{
	//block code
}
```
```
loop (iterationCount i)
	//repeats the code for given iterations i 
```
```
loopParse(string, delimiter)
	//repeat for EACH delimiter value
```
* current iteration index can be accessed using variable ```M_Index```
* current parse value can be accessed using variable ```M_LoopField``` (loopParse only)


Functions
-------------
Declaration :
```
someFunction(var1, var2 = "default value")
{
	//code block
}
```
```
someFunction(ref var1, var2) //var1 is passed by reference. Any variable can be passed by ref.
{
	//code block
}
```
```
//This function will accept minimum 1 variable. var2 is variadic, and values passed can be accessed as var2[index]. Variadics can ONLY be the last parameter of function signature. They can not have any default value.
someFunction(var1, var2*)
{
	//code block
}
```
```
//returns can be used inside functions. To return a null or nothing, use return.
return
return someExpression
```
Usage :
```
someFunction(some expression, variable, other paramtere)
object.someFunction()
someFunction().otherFunction() //if someFunction returns valid object.
someFunction().object.otherFunction()
```
If function does not return anything, or returns a null, the LValue will be a null or empty.

Classes and Objects
-------------
```
class NAME
{
	constantVariable = "string"
    constantVariable2 = 123
    
    someFunction()
    {
    	//code block
        return this.constantVariable
    }
    
    class NESTED
    {
    	//code block
    }
}
```
```
//usage : 
variable = new className()
```
class variables can __ONLY__ be constants, i.e. no expression evaluated at runtime can be assigned. However, there is no limitation on variables used inside the function. All the rules applied to regular functions apply to class member functions as well.

Static Classes:
```
var = new NAME()
//var2 = var.someFunction() will produce error, since NESTED is not in the scope of instance variable var.
//var2 = NAME.someFunction() will work, since NAME is considered static in this context.
//var2 = new NAME.NESTED() will work, since NAME is considered static in this context.

class NAME
{
    someFunction()
    {
    	//code block
        return new this.NESTED()
    }
    
    class NESTED
    {
    	//code block
    }
}
```

Meta-Functions
-------------
```
__new(param1)
{
	//acts as constructor to the class.
}
```
```
__delete()
{
	//acts as destruuctor to the class.
}
```
```
//invoked when object.function() is called
__call(functionName, param)
{
	// functionName contains name of the function called.
    // param[index] contains parameters.
}
```
```
//invoked when object.var = value is used
__set(varName, value)
{
	//varName contains name of the variable.
    //value contains the value.
}
```
```
//invoked when object.var is queried
__new(varName)
{
	//varName contains name of the variable.
}
```


Try-Catch-Finally
-------------
catch and finally are optional.
```
try { some code }
catch{ some code }
finally{ some code }
```