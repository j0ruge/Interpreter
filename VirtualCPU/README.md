Aqui está o projeto de Máquina virtual (CPU) do curso de Arquitetura de Computadores ministrado pelo mestre Miguel Angelo Zaccur de Figueiredo

### Alunos:
* Brenno de C.P.F. Sciammarella
* Jorge Luis de Oliveira Ferrari

**OBS**:
No código fonte, existem 7 módulos de memória contendo instruções cujo podem ser alteradas na linha 128, para M1, M2, M3, M4, M5, M6, M7.<br>

| Memória | Operação | 
|---------|----------|
| M1    | ADD      |
|M2  |SUD|
|M3  |AND|
|M4  |OR|
|M5  |XOR|
|M6  |INCREMENT_A|
|M7  |INCREMENT_A com CLEAR|

```
EXEMPLO DE EXECUÇÃO

== MEMÓRIA INICIAL ==

MEM[0] 0001
MEM[1] 0110
MEM[2] 0111
MEM[3] 0111
MEM[4] 1111
MEM[5] 0000
MEM[6] 1000
MEM[7] 0011

== FIM MEMÓRIA INICIAL ==

== FLAGS INICIAIS ==

Transbordo (Overflow): False
Negativo (Negative): False
Zero (Zero): False

== FIM FLAGS INICIAIS ==

== REGISTRADORES INICIAIS ==

REGISTRADOR_A: 0000
REGISTRADOR_B: 0000
IR: 0000

== FIM REGISTRADORES INICIAIS ==

== EXECUÇÃO ==

ADD A B
Endereco_Pos: 6
Reg_A: 1000
Endereco_Pos: 7
Reg_B: 0011
Reg_A (Acc): 1011
HALT

== FIM EXECUÇÃO ==

== MEMÓRIA FINAL ==

MEM[0] 0001
MEM[1] 0110
MEM[2] 0111
MEM[3] 0111
MEM[4] 1111
MEM[5] 0000
MEM[6] 1000
MEM[7] 1011

== FIM MEMÓRIA FINAL ==

== FLAGS FINAIS ==

Transbordo (Overflow): False
Negativo (Negative): False
Zero (Zero): False

== FIM FLAGS FINAIS ==

== REGISTRADORES FINAIS ==

REGISTRADOR_A: 1011
REGISTRADOR_B: 0011
IR: 1111

== FIM REGISTRADORES FINAIS ==

Aperte qualquer tecla para continuar
```
