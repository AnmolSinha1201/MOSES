<img src="http://i.imgur.com/p3XrMwh.png" width=400>

MosesFramework for .NET
--------------------
Modifiable Scriptable Embeddable System for .NET based applications.

<img src = "http://i.imgur.com/5hiH4P1.png">

And the result is fully scriptable application. Host provides its own functions, so scripts are bound to them. The host also choses the directives to be used by MOSES. This allows a host to decide whether the script can include other files or refer to a .NET assembly.

Current Version
--------------------
Alpha 1.0


Current Features
------------------
* Seperate scripting runtimes
* Static class/instance differentiation
* Automated GC
* No variable types while scripting
* Strict variable types while using in host
* Meta Functions (new, delete, call, set, get) for classes
* Host has complete access to script
* Host can build instances, new class definitions, functions on the fly


Requirements
-------------------
* .NET framework 4.6

Roadmap
--------------

|Milestone|Status|
|---------|------|
|Interface redisign for multiple runtimes
|Unified types for memory and performance optimization
|.NET type casting instead of type assumption implementaion
|Built-in function support for variable dereferencing
|native windows application support by DllExport
|Cross package support for script-host-native(win) compatibility
|Cross platform support for script-host system
|Memory optimization for extra objects being created (CDef)
|Debugging by Line/Column count
|Option for changing default stream for error report and output
|Allow expression coalascing and chaining
|Allow expressions in parenthesis to be coalasced and chained
|Resolution of arrays and variadics
|Smarter objects for arrays, dictionaries and object rather than generic objects
|Error throwing/stdout at required places.
|Disallow '_' and 'this' as variable names.
|CoreCLR implementation.
|Easier syntax changing by generating code on the fly.
