## VirtualCPU

Aqui está o projeto de Máquina virtual (CPU) do curso de Arquitetura de Computadores ministrado pelo mestre Miguel Angelo Zaccur de Figueiredo

### Alunos:
* Brenno de C.P.F. Sciammarella
* Jorge Luis de Oliveira Ferrari

----

**OBS**:
No código fonte, existem 7 módulos de memória contendo instruções que podem ser alteradas através do menu inicial.

| Memória | Operação | 
|---------|----------|
| M1    | ADD      |
|M2  |SUB|
|M3  |AND|
|M4  |OR|
|M5  |XOR|
|M6  |INCREMENT_A|
|M7  |INCREMENT_A com CLEAR|

----

### MENU DE EXECUÇÃO


```
Menu Principal da Maquina Virtual

  1. M1 => ADD Reg_A Reg_B
  2. M2 => SUB Reg_A Reg_B
  3. M3 => AND Reg_A Reg_B
  4. M4 => OR Reg_A Reg_B
  5. M5 => XOR Reg_A Reg_B
  6. M6 => INCREMENT_A
  7. M7 => INCREMENT_A CLEAR

Escolha um setup de memória =>
```


### EXEMPLO DE EXECUÇÃO

```

M1 => ADD Reg_A Reg_B

== MEMORIA INICIAL ==

MEM[0] 0001
MEM[1] 0110
MEM[2] 0111
MEM[3] 0111
MEM[4] 1111
MEM[5] 0000
MEM[6] 1000
MEM[7] 0011

== FIM MEMORIA INICIAL ==

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

== EXECUÇAO ==

ADD A B
Endereco_Pos: 6
Reg_A: 1000
Endereco_Pos: 7
Reg_B: 0011
Reg_A (Acc): 1011
HALT

== FIM EXECUÇAO ==

== MEMORIA FINAL ==

MEM[0] 0001
MEM[1] 0110
MEM[2] 0111
MEM[3] 0111
MEM[4] 1111
MEM[5] 0000
MEM[6] 1000
MEM[7] 1011

== FIM MEMORIA FINAL ==

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
