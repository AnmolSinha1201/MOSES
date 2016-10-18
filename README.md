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
* Case insensitive
* Strict variable types while using in host
* Meta Functions (new, delete, call, set, get) for classes
* Host has complete access to script
* Host can build instances, new class definitions, functions on the fly


Requirements
-------------------
* .NET framework 4.5

Roadmap
--------------

|Milestone|Status|
|---------|------|
|Case-insensitivity|Completed
|different equality operators(+=, *= etc)|Pending
|comments (inline/block)|Completed
|file inclusion|Completed
|context-free-functions|Pending
|Allow scoping of classes by static names (class.class.varOrFunction)|Completed
|Remove '//' operator in favor of comments|Completed
|ternary operators|Completed
|null coalescing operators|Completed
|Interface redisign for multiple runtimes|Completed
|native windows application support by DllExport|Pending
|Cross platform support for script-host system|Pending\*
|Better debugging options (Line/Column pos)|Pending
|Option for changing default stream for error report and output|Pending
|Allow expressions in parenthesis to be coalasced and chained|Completed
|Smarter objects for arrays, dictionaries and object rather than generic objects|Pending\*
|Error throwing/stdout at required places|Pending
|Disallow '_' and 'this' as variable names|Pending
|CoreCLR implementation.|Pending*
|Easier syntax changing by generating code on the fly.|Pending\*
|Implement escape sequences|Pending
\* = To be implemented with CoreCLR implementation

Documentaion
--------------
[Link](Documentation.md)
