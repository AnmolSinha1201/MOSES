Hosting Documentation
-----------
Document contains common syntax for host side for MOSES.

Quick Links :

* [Runtime](#Runtime)
* [Interop Class](#Interop)
* [Classes](#Classes)
* [Functions](#Functions)
* [Variables](#Variables)

<a name="Runtime"></a> Creating Moses Runtime
----------
Each MOSES runtime is responsible for running a single script. Multiple runtimes can be hosted at a time. A runtime can be hosted by creating a new instance.
```
var runtime = new MOSES.Runtime() // uses testFile.txt
var runtime = new MOSES.Runtime("fileName.txt")
```

Each runtime creates its own instance of ```Interop``` class. Therefore it is recommended that the class created while creating the runtime to be used for host-script interop.

Host must call ```runtime.execute()``` method to start executing the runtime script.


<a name="Interop"></a> Interop Class 
----------
Interop class is obtained by ```runtimeObject.Interop```. Each runtime has its own unique Interop class. This class is used for host-script interop.

<a name="Classes"></a> Classes
----------
Creating a class : A script-compatible class can be generated from host using the  function
```
interop.newClassDef(context, string name)
```
* the new class definition is the returned value. This might be used with other MOSES functions as appropiate context.
* ```context``` is the class to which the class would be registered, which allows class nesting. If ```null```, the class would be registered to global context.
* scripts can see the class by its ```name```. Therefore ```var = new name()``` would allow the script to access the class. 
* If the definition already exists, an error is thrown.

Fetching a class : Host can get a class definition defined either by host or by script using
```
interop.getClassDef(context, name)
```
* ```context``` is obtained by appropiate MOSES functions. If ```null```, global context is used to search the class.
* ```name``` is name of the class to be searched. If no class is found, an error is thrown.
* the class definition is returned which is compatible with all ```contexts```

Creating instance : Host can create instance of MOSES class defined by either host itself or by the script. This can be done by calling the function
```
interop.createInstance(context, name)
interop.createInstance(context, name, args)
```
* ```context``` is obtained by appropiate MOSES functions. If ```null```, global context is used to search the class.
* ```name``` is name of the class to be searched. If no class is found, an error is thrown.
* If ```args``` is present, constructor is called as well. ```args``` is of type ```IContainer[]```.
* No constructor is called if args is absent.
* a class is returned which is compatibale with all ```contexts```

<a name="Functions"></a> Functions
----------
Registering a function : A host can register a function which can be called by the script or the host itself.
```
interop.registerFunction(context, delegate, definition)
```
* ```context``` is the class for which function is registered. if ```null```, the function is registered to the global context. 
* ```Delegate``` is the function delegate of type  ```function(object context, IContainer[] args)```. 
* ```Definition``` is a string, used to define a function signature. It is similar to script function definition ```funcName(var, var = 123, var*) {}```.  This would allow the script to call the function with ```funcName(params)```

Note : The arguments received by delegate are unnamed, therefore if there are matching variable names as parameters, the arguments will not reflect that.

Invoking a function : A function, whether it is a host-declared function or a script-declared function can be invoked by the host.
```
interop.invokeFunction(context, functionName, args)
```
* ```context``` is obtained using appropiate MOSES functions. If null, global context is used.
* ```functionName``` is a string, and name of the method to be invoked
* ```args``` is the argument array of type ```IContainer[]```

Note : If the context has a ```__call``` meta-function defined, the ```invokeFunction``` call will be redirected to it. Therefore the actual target function may / may not be called. Another overload of ```invokeFunction``` might be used for calling the function without redirecting it to ```__call```, however it is reserved to be used internally.


<a name="Variables"></a> Variables
----------
Fetching a variable : Variables can be fetched from global contexts as well as from a class instance. ```instance``` here is an instance of ```Interop``` class
```
instance.getVariable(IContainer container, string name)
instance.getVariable(IContainer container, int index)
instance.getVariable(object context, int index)
instance.getVariable(object context, string name)
```
all the overloaded forms of functions are can be used to obtain the variable from a context. If context is ```null```, variables are obtained from global context. contexts are supposed to be classes which are obtained from appropiate MOSES functions.
Overloads using ```IContainer``` are used in case of Arrays and Dictionaries.

Equivalent methods for setting a variable :
```
setVariable(IContainer container, string name, object value)
setVariable(IContainer container, int index, object value)
setVariable(object context, string name, object value)
setVariable(object context, int index, object value)
```

