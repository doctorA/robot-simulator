grammar RobotLanguage;

options
{
 language=CSharp3;
 output=AST;
 ASTLabelType=CommonTree; 
}

public start
	:	prog;

prog	:	job name pos npos user tool postype rectan rconf cindeks+ inst date comm attr frame group main -> job name pos npos user tool postype rectan rconf cindeks+ inst date comm attr frame group main;

job 	:	JOB;
name	:	NAME NIZ;
pos	:	POS;
npos	:	NPOS INT (','! ostanek)+;
ostanek	:	INT;
user	:	USER INT;
tool	:	TOOL INT;
postype	:	POSTYPE ('USER' | 'PULSE');
rectan	:	RECTAN;
rconf	:	RCONF ('0'|'1') (','! ('0'|'1'))+;
cindeks	:	INDEKS '='! MINUS? REAL (','! MINUS? REAL)+;
inst	:	INST;
date	:	DATE INT+ '/'! INT+ '/'! INT+ TIME;
comm	:	COMM NIZ+;
attr	:	ATTR NIZ (','! NIZ)+;
frame	:	FRAME 'USER' INT;
group	:	GROUP NIZ;
main	:	'NOP' ukazi 'END';
ukazi	:	premik DOUT ONOFF TIMER '='! REAL premiki DOUT ONOFF premik;
premiki	:	premik premiki | premik;
premik	:	(MOVJ INDEKS 'VJ' '='! REAL) | (MOVL INDEKS 'V' '='! REAL) | (MOVC INDEKS 'V' '='! REAL) | (MOVS INDEKS 'V' '='! REAL);

JOB	:	'/JOB';
NAME	:	'//NAME';
POS	:	'//POS';
NPOS	:	'///NPOS';
USER	:	'///USER';
TOOL	:	'///TOOL';
POSTYPE	:	'///POSTYPE';
RECTAN	:	'///RECTAN';
RCONF	:	'///RCONF';
INDEKS	:	'C' INT+;
INST	:	'//INST';
DATE	:	'///DATE';
TIME	:	INT+ ':' INT+;
COMM	:	'///COMM';
ATTR	:	'///ATTR';
FRAME	:	'////FRAME';
GROUP	:	'///GROUP';
DOUT	:	'DOUT OT#' '(' INT ')';
DIN	:	'DIN OT#' '(' INT ')';
TIMER	:	'TIMER T';
MOVJ	:	'MOVJ';
MOVL	:	'MOVL';
MOVC	:	'MOVC';
MOVS	:	'MOVS';
ONOFF	:	'ON' | 'OFF';

MINUS	:	'-';
NIZ	:	'A'..'Z'+;
REAL	:	INT+ '.' INT+;
INT 	:	'0'..'9'+;
NEWLINE	:	'\r'? '\n' {Skip();};
WS 	:	(' '|'\t')+ {Skip();};


