grammar Moses;

fragment A:('a'|'A');
fragment B:('b'|'B');
fragment C:('c'|'C');
fragment D:('d'|'D');
fragment E:('e'|'E');
fragment F:('f'|'F');
fragment G:('g'|'G');
fragment H:('h'|'H');
fragment I:('i'|'I');
fragment J:('j'|'J');
fragment K:('k'|'K');
fragment L:('l'|'L');
fragment M:('m'|'M');
fragment N:('n'|'N');
fragment O:('o'|'O');
fragment P:('p'|'P');
fragment Q:('q'|'Q');
fragment R:('r'|'R');
fragment S:('s'|'S');
fragment T:('t'|'T');
fragment U:('u'|'U');
fragment V:('v'|'V');
fragment W:('w'|'W');
fragment X:('x'|'X');
fragment Y:('y'|'Y');
fragment Z:('z'|'Z');

TRY : T R Y;
CATCH : C A T C H;
FINALLY : F I N A L L Y;
IF : I F;
ELSE : E L S E;
RETURN : R E T U R N;
BREAK : B R E A K;
CONTINUE : C O N T I N U E;
CLASS : C L A S S;
WHILE : W H I L E;
LOOP : L O O P;
LOOPPARSE : L O O P P A R S E;
REF : R E F;
THIS : T H I S;
NEW : N E W;
INCLUDE : I N C L U D E;

chunk
    : block* EOF
    ;

block
    : innerfunctionBlock 
    | functionDecl
    | classDecl
	| includeBlock
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

includeBlock
	: INCLUDE '(' exp ')'
	;

tryCatchFinally
    : TRY segmentBlock (CATCH segmentBlock (FINALLY segmentBlock)?)?
    ;

ifElseLadder
    : IF '(' exp ')' segmentBlock (ELSE segmentBlock)?
    ;

returnBlock
    : RETURN exp?
    ;

loopBlock
    : innerfunctionBlock
    | BREAK | CONTINUE
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
    : CLASS NAME '{' classBlock* '}'
    ;

functionDecl
    : functionDef functionBody
    ;

whileLoop
    : WHILE '(' exp ')' segmentBlock
    ;

loop
    : LOOP '(' exp ')' segmentBlock
    ;

loopParse
    : LOOPPARSE '(' exp ',' exp ')' segmentBlock
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
    : REF
    ;

functionBody
    : '{' innerfunctionBlock* '}'
    ;

segmentBlock
    : loopBlock 
    | '{' loopBlock* '}'
    ;

this
    : THIS
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
    : NEW complexVariable '(' exp? (',' exp)* ')' 
    ;

exp
    : constExp                                  #constantExp
    | complexVariable                           #variableFetch
    | complexFunctionCall                       #functionFetch
    | newInstance                               #newClassObject
    | varAssign                                 #variableAssign
    | unaryOp                                   #unaryVar
    | prePostIncrDecr                           #unaryIncrDecr
    | <assoc=right> exp operatorPower exp       #expOpPow
    | exp '?' exp ':' exp                       #expTernary
	| exp '??' exp								#expNullCoalesce
    | exp operatorMulDivMod exp                 #expMulDivMod
    | exp operatorAddSub exp                    #expAddSub
    | exp ' . ' exp                             #expConcat
    | exp operatorComparison exp                #expCompare
    | exp operatorBitwise exp                   #expBitwise
    | exp operatorAnd exp                       #expAND
    | exp operatorOr exp                        #expOR
	| '(' exp ')'								#expPriority
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
    : '*' | '/' | '%';

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

BLOCK_COMMENT
    : '/*' .*? '*/' -> skip
;

LINE_COMMENT
    : '//' ~[\r\n]* -> skip
;