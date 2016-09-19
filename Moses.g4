grammar Moses;

chunk
    : block* EOF
    ;

block
    : innerfunctionBlock 
    | functionDecl
    | classDecl
    ;

innerfunctionBlock
    : varAssign 
    | complexFunctionCall
    | loops
    | 'if' '(' exp ')' segmentBlock ('else' segmentBlock)?
    | returnBlock
    ;

returnBlock
    : 'return' exp?
    ;

loopBlock
    : innerfunctionBlock
    | 'break' | 'continue'
    ;

classBlock
    : localConstVarAssign
    | functionDecl
    | classDecl
    ;

localConstVarAssign
    : NAME '=' constExp
    ;

classDecl
    : 'class' NAME '{' classBlock* '}'
    ;

functionDecl
    : functionDef functionBody
    ;

whileLoop
    : 'while' '(' exp ')' segmentBlock
    ;

loop
    : 'loop' '(' exp ')' segmentBlock
    ;

loops
    : loop
    | whileLoop
    ;

varAssign
    : complexVariable '=' exp
    ;

functionDef
    : NAME '(' functionParameterList? ')'
    ;

functionParameterList
    : functionParameterNoDefault (',' functionParameterNoDefault)* (',' functionParameterDefault)* (',' functionPrameterVariadic)?
    | functionParameterDefault (',' functionParameterDefault)* (',' functionPrameterVariadic)?
    | functionPrameterVariadic
    ;

functionParameterDefault
    : ref? NAME ('=' constExp)?
    ;

functionParameterNoDefault
    : ref? NAME
    ;

functionPrameterVariadic
    : NAME variadic
    ;

variadic
    : '*'
    ;

ref
    : 'ref'
    ;

functionBody
    : '{' innerfunctionBlock* '}'
    ;

segmentBlock
    : loopBlock 
    | '{' loopBlock* '}'
    ;

this
    : 'this'
    ;

complexFunctionCall
    : (this '.')? variableOrFunction '.' functionCall
    | (this '.')? functionCall
    ;

complexVariable
    : (this '.')? variableOrFunction '.' var
    | (this '.')? var
    | (this '.')? variableOrFunction '[' exp ']'
    ;

variableOrFunction
    : var
    | functionCall
    | variableOrFunction '.' variableOrFunction
    | variableOrFunction '[' exp ']'
    ;

var
    : NAME
    ;

functionCall
    : NAME '(' exp? (',' exp)* ')'
    ;

newInstance
	: 'new' complexVariable '(' exp? (',' exp)* ')' 
	;

exp
    : constExp                                  #constantExp
    | complexVariable                           #variableFetch
    | complexFunctionCall                       #functionFetch
    | newInstance			                    #newClassObject
    | varAssign                                 #variableAssign
    | operatorUnary variableOrFunction          #unaryVar
    | <assoc=right> exp operatorPower exp       #expOpPow
    | exp operatorMulDivMod exp                 #expMulDivMod
    | exp operatorAddSub exp                    #expAddSub
    | exp ' . ' exp                             #expConcat
    | exp operatorComparison exp                #expCompare
    | exp operatorBitwise exp                   #expBitwise
    | exp operatorAnd exp                       #expAND
    | exp operatorOr exp                        #expOR
    ;

constExp
    : NUMBER                                    #number
    | STRING                                    #string
    ;

operatorOr 
    : '||';

operatorAnd 
    : '&&';

operatorComparison 
    : '<' | '>' | '<=' | '>=' | '!=' | '==';

operatorPower
    : '^'
    ;

operatorUnary
    : '+'
    | '-'
    | '!'
    | '~'
    ;

operatorAddSub
    : '+' | '-';

operatorMulDivMod
    : '*' | '/' | '%' | '//';

operatorBitwise
    : '&' | '|' | '~' | '<<' | '>>';



STRING
    : '"' (~('"' | '\\' | '\r' | '\n') | '\\' ('"' | '\\'))* '"'
    ;

NAME
    : [a-zA-Z_][a-zA-Z_0-9]*
    ;

NUMBER
    : INT | HEX | FLOAT
    ;

INT
    : Digit+
    ;

HEX
    : '0' [xX] [0-9a-fA-F]+
    ;

FLOAT
    : Digit* '.' Digit+ 
    ;

Digit
    : [0-9]
    ;

WS  
    : [ \t\u000C]+ -> skip
    ;

NL
    : [\r\n]+ -> skip
    ;