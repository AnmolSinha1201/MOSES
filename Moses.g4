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
    | ifElseLadder
    | returnBlock
    | prePostIncrDecr
    | tryCatchFinally
    ;

tryCatchFinally
    : 'try' segmentBlock ('catch' segmentBlock ('finally' segmentBlock)?)?
    ;

ifElseLadder
    : 'if' '(' exp ')' segmentBlock ('else' segmentBlock)?
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

loopParse
    : 'loopParse' '(' exp ',' exp ')' segmentBlock
    ;

loops
    : loop
    | whileLoop
    | loopParse
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
    | unaryOp                                   #unaryVar
    | prePostIncrDecr                           #unaryIncrDecr
    | <assoc=right> exp operatorPower exp       #expOpPow
    | exp operatorMulDivMod exp                 #expMulDivMod
    | exp operatorAddSub exp                    #expAddSub
    | exp ' . ' exp                             #expConcat
    | exp operatorComparison exp                #expCompare
    | exp operatorBitwise exp                   #expBitwise
    | exp operatorAnd exp                       #expAND
    | exp operatorOr exp                        #expOR
    ;

unaryOp
    : operatorUnary complexVariable
    | operatorUnary NUMBER
    ;

constExp
    : NUMBER                                    #number
    | STRING                                    #string
    ;

prePostIncrDecr
    : preIncrDecr
    | postIncrDecr
    ;

preIncrDecr
    : incrDecr complexVariable
    ; 

postIncrDecr
    : complexVariable incrDecr
    ;

incrDecr
    : '++'
    | '--'
    ;

operatorOr 
    : '||';

operatorAnd 
    : '&&';

operatorComparison 
    : '<' | '>' | '<=' | '>=' | '!=' | '==';

operatorPower
    : '**'
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
    : '&' | '|' | '^' | '<<' | '>>';



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