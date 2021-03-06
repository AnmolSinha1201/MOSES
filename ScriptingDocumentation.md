Scripting Documentation
-----------
Document contains common syntax for script side for MOSES.

Quick Links :

* [Variable Assign](#varAssign)
* [Escape Characters](#escapeCharacters)
* [Unary Operations](#unaryOp)
* [Binary Operations](#binaryOp)
* [Conditionals](#conditionals)
* [Loops](#loops)
* [Functions](#functions)
* [Classes and Objects](#classes)
* [Arrays and Dictionaries](#arraysAndDictionaries)
* [Pre-defined functions](#preDefFunctions)
* [Meta-functions](#metaFunctions)
* [Comments](#comments)
* [Includes](#includes)
* [Try-Catch-Finally](#tryCatchFinally)


<a name="varAssign"></a>Variable assign
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
global variables can be accessed using ```_```, and instance variables can be accessed using ```this```

```
//also supported +=, -=, *=, /=, %=, **=, .=, |=, &=, ^=, >>=, <<=
```

<a name="escapeCharacters"></a>Escape Characters
----------------
```
\" - Quotes
\r - Carraige Return
\n - Line Feed
\t - Horizontal Tab
\v - Vertical Tab
\\ - Escaped Backslash
```

<a name="unaryOp"></a>Unary Operators
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

<a name="binaryOp"></a>Binary Operators
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

<a name="conditionals"></a>Conditionals
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

<a name="loops"></a>Loops
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
```
for(initialization; condition; increment)
    //repeat the code till condition is met.
```

* current iteration index can be accessed using variable ```M_Index```
* current parse value can be accessed using variable ```M_LoopField``` (loopParse only)
* all three parameters for ```for``` loop are optional. ```initialization``` is executed once when the loop is called, ```condition``` is executed each time before loop body is executed and ```increment``` is executed after.
* ```for``` loop does not use ```M_Index```. Therefore, script must maintain its own index.


<a name="functions"></a>Functions
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

<a name="classes"></a>Classes and Objects
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
// ONLY static classes can access inner class.

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

<a name="arraysAndDictionaries"></a>Arrays and dictionaries
-------------
Arrays :
```
var = ["text", variable, new object()]
otherVar = var[0]
var[3] = "other text"
```
arrays and dictionaries are equivalent and mostly interchangable; and non-number indices/keys can be used to store the corresponding values.
```
var = {"key" : "value", 123 : 456}
otherVar = var[key]
otherVar2 = var[123]
var["key2"] = "value"
```
Arrays and dictionaries don't require constant expressions (as in the case of class declarations). The syntax ```instance[key]``` and ```instance.key``` are interchangeable. However, numberical values can't be used as keys in the later format.

<a name="preDefFunctions"></a>Pre-defined object functions
-------------
Several functions are pre-defined for all MOSES objects, and are available with Dictionaries, Arrays or even custom classes.
```
object.count() - returns number of keys in given object
object.clone() - clones to object and returns the cloned instance (duplicates primitive and leaves objects as reference, i.e. creates shallow copy)
object.haskey(key) - returns boolean value if object has key
object.remove(key) - removes a key from object (does not maintain order in case of arrays)
```

<a name="metaFunctions"></a>Meta-Functions
-------------
Meta functions should be used inside class definitions. Any meta function (except __new) which does not match the default signature is considered as a regular function, and will not be recognized if it is called.

Default signatures :
- __new : any parameters
- __delete : no parameter
- __call : 2 parameters
- __set : 2 parameters
- __get : 1 parameter
Parameters can be of any type (variadic, default valued, regular).
```
// A default constructor will be called if caller is passing no parameter (new class()), and class itslef is not overriding it.
__new(param1)
{
    //acts as constructor to the class.
    //returning nothing returns the instance of an object. Returning some non-null value prevents object creation.
    // this makes the class a static-only, i.e. no instance of that class can be created.
}
```
```
__delete()
{
    //acts as destruuctor to the class.
    //It can return some value, which will be assigned to its LValue.
}
```
```
//invoked when object.function() is called
__call(functionName, param)
{
    // functionName contains name of the function called.
    // param[index] contains parameters.
    // return works like a regular function.
}
```
```
//invoked when object.var = value is used
__set(varName, value)
{
    //varName contains name of the variable.
    //value contains the value.
    //if some value is returned, it is assigned to its Placeholder.
}
```
```
//invoked when object.var is queried
__get(varName)
{
    //varName contains name of the variable.
    //returned value is assigned to its placeholder. If nothing is returned, placeholder/LValue becomes null/empty.
}
```

<a name="comments"></a>Comments
-------------
```
//line comment
/*
block comment
*/
```

<a name="includes"></a>Including files
-------------
MOSES includes are dynamic, i.e. they are included during runtime. While this does give flexibility, one must be sure to include files at correct position. Since files are included at runtime, all the constructs and code are processed at that position. This means if the included file contains some construct like class or value, the parent file will not see it unless its been already processed. If there is parse error, an exception is thrown which is processed by host.
```
include(expression_containing_file_name) //can be variable or a string or a function etc.
```

<a name="tryCatchFinally"></a>Try-Catch-Finally
-------------
Try must be coupled with catch and/or finally. Catch can catch an exception which displays exception.message
```
try { some code }
catch{ some code } // or catch varName{ some code}
finally{ some code }
```