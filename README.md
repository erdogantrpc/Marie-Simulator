# Marie Simulator
Used CLASS and Linked list instead of array when creating RAM structure. In this way, the program gained flexibility and readability.
# Syntax
There should only be one Marie code per line.
Marie code must contain an ORG command.
![line](https://user-images.githubusercontent.com/43681383/88286585-a5466b00-ccf9-11ea-9f3e-50be37a06d57.png)
# Input Variable
Entering "Input" value in the program when prompted, to the desired line of code "Input" command should be written. then from the bottom corner type should be selected. When the program runs, Prompt to enter input value the screen will appear.
# Reaching Variables
Reaching the variables and values defined after running the Marie Program, just click on the desired variable from the Tags table in the lower right corner.
Variable after click its location in memory will be selected in the Ram table. Same time the decimal value of the variable you selected It will be located under 'Etiketler' table.
# Example
MARIE program that multiplies each of the numbers between (210)16-(22F)16 fields in memory by 8 and overwrites it.<br/>
ORG 1F0<br/>
Loop1<br/>
Load Loop2_temp<br/>
Store Loop2_counter<br/>
Load Total_temp<br/>
Store Total<br/>
Loop2<br/>
Load Total<br/>
AddI Address<br/>
Store Total<br/>
Load Loop2_counter<br/>
Subt One<br/>
Store Loop2_counter<br/>
Skipcond 0400<br/>
Jump Loop2<br/>
Load Total<br/>
StoreI Address<br/>
Load Address<br/>
Add One<br/>
Store Address<br/>
Load Loop1_counter<br/>
Subt One<br/>
Store Loop1_counter<br/>
Skipcond 0400<br/>
Jump Loop1<br/>
Halt<br/>
Loop2_counter, DEC 8<br/>
Loop2_temp, DEC 8<br/>
Loop1_counter, DEC 13<br/>
Address, HEX 210<br/>
One, DEC 1<br/>
Total, DEC 0<br/>
Total_temp, DEC 0<br/>
A, DEC 27<br/>
B, DEC 39<br/>
C, DEC 11<br/>
D, DEC 30<br/>
E, DEC 23<br/>
F, DEC 0<br/>
G, DEC 30<br/>
H, DEC 58<br/>
I, DEC 20<br/>
J, DEC -12<br/>
K, DEC 67<br/>
L, DEC 44<br/>
M, DEC 72<br/>
N, DEC 3<br/>
O, DEC 81<br/>
P, DEC 48<br/>
R, DEC 0<br/>
S, DEC 16<br/>
T, DEC 9<br/>
U, DEC 0<br/>
V, DEC 1<br/>
Y, DEC 28<br/>
Z, DEC -5<br/>
AA, DEC 37<br/>
BB, DEC 0<br/>
CC, DEC 85<br/>
DD, DEC 61<br/>
EE, DEC 4<br/>
FF, DEC 5<br/>
GG, DEC 0<br/>
HH, DEC 49<br/>
END
