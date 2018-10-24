grammar MyGrammar;

/*
 * Parser Rules
 */

compileUnit
	:	EOF
	;

program
    : expression
;
 
expression
    : '(' expression ')'    #Parenthesis
    | expression operate = ('*' | '/') expression   #MultiplyDivide
    | expression operate = ('+' | '-') expression   #AddSubtraction    
    | INT   #Number
;

/*
 * Lexer Rules
 */

//WS
//	:	' ' -> channel(HIDDEN)
//	;
	  
ADD : '+' ;
SUB : '-' ;
MUL : '*' ;
DIV : '/' ;
 
INT : '0'..'9'+ ;
 
WS : [ \t\r\n]+ -> skip ;