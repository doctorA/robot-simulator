grammar RobotLanguage;

options
{
 language=CSharp2;
 output=AST;
 ASTLabelType=CommonTree; 
}

public start
	:	prog 
	{
	 if ($prog.tree != null)
	  System.Console.WriteLine($prog.tree.ToStringTree());
         else 
 	  System.Console.WriteLine("drevo je prazno");
	};

prog	:	job name pos npos user tool postype rectan (rconf? cindeks)+ inst date comm attr frame group main -> job name pos npos user tool postype rectan rconf cindeks inst date comm attr frame group main;

job 	:	JOB;
name	:	NAME ('A'..'Z')+;
pos	:	POS;
npos	:	NPOS INT (','? '-'? INT)+;
user	:	USER INT;
tool	:	TOOL INT;
postype	:	POSTYPE ('USER' | 'PULSE');
rectan	:	RECTAN;
rconf	:	RCONF (','? ('0'|'1'))+;
cindeks	:	'C' INT+ '=' (','? '-'? REAL)+;
inst	:	INST;
date	:	DATE INT+ '/'! INT+ '/'! INT+ TIME;
comm	:	COMM (INT|'A'..'Z')+;
attr	:	ATTR (','? ('A'..'Z')+)+;
frame	:	FRAME 'USER' INT;
group	:	GROUP ('A'..'Z'|INT)+;
main	:	'NOP' ukazi 'END';
ukazi	:	premik DOUT TIMER premiki DOUT premik;
premiki	:	premik premiki | premik;
premik	:	(MOVJ 'C' INT+ 'VJ' '='! REAL) | (MOVL 'C' INT+ 'V' '='! REAL) | (MOVC 'C' INT+ 'V' '='! REAL) | (MOVS 'C' INT+ 'V' '='! REAL);

JOB	:	'/JOB';
NAME	:	'//NAME';
POS	:	'//POS';
NPOS	:	'///NPOS';
USER	:	'///USER';
TOOL	:	'///TOOL';
POSTYPE	:	'///POSTYPE';
RECTAN	:	'///RECTAN';
RCONF	:	'///RCONF';
INST	:	'//INST';
DATE	:	'///DATE';
TIME	:	INT+ ':' INT+;
COMM	:	'///COMM';
ATTR	:	'///ATTR';
FRAME	:	'////FRAME';
GROUP	:	'///GROUP1';
DOUT	:	'DOUT OT#' '(' INT ')' ' ' ('ON' | 'OFF');
DIN	:	'DIN OT#' '(' INT ')';
TIMER	:	'TIMER T=' REAL;
MOVJ	:	'MOVJ';
MOVL	:	'MOVL';
MOVC	:	'MOVC';
MOVS	:	'MOVS';

REAL	:	INT+ '.' INT+;
INT 	:	('0'..'9')+;
NEWLINE	:	'\r'? '\n' {Skip();};
WS 	:	(' '|'\t')+ {Skip();};


